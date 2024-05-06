using System;
using System.Numerics;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IPlayerRepository
	{
        Task<List<Player>> GetPlayersByClubAsync(int clubId);
    }
}