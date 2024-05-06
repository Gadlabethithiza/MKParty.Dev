using System;
using System.Numerics;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Provinces.Commands
{
	public class ProvinceCreatedEvent : BaseEvent
	{
        public Province Province { get; }

        public ProvinceCreatedEvent(Province province)
        {
            province = Province;
        }
    }
}