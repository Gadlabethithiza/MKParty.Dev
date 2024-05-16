using System;
using System.ComponentModel.DataAnnotations;

namespace eMKParty.BackOffice.Support.Domain.Models
{
	public class RegisterViewModel
	{
        [Required(ErrorMessage = "First Name(s) is required"), MaxLength(50)]
        public string name { get; set; }

        [Required(ErrorMessage = "Surname is required"), MaxLength(50)]
        public string surname { get; set; }

        [Required(ErrorMessage = "RSA Identity No is required"), MaxLength(13)]
        public string id_no { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Mobile is required"), MaxLength(20)]
        public string mobile { get; set; }

        [Required(ErrorMessage = "Gender is required"), MaxLength(50)]
        public string gender { get; set; }

        [Required(ErrorMessage = "Language is required"), MaxLength(20)]
        public string prefered_lang { get; set; }

        [Required(ErrorMessage = "Residential Address is required"), MaxLength(100)]
        public string building_site_no { get; set; }

        [Required(ErrorMessage = "Suburb/Village is required"), MaxLength(50)]
        public string suburb { get; set; }

        [Required(ErrorMessage = "City is required"), MaxLength(30)]
        public string city { get; set; }

        [Required(ErrorMessage = "Postal Code is required"), MaxLength(5)]
        public string postal_code { get; set; }

        [Required(ErrorMessage = "Race is required"), MaxLength(20)]
        public string race { get; set; }

        [Required(ErrorMessage = "Province is required"), MaxLength(50)]
        public string province_name { get; set; }

        [Required(ErrorMessage = "Municipality is required"), MaxLength(150)]
        public string municipality_name { get; set; }

        //[DataType(DataType.EmailAddress)]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string? email { get; set; }
        public string? branch_name { get; set; }
        public string? ward_name { get; set; }
        public string? employment_status { get; set; }
        public string? occupation { get; set; }
        public string? region { get; set; }
        public string? sub_region { get; set; }
        public string? tel { get; set; }
        public Boolean? mobile_use_whatsapp { get; set; } = true;       
    }
}

