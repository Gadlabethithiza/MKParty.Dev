using System;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Domain.Entities.Feedback;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IMunicipalityRepository
	{
        Task<List<MunicipalityFeedDto>> GetAllMunicipalitiesAsync();
    }
}