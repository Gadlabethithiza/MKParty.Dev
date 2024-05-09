using System;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Configurations.Queries
{
    public class ConfigDto : IMapFrom<Config>
    {
        public int? id { get; set; }
        public string? configType { get; set; }
        public int? configValue { get; set; }
        public string? configDesc { get; set; }
        //public string? creationby { get; set; }
        //public DateTime? creationdate { get; set; }
        //public string? updatedby { get; set; }
        //public DateTime? updateddate { get; set; }
        //public Guid Guid { get; set; }
    }
}

