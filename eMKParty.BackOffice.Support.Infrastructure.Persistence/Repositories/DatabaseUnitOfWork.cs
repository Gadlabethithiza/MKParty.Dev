using System;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
	public class DatabaseUnitOfWork : IDatabaseUnitOfWork
    {
        private IGenericRepository<VotingStation> _votingStationRepository;

        public DatabaseUnitOfWork(ApplicationMySqlDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public ApplicationMySqlDbContext DatabaseContext { get; private set; }

        public async Task<bool> Save()
        {
            try
            {
                int _save = await DatabaseContext.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (System.Exception e)
            {
                return await Task.FromResult(false);
            }
        }

        public IGenericRepository<VotingStation> VotingStationRepository
        {
            get
            {
                if (_votingStationRepository == null)
                {
                    _votingStationRepository = new GenericRepository<VotingStation>(DatabaseContext);
                }
                return _votingStationRepository;
            }
        }


    }
}

