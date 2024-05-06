using System;
using System.ComponentModel.DataAnnotations;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Application.DTOs
{
	public class LoginDto : BaseAuditableEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}