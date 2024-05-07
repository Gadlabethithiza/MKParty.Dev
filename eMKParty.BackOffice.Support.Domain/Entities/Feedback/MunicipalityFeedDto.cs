using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMKParty.BackOffice.Support.Domain.Entities.Feedback
{
	public class MunicipalityFeedDto
	{
        public int Municipality_ID { get; set; }
        public string? MunicipalityCode { get; set; }
        public string? MunicipalityName { get; set; }

        //[ForeignKey("FkProvince_ID")]
        public int? FkProvince_ID { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }
        public Guid Guid { get; set; }

        public Province? Province { get; set; }

        //public Province Province
        //{
        //    get { return new ErrorEntryList(this.EntryID, "MetaData_Received").Listing; }
        //    set { }
        //}
    }
}

