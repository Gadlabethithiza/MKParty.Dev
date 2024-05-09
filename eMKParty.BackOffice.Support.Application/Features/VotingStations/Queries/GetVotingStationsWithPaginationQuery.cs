using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Extensions;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<GetVotingStationsWithPaginationQueryHandler> _logger;

        public GetVotingStationsWithPaginationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetVotingStationsWithPaginationQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<VotingStationDto>> Handle(GetVotingStationsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            PaginatedResult<VotingStationDto> Listing = null;

            try
            {
                //_logger.LogInformation("Start Seri Log is Working");

                Listing = await _unitOfWork.Repository<VotingStation>().Entities
                       .OrderBy(x => x.VotingStationName)
                       .ProjectTo<VotingStationDto>(_mapper.ConfigurationProvider)
                       .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

                //_logger.LogInformation("End Seri Log is Working");

                return Listing;

                //return await _unitOfWork.Repository<VotingStation>().Entities
                //       .OrderBy(x => x.VotingStationName)
                //       .ProjectTo<VotingStationDto>(_mapper.ConfigurationProvider)
                //       .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Listing;
        }
    }
}