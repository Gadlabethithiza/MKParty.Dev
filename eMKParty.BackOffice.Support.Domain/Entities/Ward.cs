using System;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class Ward : BaseAuditableEntity
    {
        public int Ward_ID { get; set; }
        public string? WardCode { get; set; }
        public int? FkMunicipality_ID { get; set; }
        public int? FkProvince_ID { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }
        public Guid Guid { get; set; }
    }
}