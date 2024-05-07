using System;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Provinces.Queries
{
    public class ProvinceDto : IMapFrom<Province>
    {
        public int Province_ID { get; set; }
        public string? ProvinceCode { get; set; }
        public string? ProvinceDesc { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }
        public Guid Guid { get; set; }
    }
}