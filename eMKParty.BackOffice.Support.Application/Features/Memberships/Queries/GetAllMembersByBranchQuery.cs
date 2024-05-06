using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetMembersByBranch
{
	public class GetAllMembersByBranchQuery : IRequest<Result<List<MemberDto>>>
    {
        public int branch_id { get; set; }

        public GetAllMembersByBranchQuery()
        {

        }

        public GetAllMembersByBranchQuery(int id)
        {
            branch_id = id;
        }
    }

    internal class GetAllMembersByBranchQueryHandler : IRequestHandler<GetAllMembersByBranchQuery, Result<List<MemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMembershipRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetAllMembersByBranchQueryHandler(IUnitOfWork unitOfWork, IMembershipRepository membershipRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _memberRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetAllMembersByBranchQuery query, CancellationToken cancellationToken)
        {
            var entities = await _memberRepository.GetMembersByBranchAsync(query.branch_id);
            var members = _mapper.Map<List<MemberDto>>(entities);
            return await Result<List<MemberDto>>.SuccessAsync(members);
        }
    }
}