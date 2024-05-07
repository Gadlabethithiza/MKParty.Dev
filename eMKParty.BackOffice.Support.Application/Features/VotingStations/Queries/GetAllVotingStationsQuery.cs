using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries
{
    public record GetAllVotingStationsQuery : IRequest<Result<List<VotingStationDto>>>;

    internal class GetAllVotingStationsQueryHandler : IRequestHandler<GetAllVotingStationsQuery, Result<List<VotingStationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVotingStationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<VotingStationDto>>> Handle(GetAllVotingStationsQuery query, CancellationToken cancellationToken)
        {
            var votingstations = await _unitOfWork.Repository<VotingStation>().Entities
                    .ProjectTo<VotingStationDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return await Result<List<VotingStationDto>>.SuccessAsync(votingstations);
        }
    }
}