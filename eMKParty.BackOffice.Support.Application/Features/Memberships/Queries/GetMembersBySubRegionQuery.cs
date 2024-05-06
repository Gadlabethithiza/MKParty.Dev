using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Shared;
using MediatR;

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

        public GetMembersBySubRegionQueryHandler(IUnitOfWork unitOfWork, IMembershipRepository membershipRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _memberRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetMembersBySubRegionQuery query, CancellationToken cancellationToken)
        {
            var entities = await _memberRepository.GetMembersBySubResionAsync(query.SubRegion);
            var members = _mapper.Map<List<MemberDto>>(entities);
            return await Result<List<MemberDto>>.SuccessAsync(members);
        }
    }
}

