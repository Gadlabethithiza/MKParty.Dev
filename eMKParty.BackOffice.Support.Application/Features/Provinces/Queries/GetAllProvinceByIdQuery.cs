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
using Microsoft.Extensions.Configuration;

namespace eMKParty.BackOffice.Support.Application.Features.Provinces.Queries
{
    public class GetAllProvinceByIdQuery : IRequest<Result<ProvinceDto>>
    {
        public int provinceId { get; set; }

        public GetAllProvinceByIdQuery()
        {

        }

        public GetAllProvinceByIdQuery(int id)
        {
            provinceId = id;
        }
    }

    internal class GetAllProvinceByIdQueryHandler : IRequestHandler<GetAllProvinceByIdQuery, Result<ProvinceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IProvinceRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IAesOperation _securityService;
        private readonly IConfiguration config;

        public GetAllProvinceByIdQueryHandler(IUnitOfWork unitOfWork, IProvinceRepository membershipRepository, IConfiguration _config, IAesOperation securityService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_memberRepository = membershipRepository;
            _mapper = mapper;
            config = _config;
            _securityService = securityService;
        }

        public async Task<Result<ProvinceDto>> Handle(GetAllProvinceByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Province>().GetByIdAsync(query.provinceId);
            var province = _mapper.Map<ProvinceDto>(entity);
            return await Result<ProvinceDto>.SuccessAsync(province);


            //var votingstations = await _unitOfWork.Repository<Province>().GetByIdAsync(query.provinceId);
            //        .ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
            //        .ToListAsync(cancellationToken);
        }
    }
}