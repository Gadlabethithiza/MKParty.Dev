using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Features.Provinces.Queries;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Application.Features.Wards.Queries
{
    public record GetAllWardsQuery : IRequest<Result<List<WardDto>>>;

    internal class GetAllWardsQueryHandler : IRequestHandler<GetAllWardsQuery, Result<List<WardDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllWardsQueryHandler> _logger;

        public GetAllWardsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllWardsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<List<WardDto>>> Handle(GetAllWardsQuery query, CancellationToken cancellationToken)
        {
            var votingstations = await _unitOfWork.Repository<Ward>().Entities
                    .ProjectTo<WardDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return await Result<List<WardDto>>.SuccessAsync(votingstations);
        }
    }
}