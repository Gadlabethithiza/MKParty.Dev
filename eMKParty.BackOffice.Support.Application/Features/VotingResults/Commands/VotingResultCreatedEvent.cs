using System;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.VotingResults.Commands
{
	public class VotingResultCreatedEvent : BaseEvent
    {
        public VotingResult _result { get; }

        public VotingResultCreatedEvent(VotingResult result)
        {
            _result = result;
        }
    }
}