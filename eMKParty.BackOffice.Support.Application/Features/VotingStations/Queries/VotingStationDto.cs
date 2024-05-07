using System;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries
{
	public class VotingStationDto : IMapFrom<VotingStation>
    {
        public int VotingStation_ID { get; set; }
        public string? VotingDistrict { get; set; }
        public string? VotingStationName { get; set; }
        public int? FkWard_ID { get; set; }
        public int? FkMunicipality_ID { get; set; }
        public int? FkProvince_ID { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }
        public Guid Guid { get; set; }
    }
}