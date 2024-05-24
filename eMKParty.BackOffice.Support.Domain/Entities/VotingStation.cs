using System;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class VotingStation : BaseAuditableEntity
    {
        public int VotingStation_ID { get; set; }
        public string? VotingDistrict { get; set; }
        public string? VotingStationName { get; set; }
        public int? FkWard_ID { get; set; }
        public int? FkMunicipality_ID { get; set; }
        public int? FkProvince_ID { get; set; }
        public string? StationAddress { get; set; }
        public string? VDUniqueCode { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }
        public Guid Guid { get; set; }
    }
}