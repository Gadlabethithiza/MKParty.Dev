using System;
using AutoMapper;
using AutoMapper.Execution;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetMembersByBranch
{
	public class GetAllMembersByBranchQuery : IRequest<Result<List<MemberDto>>>
    {
        public string branch_id { get; set; }

        public GetAllMembersByBranchQuery()
        {

        }

        public GetAllMembersByBranchQuery(string id)
        {
            branch_id = id;
        }
    }

    internal class GetAllMembersByBranchQueryHandler : IRequestHandler<GetAllMembersByBranchQuery, Result<List<MemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMembershipRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IAesOperation _securityService;
        private readonly IConfiguration config;
        private readonly ILogger<GetAllMembersByBranchQueryHandler> _logger;

        public GetAllMembersByBranchQueryHandler(IUnitOfWork unitOfWork, IMembershipRepository membershipRepository, IConfiguration _config, IAesOperation securityService, IMapper mapper, ILogger<GetAllMembersByBranchQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _memberRepository = membershipRepository;
            _mapper = mapper;
            config = _config;
            _securityService = securityService;
            _logger = logger;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetAllMembersByBranchQuery query, CancellationToken cancellationToken)
        {
            List<MemberDto> memberLs = new List<MemberDto>();
            var entities = await _memberRepository.GetMembersByBranchAsync(query.branch_id);
            var members = _mapper.Map<List<MemberDto>>(entities);

            if (members.Count > 0)
            {
                foreach (var item in members)
                {
                    MemberDto citem = new MemberDto();
                    citem.Guid = item.Guid;
                    citem.name = item.name;
                    citem.surname = item.surname;
                    citem.province_name = item.province_name;
                    citem.BirthDate = item.BirthDate;
                    citem.branch_name = item.branch_name;
                    citem.ward_name = item.ward_name;
                    citem.municipality_name = item.municipality_name;

                    citem.race = item.race;
                    citem.occupation = item.occupation;
                    citem.employment_status = item.employment_status;

                    citem.membership_no = item.membership_no;
                    citem.membership_date = item.membership_date;
                    citem.membership_card_required = item.membership_card_required;
                    citem.membership_card_printed = item.membership_card_printed;
                    citem.id_no = _securityService.DecryptString(config["SecurityKey"], item.id_no);
                    citem.gender = item.gender;
                    citem.prefered_lang = item.prefered_lang;
                    citem.building_site_no = item.building_site_no;
                    citem.suburb = item.suburb;
                    citem.city = item.city;
                    citem.postal_code = item.postal_code;
                    citem.region = item.region;
                    citem.sub_region = item.sub_region;

                    if (!string.IsNullOrWhiteSpace(item.email))
                        citem.email = _securityService.DecryptString(config["SecurityKey"], item.email);

                    citem.tel = item.tel;
                    citem.mobile = _securityService.DecryptString(config["SecurityKey"], item.mobile);
                    citem.mobile_use_whatsapp = item.mobile_use_whatsapp;
                    citem.role = item.role;
                    citem.username = _securityService.DecryptString(config["SecurityKey"], item.id_no);
                    citem.PasswordHash = item.PasswordHash;
                    citem.PasswordSalt = item.PasswordSalt;
                    citem.security_question = item.security_question;
                    citem.security_answer = item.security_answer;
                    memberLs.Add(citem);
                }
            }

            return await Result<List<MemberDto>>.SuccessAsync(memberLs);
        }
    }
}