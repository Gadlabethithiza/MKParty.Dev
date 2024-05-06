using System;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly IGenericRepository<Club> _repository;

        public ClubRepository(IGenericRepository<Club> repository)
        {
            _repository = repository;
        }
    }
}