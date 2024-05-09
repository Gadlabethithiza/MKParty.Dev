using System;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IConfigurationRepository
	{
        Task<Config> GetConfigarationsByTypeAsync(string configType);
        Task<bool> UpdateConfigValue(string configType);
    }
}

