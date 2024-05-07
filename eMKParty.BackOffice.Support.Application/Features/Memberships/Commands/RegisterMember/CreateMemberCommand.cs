﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Players.Commands.CreatePlayer;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember
{
	public class CreateMemberCommand : IRequest<Result<MemberDto>>, IMapFrom<MemberRegister>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int province_id { get; set; }
        public int? branch_id { get; set; }
        public int? ward_id { get; set; }
        public string membership_no { get; set; }
        public DateTime membership_date { get; set; }
        public Boolean? membership_card_required { get; set; }
        public Boolean? membership_card_printed { get; set; }
        public string id_no { get; set; }
        public string gender { get; set; }
        public string prefered_lang { get; set; }
        public string building_site_no { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string mobile { get; set; }
        public Boolean mobile_use_whatsapp { get; set; }
        public string? role { get; set; }
        public string? username { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public string? security_question { get; set; }
        public string? security_answer { get; set; }
    }

    internal class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<MemberDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IAesOperation _securityService;
        private readonly ILogger _logger;
        private readonly IConfiguration config;
        private readonly string key = "testdata";//"b14ca5898a4e4133bbce2ea2315a1916";

        public CreateMemberCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IAesOperation securityService, IConfiguration _config, IMapper mapper, ILogger<CreateMemberCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _securityService = securityService;
            _mapper = mapper;
            _logger = logger;
            config = _config;
            key = config["SecurityKey"];
        }

        public async Task<Result<MemberDto>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
        {
            //string key = config["SecurityKey"];

            if (!string.IsNullOrWhiteSpace(command.id_no))
                if (await UserExist(command.id_no)) return await Result<MemberDto>.SuccessAsync(null, "South African ID No is already registered.");

            _logger.LogInformation($"Passed ID Number", DateTime.UtcNow.ToLongTimeString());


            if (!string.IsNullOrWhiteSpace(command.email))
                if (await EmailExist(command.email)) return await Result<MemberDto>.SuccessAsync(null, "Membership Email address is already taken.");

            _logger.LogInformation($"Passed email verification", DateTime.UtcNow.ToLongTimeString());

            try
            {
                using var hmac = new HMACSHA512(); //for Hashing Password

                var member = new MemberRegister()
                {
                    name = command.Name,
                    surname = command.Surname,
                    province_id = command.province_id,
                    BirthDate = command.BirthDate,
                    branch_id = command.branch_id,
                    ward_id = command.ward_id,
                    membership_no = command.membership_no,
                    membership_date = command.membership_date,
                    membership_card_required = command.membership_card_required,
                    membership_card_printed = command.membership_card_printed,
                    id_no = _securityService.EncryptString(key, command.id_no), //must be encripted
                    gender = command.gender,
                    prefered_lang = command.prefered_lang,
                    building_site_no = command.building_site_no,
                    suburb = command.suburb,
                    city = command.city,
                    postal_code = command.postal_code,
                    region = command.region,
                    sub_region = command.subregion,
                    email = command.email,//must be encripted
                    tel = command.tel,//must be encripted
                    mobile = _securityService.EncryptString(key, command.mobile),//must be encripted
                    mobile_use_whatsapp = command.mobile_use_whatsapp,
                    role = command.role,
                    username = _securityService.EncryptString(key, command.id_no),//must be encripted
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("555-admin")),
                    PasswordSalt = hmac.Key,
                    security_question = command.security_question,
                    security_answer = command.security_answer,
                    creationdate = DateTime.Now,
                    updateddate = DateTime.Now,
                    creationby = "Portal Administrator",
                    updatedby = "Portal Administrator",
                    Guid = Guid.NewGuid()
                };

                await _unitOfWork.Repository<MemberRegister>().AddAsync(member);
                member.AddDomainEvent(new MemberCreatedEvent(member));

                await _unitOfWork.Save(cancellationToken);

                MemberDto member_item = new MemberDto
                {
                    username = command.username,
                    Token = _tokenService.CreateToken(member)
                };

                return await Result<MemberDto>.SuccessAsync(member_item, "Member Detail Successfully Created.");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"About page visited at {ex} ", DateTime.UtcNow.ToLongTimeString());
                return await Result<MemberDto>.FailureAsync(null, "Error Occure:" + ex);
            }
        }

        private async Task<bool> UserExist(string username)
        {
            bool returnValu = false;

            if(await _unitOfWork.Repository<MemberRegister>().Entities.Where(x => x.username == _securityService.EncryptString(key, username)).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync() != null)            
                returnValu = true;            

            return returnValu;
        }

        private async Task<bool> EmailExist(string email)
        {
            bool returnValu = false;

            if (await _unitOfWork.Repository<MemberRegister>().Entities.Where(x => x.email == email).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync() != null)
                returnValu = true;

            return returnValu;
        }
    }
}