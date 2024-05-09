using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Application.Features.Provinces.Queries
{
    public record GetAllProvincesQuery : IRequest<Result<List<ProvinceDto>>>;

    internal class GetAllProvincesQueryHandler : IRequestHandler<GetAllProvincesQuery, Result<List<ProvinceDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProvincesQueryHandler> _logger;

        public GetAllProvincesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllProvincesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<List<ProvinceDto>>> Handle(GetAllProvincesQuery query, CancellationToken cancellationToken)
        {
            var provinces = await _unitOfWork.Repository<Province>().Entities
                    .ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return await Result<List<ProvinceDto>>.SuccessAsync(provinces);
        }
    }
} 