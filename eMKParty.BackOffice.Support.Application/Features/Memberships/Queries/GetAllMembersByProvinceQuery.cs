using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetMembersByBranch;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Shared;
using MediatR;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Queries
{
	public class GetAllMembersByProvinceQuery : IRequest<Result<List<MemberDto>>>
    {
        public int ProvinceId { get; set; }

        public GetAllMembersByProvinceQuery()
        {

        }

        public GetAllMembersByProvinceQuery(int id)
        {
            ProvinceId = id;
        }
    }

    internal class GetAllMembersByProvinceQueryHandler : IRequestHandler<GetAllMembersByProvinceQuery, Result<List<MemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMembershipRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetAllMembersByProvinceQueryHandler(IUnitOfWork unitOfWork, IMembershipRepository membershipRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _memberRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetAllMembersByProvinceQuery query, CancellationToken cancellationToken)
        {
            var entities = await _memberRepository.GetMembersByProvinceAsync(query.ProvinceId);
            var members = _mapper.Map<List<MemberDto>>(entities);
            return await Result<List<MemberDto>>.SuccessAsync(members);
        }
    }
}