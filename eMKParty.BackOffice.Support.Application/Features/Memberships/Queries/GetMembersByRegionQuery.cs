using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Shared;
using MediatR;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Queries
{
	public class GetMembersByRegionQuery : IRequest<Result<List<MemberDto>>>
    {
        public string Region { get; set; }

        public GetMembersByRegionQuery()
        {

        }

        public GetMembersByRegionQuery(string region)
        {
            Region = region;
        }
    }

    internal class GetMembersByRegionQueryHandler : IRequestHandler<GetMembersByRegionQuery, Result<List<MemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMembershipRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMembersByRegionQueryHandler(IUnitOfWork unitOfWork, IMembershipRepository membershipRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _memberRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetMembersByRegionQuery query, CancellationToken cancellationToken)
        {
            var entities = await _memberRepository.GetMembersByRegionAsync(query.Region);
            var members = _mapper.Map<List<MemberDto>>(entities);
            return await Result<List<MemberDto>>.SuccessAsync(members);
        }
    }
}