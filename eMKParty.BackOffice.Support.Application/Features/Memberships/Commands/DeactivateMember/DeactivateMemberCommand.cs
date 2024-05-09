using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.ModifyMember;
using eMKParty.BackOffice.Support.Application.Features.Players.Commands.DeletePlayer;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.DeactivateMember
{
	public class DeactivateMemberCommand : IRequest<Result<int>>, IMapFrom<MemberRegister>
    {
        public int Id { get; set; }

        public DeactivateMemberCommand()
        {

        }

        public DeactivateMemberCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeactivateMemberCommandHandler : IRequestHandler<DeactivateMemberCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeactivateMemberCommandHandler> _logger;

        public DeactivateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeactivateMemberCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(DeactivateMemberCommand command, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.Repository<MemberRegister>().GetByIdAsync(command.Id);
            if (member != null)
            {
                await _unitOfWork.Repository<MemberRegister>().DeleteAsync(member);
                member.AddDomainEvent(new MemberDeletedEvent(member));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(member.Id, "Member Deactivated");
            }
            else
            {
                return await Result<int>.FailureAsync("Member Not Found.");
            }
        }
    }
}