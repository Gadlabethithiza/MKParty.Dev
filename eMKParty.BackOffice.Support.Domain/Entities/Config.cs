using System;
using System.ComponentModel.DataAnnotations;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
    public class Config : BaseAuditableEntity
    {
        //[Key]
        //public int? id { get; set; }
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