using System;
using Microsoft.EntityFrameworkCore;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IDatabaseUnitOfWork
	{
        //DbContext DatabaseContext { get; }
        Task<bool> Save();
        IGenericRepository<VotingStation> VotingStationRepository { get; }
    }
}