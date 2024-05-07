using System;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Domain.Entities.Feedback;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
	public class ProvinceRepository : IProvinceRepository
    {
        private readonly IGenericRepository<Province> _repository;

        public ProvinceRepository(IGenericRepository<Province> repository)
        {
            _repository = repository;
        }

        public async Task<Province> GetProvinceByidAsync(int Id)
        {
            return await _repository.Entities.Where(x => x.Province_ID == Id).SingleOrDefaultAsync();
        }
    }
}