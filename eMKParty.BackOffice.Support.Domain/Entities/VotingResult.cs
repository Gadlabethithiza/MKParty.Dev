using System;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class VotingResult : BaseAuditableEntity
    {
        //public int Id { get; set; }
        public string? VDPartCode { get; set; }
        public string? VDUniqueCode { get; set; }
        public int? VDResults { get; set; }
        public string? VDAgentCode { get; set; }
        public int? VDYear { get; set; }
        public string? creationby { get; set; }
        public DateTime? creationdate { get; set; }
        public string? updatedby { get; set; }
        public DateTime? updateddate { get; set; }
        public Guid Guid { get; set; }
    }
}