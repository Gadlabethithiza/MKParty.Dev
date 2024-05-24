using System;
using System.Reflection;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using Microsoft.Extensions.Logging;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
	public class VotingResultRepository : IVotingResultRepository
    {
        private readonly IGenericRepository<VotingResult> _repository;

        public VotingResultRepository(IGenericRepository<VotingResult> repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> UpadeVotingResultAsync(VotingResultDto item)
        {
            var votingResult = _repository.Entities.Where(ie => ie.VDPartCode == item.VDPartCode && ie.VDUniqueCode == item.VDUniqueCode && ie.VDYear == DateTime.Now.Year).FirstOrDefault();
            if (votingResult != null)
            {
                votingResult.VDResults = item.VDResults;
                votingResult.updateddate = DateTime.Now;
                votingResult.updatedby = item.updatedby;

                await _repository.UpdateAsync(votingResult);
                return await Result<string>.SuccessAsync(null, "VD Voting Results Updated.");
            }
            else
            {
                return await Result<string>.FailureAsync(null, "VD Voting Results failed to update.");
            }
        }
    }
}