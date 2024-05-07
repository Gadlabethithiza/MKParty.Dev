﻿using System;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.LoginMember
{
	public class LoginMemberCommand : IRequest<Result<UserDto>>, IMapFrom<LoginDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    internal class CreateMemberCommandHandler : IRequestHandler<LoginMemberCommand, Result<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IAesOperation _securityService;
        private readonly IMembershipRepository _memberRepository;
        //private string key = "b14ca5898a4e4133bbce2ea2315a1916";

        public CreateMemberCommandHandler(ITokenService tokenService, IMembershipRepository membershipRepository, IAesOperation securityService, IMapper mapper)
        {
            _tokenService = tokenService;
            _securityService = securityService;
            _memberRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(LoginMemberCommand command, CancellationToken cancellationToken)
        {
            var user = await _memberRepository.GetMembersByUsernameAsync(command.Username);

            if (user == null) return await Result<UserDto>.FailureAsync(null, "Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt); //for un-Hashing Password stored on the datbase

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(command.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return await Result<UserDto>.FailureAsync(null, "Invalid Password");
            }

            UserDto member_item = new UserDto
            {
                Username = user.username,
                Token = _tokenService.CreateToken(user)
            };

            return await Result<UserDto>.SuccessAsync(member_item, "Success");
        }
    }
}