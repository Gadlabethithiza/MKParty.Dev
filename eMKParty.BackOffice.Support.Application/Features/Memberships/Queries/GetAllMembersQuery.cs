using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetAllMembers
{
    public record GetAllMembersQuery : IRequest<Result<List<MemberDto>>>;

    internal class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, Result<List<MemberDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<MemberDto>>> Handle(GetAllMembersQuery query, CancellationToken cancellationToken)
        {
            var members = await _unitOfWork.Repository<MemberRegister>().Entities
                   .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

            return await Result<List<MemberDto>>.SuccessAsync(members);
        }
    }
}