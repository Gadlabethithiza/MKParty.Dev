using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Shared;
using MediatR;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Queries
{
	public class GetAllMembersByWardQuery : IRequest<Result<List<MemberDto>>>
    {
        public int WardId { get; set; }

        public GetAllMembersByWardQuery()
        {

        }

        public GetAllMembersByWardQuery(int id)
        {
            WardId = id;
        }
    }

    internal class GetAllMembersByWardQueryHandler : IRequestHandler<GetAllMembersByWardQuery, Result<List<MemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMembershipRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetAllMembersByWardQueryHandler(IUnitOfWork unitOfWork, IMembershipRepository membershipRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _memberRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetAllMembersByWardQuery query, CancellationToken cancellationToken)
        {
            var entities = await _memberRepository.GetMembersByWardAsync(query.WardId);
            var members = _mapper.Map<List<MemberDto>>(entities);
            return await Result<List<MemberDto>>.SuccessAsync(members);
        }
    }
}