using System;
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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger _logger;
        //private string key = "b14ca5898a4e4133bbce2ea2315a1916";
        private readonly IConfiguration config;

        public CreateMemberCommandHandler(ITokenService tokenService, IMembershipRepository membershipRepository, IConfiguration _config, IAesOperation securityService, IMapper mapper, ILogger<CreateMemberCommandHandler> logger)
        {
            _tokenService = tokenService;
            _securityService = securityService;
            _memberRepository = membershipRepository;
            _mapper = mapper;
            config = _config;
            _logger = logger;
        }

        public async Task<Result<UserDto>> Handle(LoginMemberCommand command, CancellationToken cancellationToken)
        {
            var user = await _memberRepository.GetMembersByUsernameAsync(_securityService.EncryptString(config["SecurityKey"], command.Username));

            if (user == null) return await Result<UserDto>.FailureAsync(null, "Invalid Login Details");

            using var hmac = new HMACSHA512(user.PasswordSalt); //for un-Hashing Password stored on the datbase

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(command.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return await Result<UserDto>.FailureAsync(null, "Invalid Login Details");
            }

            UserDto member_item = new UserDto
            {
                Username = _securityService.DecryptString(config["SecurityKey"], user.username),
                Token = _tokenService.CreateToken(user)
            };

            return await Result<UserDto>.SuccessAsync(member_item, "Success");
        }
    }
}