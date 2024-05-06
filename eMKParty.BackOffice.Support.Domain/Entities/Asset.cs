using System;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class Asset : BaseAuditableEntity
    {
        public int id { get; set; }
        public int? supplier_id { get; set; } = 0;
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? warrant_start_date { get; set; } = DateTime.Now;
        public string category { get; set; }
        public string condition { get; set; }
        public Boolean date_acquired { get; set; }
        public float? current_value { get; set; }
    }
}

