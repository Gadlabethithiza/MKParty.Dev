﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class Province : BaseAuditableEntity
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