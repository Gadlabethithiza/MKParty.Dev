using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Queries
{
	public class GetMembersBySubRegionQuery : IRequest<Result<List<MemberDto>>>
    {
        public string SubRegion { get; set; }

        public GetMembersBySubRegionQuery()
        {

        }

        public GetMembersBySubRegionQuery(string subregion)
        {
            SubRegion = subregion;
        }
    }

    internal class GetMembersBySubRegionQueryHandler : IRequestHandler<GetMembersBySubRegionQuery, Result<List<MemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMembershipRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration config;
        private readonly IAesOperation _securityService;

        public GetMembersBySubRegionQueryHandler(IUnitOfWork unitOfWork, IMembershipRepository membershipRepository, IConfiguration _config, IAesOperation securityService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _memberRepository = membershipRepository;
            _mapper = mapper;
            config = _config;
            _securityService = securityService;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetMembersBySubRegionQuery query, CancellationToken cancellationToken)
        {
            List<MemberDto> memberLs = new List<MemberDto>();
            var entities = await _memberRepository.GetMembersBySubResionAsync(query.SubRegion);
            var members = _mapper.Map<List<MemberDto>>(entities);

            if (members.Count > 0)
            {
                foreach (var item in members)
                {
                    MemberDto citem = new MemberDto();
                    citem.Guid = item.Guid;
                    citem.name = item.name;
                    citem.surname = item.surname;
                    citem.province_id = item.province_id;
                    citem.BirthDate = item.BirthDate;
                    citem.branch_id = item.branch_id;
                    citem.ward_id = item.ward_id;
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
                    citem.email = item.email;
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