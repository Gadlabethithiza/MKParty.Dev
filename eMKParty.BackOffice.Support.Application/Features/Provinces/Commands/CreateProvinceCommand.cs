using System;
using MediatR;
using System.Numerics;
using eMKParty.BackOffice.Support.Domain.Entities;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Shared;
using eMKParty.BackOffice.Support.Application.Common.Mappings;

namespace eMKParty.BackOffice.Support.Application.Features.Provinces.Commands
{
	public record CreateProvinceCommand : IRequest<Result<int>>, IMapFrom<Province>
    {
        public string Name { get; set; }
        public int ShirtNo { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime BirthDate { get; set; }
    }


    internal class CreatePlayerCommandHandler : IRequestHandler<CreateProvinceCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePlayerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateProvinceCommand command, CancellationToken cancellationToken)
        {
            var player = new Province()
            {
                Name = command.Name
                //ShirtNo = command.ShirtNo,
                //PhotoUrl = command.PhotoUrl,
                //BirthDate = command.BirthDate
            };

            await _unitOfWork.Repository<Province>().AddAsync(player);
            player.AddDomainEvent(new ProvinceCreatedEvent(player));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(player.Id, "Province Created.");
        }
    }
}