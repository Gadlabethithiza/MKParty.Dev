using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Application.Features.Players.Commands.CreatePlayer;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember
{
	public class CreateMemberCommand : IRequest<Result<int>>, IMapFrom<MemberRegister>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int ProvinceId { get; set; }
        public int? BranchId { get; set; }
        public int? WardId { get; set; }
        public string MembershipNo { get; set; }
        public DateTime MembershipDate { get; set; }
        public Boolean? MembershipCardRequired { get; set; }
        public Boolean? MembershipCardPrinted { get; set; }
        public string IdNo { get; set; }
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
        public string? Role { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public string? security_question { get; set; }
        public string? security_answer { get; set; }
    }

    internal class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
        {
            var member = new MemberRegister()
            {
                name = command.Name,
                surname = command.Surname,
                province_id = command.ProvinceId,
                BirthDate = command.BirthDate,
                branch_id = command.BranchId,
                ward_id = command.WardId,
                membership_no = command.MembershipNo,
                membership_date = command.MembershipDate,
                membership_card_required = command.MembershipCardRequired,
                membership_card_printed = command.MembershipCardPrinted,
                id_no = command.IdNo, //must be encripted
                gender = command.Gender,
                prefered_lang = command.PreferedLang,
                building_site_no = command.BuildingSiteNo,
                suburb = command.TownshipSuburb,
                city = command.City,
                postal_code = command.PostalCode,
                region = command.Region,
                subregion = command.SubRegion,
                email = command.Email,//must be encripted
                tel = command.Tel,//must be encripted
                mobile = command.Mobile,//must be encripted
                mobile_use_whatsapp = command.Mobile_use_whatsapp,
                role = command.Role,
                username = command.Username,
                PasswordHash = command.PasswordHash,
                PasswordSalt = command.PasswordSalt,
                security_question = command.security_question,
                security_answer = command.security_answer,
                createddate = DateTime.Now,
                modifieddate = DateTime.Now,
                Guid = Guid.NewGuid()
            };

            await _unitOfWork.Repository<MemberRegister>().AddAsync(member);
            member.AddDomainEvent(new MemberCreatedEvent(member));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(member.Id, "Member Detail Successfully Created.");
        }
    }
}