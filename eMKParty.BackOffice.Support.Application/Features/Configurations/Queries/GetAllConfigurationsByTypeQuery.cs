using System;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Configurations.Queries;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Queries;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Application.Features.Configurations.Queries
{
    public class GetAllConfigurationsByTypeQuery : IRequest<Result<int>>
    {
        public string ConfigType { get; set; }

        public GetAllConfigurationsByTypeQuery()
        {

        }

        public GetAllConfigurationsByTypeQuery(string configType)
        {
            ConfigType = configType;
        }
    }
}

internal class GetAllConfigurationsByTypeQueryHandler : IRequestHandler<GetAllConfigurationsByTypeQuery, Result<int>>
{
    private readonly IConfigurationRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllConfigurationsByTypeQueryHandler> _logger;

    public GetAllConfigurationsByTypeQueryHandler(IConfigurationRepository repository, IMapper mapper, ILogger<GetAllConfigurationsByTypeQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(GetAllConfigurationsByTypeQuery query, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetConfigarationsByTypeAsync(query.ConfigType);
        var config = _mapper.Map<ConfigDto>(entities).configValue;

        //Update
        await _repository.UpdateConfigValue(query.ConfigType);

        return await Result<int>.SuccessAsync(config.ToString());
    }
}

