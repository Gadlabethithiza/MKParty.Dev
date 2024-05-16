using System;
using System.ComponentModel.DataAnnotations;
using eMKParty.BackOffice.Support.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class Municipality : BaseAuditableEntity
    {
        //[Key]
        public int Municipality_ID { get; set; }
        public string? MunicipalityCode { get; set; }
        public string? MunicipalityName { get; set; }

        //[ForeignKey("FkProvince_ID")]
        public int FkProvince_ID { get; set; }
        //public string? ProvinceName { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }
        public Guid Guid { get; set; }
        //public Province Province { get; set; }

        //public Province? Province { get; set; }
        //public int Id { get; set; }
    }
}