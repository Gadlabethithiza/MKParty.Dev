using System;
using eMKParty.BackOffice.Support.Application.Features.Provinces.Queries;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Domain.Entities.Feedback;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
	public class ProvinceRepository : IProvinceRepository
    {
        //private readonly IGenericRepository<Province> _repository;
        //private readonly ApplicationDbContext _dbContext;

        //public ProvinceRepository(IGenericRepository<Province> repository, ApplicationDbContext dbContext)
        //{
        //    //_repository = repository;
        //    _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        //}


        private readonly ApplicationMySqlDbContext _dbContext;
        public ProvinceRepository(IGenericRepository<Province> repository, ApplicationMySqlDbContext dbContext)
        {
            //_repository = repository;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Province> GetProvinceByidAsync(int Id)
        {

            //var item = _dbContext.Provinces.Where(x => x.Id == Id).SingleOrDefaultAsync();
            //var context = ApplicationDbContext();

            //var item = new GetAllProvinceByIdQuery(Id)

            return await _dbContext.Provinces.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
    }
}