using System;
using System.Collections.Generic;
using System.Linq;
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

namespace eMKParty.BackOffice.Support.Web.Razor.Pages.Account
{
	public class LoginUserModel : SharedPageModel
    {
        private readonly ILogger<LoginUserModel> _logger;
        private readonly IMediator _mediator;
        private static readonly String conn = "https://localhost:52189/api/incident/";

        [BindProperty]
        public LoginViewModel User { get; set; }

        public LoginUserModel(IMediator mediator, ILogger<LoginUserModel> logger, ITokenService tokenService)
        {
            _mediator = mediator;
            _logger = logger;
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