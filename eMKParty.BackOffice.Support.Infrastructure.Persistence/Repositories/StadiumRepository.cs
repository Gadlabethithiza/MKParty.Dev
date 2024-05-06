using System;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
    public class StadiumRepository : IStadiumRepository
    {
        private readonly IGenericRepository<Stadium> _repository;

        public StadiumRepository(IGenericRepository<Stadium> repository)
        {
            _repository = repository;
        }
    }
}