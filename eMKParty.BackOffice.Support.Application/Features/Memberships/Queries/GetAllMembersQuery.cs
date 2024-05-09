using System;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetAllMembers
{
    public record GetAllMembersQuery : IRequest<Result<List<MemberDto>>>;

    internal class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, Result<List<MemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration config;
        private readonly IAesOperation _securityService;
        private readonly ILogger<GetAllMembersQueryHandler> _logger;

        public GetAllMembersQueryHandler(IUnitOfWork unitOfWork, IConfiguration _config, IAesOperation securityService, IMapper mapper, ILogger<GetAllMembersQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            config = _config;
            _securityService = securityService;
            _logger = logger;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetAllMembersQuery query, CancellationToken cancellationToken)
        {
            List<MemberDto> memberLs = new List<MemberDto>();

            var members = await _unitOfWork.Repository<MemberRegister>().Entities
                   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            if(members.Count > 0)
            {
                foreach(var item in members)
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