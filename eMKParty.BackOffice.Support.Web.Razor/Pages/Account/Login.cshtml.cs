using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.LoginMember;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Models;
using eMKParty.BackOffice.Support.Shared;
using eMKParty.BackOffice.Support.Web.Razor.Pages.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace eMKParty.BackOffice.Support.Web.Razor.Pages.Account
{
	public class LoginModel : SharedPageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IMediator _mediator;

        //[BindProperty]
        //public LoginViewModel User { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        public string ReturnUrl { get; set; }
        [BindProperty, Required]
        public string Username { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel(IMediator mediator, ILogger<LoginModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public void OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            if (User.Identity.IsAuthenticated)
            {
                returnUrl = returnUrl ?? Url.Content("/BackOffice/Dashboard");
                //return RedirectToPage(returnUrl);
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;
        }

        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    if (ModelState.IsValid)
        //    {
        //        var verificationResult = true; // TODO: Verify username and password

        //        if (verificationResult)
        //        {
        //            var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, Username)
        //        };
        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        //            return Redirect(returnUrl);
        //        }

        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return Page();
        //}

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("/BackOffice/Dashboard");

            if (ModelState.IsValid)
            {
                LoginMemberCommand command = new LoginMemberCommand();
                command.Username = this.Username;
                command.Password = this.Password;

                var returnVal = await _mediator.Send(command);

                if (returnVal.Messages != null && !returnVal.Messages.Contains("Success"))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, returnVal.Data.Username),
                        new Claim("FullName", returnVal.Data.MemberDetail.name + " " + returnVal.Data.MemberDetail.surname),
                        new Claim("EmailAddress", returnVal.Data.MemberDetail.email),
                        new Claim("Mobile", returnVal.Data.MemberDetail.mobile),
                        new Claim(ClaimTypes.Role, returnVal.Data.MemberDetail.role)
                    };

                    ViewData["FullName"] = string.Concat(new string[] { returnVal.Data.MemberDetail.name, " ", returnVal.Data.MemberDetail.surname });
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                    return RedirectToPage(ReturnUrl);
                }

                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        //public async Task<ActionResult<Result<UserDto>>> Login(LoginMemberCommand command)
        //{
        //    return await _mediator.Send(command);
        //}

        //public async Task<ActionResult> OnPost()
        //{
        //    //command.Username = User.Username;
        //    //command.Password = User.Password;

        //    //var d = new LoginMemberCommand(command);

        //    //return await _mediator.Send(command);


        //    using (var httpClient = new HttpClient())
        //    {
        //        using (HttpResponseMessage response = await httpClient.GetAsync("http://102.211.28.103/api/Account/LoginUser"))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            var incidents = JsonConvert.DeserializeObject<UserDto>>(apiResponse);
        //        }
        //    }

        //    return new JsonResult(incidents);
        //}

    }
}