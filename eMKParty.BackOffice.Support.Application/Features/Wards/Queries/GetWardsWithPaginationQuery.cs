using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Extensions;
using eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Application.Features.Wards.Queries
{
    public record GetWardsWithPaginationQuery : IRequest<PaginatedResult<WardDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetWardsWithPaginationQuery() { }

        public GetWardsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetWardsWithPaginationQueryHandler : IRequestHandler<GetWardsWithPaginationQuery, PaginatedResult<WardDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetWardsWithPaginationQueryHandler> _logger;

        public GetWardsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetWardsWithPaginationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResult<WardDto>> Handle(GetWardsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Ward>().Entities
                   .OrderBy(x => x.WardCode)
                   .ProjectTo<WardDto>(_mapper.ConfigurationProvider)
                   .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}