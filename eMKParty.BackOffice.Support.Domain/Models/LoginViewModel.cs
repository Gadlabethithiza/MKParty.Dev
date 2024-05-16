using System;
using System.ComponentModel.DataAnnotations;

namespace eMKParty.BackOffice.Support.Domain.Models
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "User Name is required")]
        public string UsernameOrEmail
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        //public bool RememberMe
        //{
        //    get;
        //    set;
        //}
    }
}

