using System;
using eMKParty.BackOffice.Support.Domain.Common.Interfaces;

namespace eMKParty.BackOffice.Support.Domain.Common
{
	public class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}