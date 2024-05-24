using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eMKParty.BackOffice.Support.Web.Razor.Pages.Account
{
	public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");

            //var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            //.WithRedirectUri("/")
            //.Build();

            //await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
