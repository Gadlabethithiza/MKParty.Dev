using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class Province : BaseAuditableEntity
	{
        [Key]
        //[Column("Province_ID", TypeName = "int")]
        public int? Id { get; set; }
        public string? ProvinceCode { get; set; }
        public string? ProvinceDesc { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }
        public Guid Guid { get; set; }
    }
}