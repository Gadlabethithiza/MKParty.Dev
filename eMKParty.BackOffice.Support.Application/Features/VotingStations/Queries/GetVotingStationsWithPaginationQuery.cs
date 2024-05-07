using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Extensions;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;

namespace eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries
{
    public record GetVotingStationsWithPaginationQuery : IRequest<PaginatedResult<VotingStationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetVotingStationsWithPaginationQuery() { }

        public GetVotingStationsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetVotingStationsWithPaginationQueryHandler : IRequestHandler<GetVotingStationsWithPaginationQuery, PaginatedResult<VotingStationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVotingStationsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<VotingStationDto>> Handle(GetVotingStationsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<VotingStation>().Entities
                   .OrderBy(x => x.VotingStationName)
                   .ProjectTo<VotingStationDto>(_mapper.ConfigurationProvider)
                   .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}