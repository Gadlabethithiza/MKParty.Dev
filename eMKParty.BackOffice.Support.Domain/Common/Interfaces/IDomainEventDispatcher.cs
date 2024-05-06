using System;
namespace eMKParty.BackOffice.Support.Domain.Common.Interfaces
{
	public interface IDomainEventDispatcher
	{
        Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents);
    }
}