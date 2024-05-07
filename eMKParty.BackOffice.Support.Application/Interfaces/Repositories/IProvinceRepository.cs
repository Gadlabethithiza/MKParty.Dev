using System;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IProvinceRepository
	{
        Task<Province> GetProvinceByidAsync(int Id);
    }
}