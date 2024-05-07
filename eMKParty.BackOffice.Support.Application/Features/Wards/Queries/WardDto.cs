using System;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Wards.Queries
{
	public class WardDto : IMapFrom<Ward>
    {
        public int Ward_ID { get; set; }
        public string? WardCode { get; set; }
        public string? FkMunicipality_ID { get; set; }
        public int? FkProvince_ID { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }
        public Guid Guid { get; set; }
    }
}