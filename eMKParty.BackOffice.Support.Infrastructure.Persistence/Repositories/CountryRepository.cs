using System;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IGenericRepository<Country> _repository;

        public CountryRepository(IGenericRepository<Country> repository)
        {
            _repository = repository;
        }
    }
}