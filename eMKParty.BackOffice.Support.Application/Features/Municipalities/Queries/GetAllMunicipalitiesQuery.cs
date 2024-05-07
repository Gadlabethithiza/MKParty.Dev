using System;
using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Features.Provinces.Queries;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Domain.Entities.Feedback;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Application.Features.Municipalities.Queries
{
    public record GetAllMunicipalitiesQuery : IRequest<Result<List<MunicipalityFeedDto>>>;

    internal class GetAllMunicipalitiesQueryHandler : IRequestHandler<GetAllMunicipalitiesQuery, Result<List<MunicipalityFeedDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProvinceRepository _repository;
        //private readonly IMunicipalityRepository _mrepository;

        public GetAllMunicipalitiesQueryHandler(IUnitOfWork unitOfWork, IProvinceRepository repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<MunicipalityFeedDto>>> Handle(GetAllMunicipalitiesQuery query, CancellationToken cancellationToken)
        {
            //var entities = await _repository.GetAllMunicipalitiesAsync();


            //var provinces = await _unitOfWork.Repository<Province>().Entities
            //       .ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
            //       .ToListAsync(cancellationToken);

            //.GetByIdAsync(query.Id);

            //var municipalities1 = await _unitOfWork.Repository<Municipality>().Entities
            //    .Select(x => new 
            //    {

            //        x,
            //        x.Province = x.Province
            //    });

            var municipalities = await _unitOfWork.Repository<Municipality>().Entities
                    .ProjectTo<MunicipalityDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            List<MunicipalityFeedDto> munipals = new List<MunicipalityFeedDto>();

            if (municipalities.Count > 0)
            {
                foreach (var item in municipalities)
                {
                    MunicipalityFeedDto citem = new MunicipalityFeedDto();
                    citem.Municipality_ID = item.Municipality_ID;
                    citem.MunicipalityCode = item.MunicipalityCode;
                    citem.MunicipalityName = item.MunicipalityName;
                    citem.FkProvince_ID = item.FkProvince_ID;
                    citem.createdby = item.createdby;
                    citem.createddate = item.createddate;
                    citem.modifiedby = item.modifiedby;
                    citem.modifieddate = item.modifieddate;
                    citem.Guid = item.Guid;
                    //citem.Province = item.pr
                    //citem.Province = await _repository.GetProvinceByidAsync(item.FkProvince_ID);
                    munipals.Add(citem);
                }
            }

            return await Result<List<MunicipalityFeedDto>>.SuccessAsync(munipals);
        }
    }
}