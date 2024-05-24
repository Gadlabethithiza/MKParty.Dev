using System;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.DTOs
{
	public class IncidentDto : IMapFrom<Incident>
    {
        public string? VDPartCode { get; set; }
        public string? VDUniqueCode { get; set; }
        public string? VDAgentCode { get; set; }
        public string? Incident_Description { get; set; }
        public string? Category { get; set; }
        public string? Severity { get; set; }
        public string? IncStatus { get; set; }
        public string? Resolution_Description { get; set; }
        public Boolean? IsIECRelated { get; set; } = true;
        public string? AssignedTo { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string? EscalatedTo { get; set; }
        public DateTime? EscalatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? VDYear { get; set; }
        public string? creationby { get; set; }
        public DateTime? creationdate { get; set; }
        public string? updatedby { get; set; }
        public DateTime? updateddate { get; set; }
        public Guid Guid { get; set; }
    }
}