using System;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Shared;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IVotingResultRepository
	{
        Task<Result<string>> UpadeVotingResultAsync(VotingResultDto item);
    }
}