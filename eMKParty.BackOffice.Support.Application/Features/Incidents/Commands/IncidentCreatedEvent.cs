using System;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Incidents.Commands
{
	public class IncidentCreatedEvent : BaseEvent
    {
        public Incident _result { get; }

        public IncidentCreatedEvent(Incident result)
        {
            _result = result;
        }
    }
}