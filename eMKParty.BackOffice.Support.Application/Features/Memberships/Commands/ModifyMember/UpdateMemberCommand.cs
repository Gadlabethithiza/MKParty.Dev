﻿using System;
using System.Data;
using System.Reflection;
using System.Security.Policy;
using System.Xml.Linq;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.Features.Players.Commands.UpdatePlayer;
using eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.ModifyMember
{
	public class UpdateMemberCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? province_name { get; set; }
        public string? branch_name { get; set; }
        public string? ward_name { get; set; }
        public string? municipality_name { get; set; }
        public DateTime MembershipDate { get; set; }
        public Boolean? MembershipCardRequired { get; set; }
        public Boolean? MembershipCardPrinted { get; set; }

        public string? employment_status { get; set; }
        public string? occupation { get; set; }
        public string race { get; set; }

        public string Gender { get; set; }
        public string PreferedLang { get; set; }
        public string BuildingSiteNo { get; set; }
        public string TownshipSuburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public Boolean Mobile_use_whatsapp { get; set; }
        public string? modifiedby { get; set; }
    }

    internal class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateMemberCommandHandler> _logger;

        public UpdateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateMemberCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(UpdateMemberCommand command, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.Repository<MemberRegister>().GetByIdAsync(command.Id);
            if (member != null)
            {
                member.name = command.Name;
                member.surname = command.Surname;
                member.province_name = command.province_name;
                member.BirthDate = command.BirthDate;
                member.branch_name = command.branch_name;
                member.ward_name = command.ward_name;
                member.municipality_name = command.municipality_name;

                member.race = command.race;
                member.occupation = command.occupation;
                member.employment_status = command.employment_status;

                member.membership_date = command.MembershipDate;
                member.membership_card_required = command.MembershipCardRequired;
                member.membership_card_printed = command.MembershipCardPrinted;
                member.gender = command.Gender;
                member.prefered_lang = command.PreferedLang;
                member.building_site_no = command.BuildingSiteNo;
                member.suburb = command.TownshipSuburb;
                member.city = command.City;
                member.postal_code = command.PostalCode;
                member.region = command.Region;
                member.sub_region = command.SubRegion;
                member.email = command.Email;//must be encripted
                member.tel = command.Tel;//must be encripted
                member.mobile = command.Mobile;//must be encripted
                member.mobile_use_whatsapp = command.Mobile_use_whatsapp;
                //member.modifieddate = DateTime.Now;
                //member.modifiedby = command.modifiedby;

                await _unitOfWork.Repository<MemberRegister>().UpdateAsync(member);
                member.AddDomainEvent(new MemberUpdatedEvent(member));
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(member.Id, "Member Detail Successfully updated..");
            }
            else
            {
                return await Result<int>.FailureAsync("Member Not Found.");
            }
        }
    }
}