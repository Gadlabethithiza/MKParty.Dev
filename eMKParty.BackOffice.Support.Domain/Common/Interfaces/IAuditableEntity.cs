using System;
namespace eMKParty.BackOffice.Support.Domain.Common.Interfaces
{
	public interface IAuditableEntity : IEntity
	{
        int? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        int? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}