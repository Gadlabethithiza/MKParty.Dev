using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.Logging;


namespace eMKParty.BackOffice.Support.Application.Features.Provinces.Queries
{
    public class GetAllProvinceByIdQuery : IRequest<Result<ProvinceDto>>
    {
        public int Id { get; set; }

        public GetAllProvinceByIdQuery()
        {

        }

        public GetAllProvinceByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetAllProvinceByIdQueryHandler : IRequestHandler<GetAllProvinceByIdQuery, Result<ProvinceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAesOperation _securityService;
        private readonly IConfiguration config;
        private readonly ILogger<GetAllProvinceByIdQueryHandler> _logger;

        public GetAllProvinceByIdQueryHandler(IUnitOfWork unitOfWork, IConfiguration _config, IAesOperation securityService, IMapper mapper, ILogger<GetAllProvinceByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            config = _config;
            _securityService = securityService;
            _logger = logger;
        }

        public async Task<Result<ProvinceDto>> Handle(GetAllProvinceByIdQuery query, CancellationToken cancellationToken)
        {
            var provinces = await _unitOfWork.Repository<Province>().Entities
                    .ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            var province = provinces.Where(x => x.Id == query.Id).SingleOrDefault();
            return await Result<ProvinceDto>.SuccessAsync(province);
        }
    }
}