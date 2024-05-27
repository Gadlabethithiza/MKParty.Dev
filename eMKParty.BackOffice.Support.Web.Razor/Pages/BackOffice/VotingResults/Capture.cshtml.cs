using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.LoginMember;
using eMKParty.BackOffice.Support.Application.Features.VotingResults.Commands;
using eMKParty.BackOffice.Support.Web.Razor.Pages.Account;
using eMKParty.BackOffice.Support.Web.Razor.Pages.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eMKParty.BackOffice.Support.Web.Razor.Pages.BackOffice.VotingResults
{
	public class CaptureModel : SharedPageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IMediator _mediator;

        public SelectList PoliticalPartiesSL { get; set; }


        [TempData]
        public string ErrorMessage { get; set; }
        public string ReturnUrl { get; set; }

        [BindProperty]
        public string VDPartyCode { get; set; }

        [BindProperty, Required]
        public string VDUniqueCode { get; set; }

        //[RegularExpression("([1-9][0-9]*)")] 
        [BindProperty, Required]
        //[DataType(DataType.Currency)]
        //[Range(0, Int32.MaxValue, ErrorMessage = "VD Results should not contain characters")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "VD Results must be numeric")]
        public int VDResults { get; set; }

        [BindProperty, Required]
        public string VDAgentCode { get; set; }

        public CaptureModel(IMediator mediator, ILogger<LoginModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        //public async void OnGet()
        //{
        //    this.PoliticalPartiesSL = new SelectList(PoliticalParties(), "Value", "Value");
        //}

        public void OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            //if (User.Identity.IsAuthenticated)
            //{
            //    returnUrl = returnUrl ?? Url.Content("/BackOffice/VotingResults/Capture");
            //}
            //else
            //{
            //    returnUrl = returnUrl ?? Url.Content("/BackOffice/VotingResults/Capture");
            //}

            this.PoliticalPartiesSL = new SelectList(PoliticalParties(), "Value", "Value");

            returnUrl = returnUrl ?? Url.Content("~/");
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("/BackOffice/VotingResults/Index");

            if (ModelState.IsValid)
            {
                CreateVotingResultCommand command = new CreateVotingResultCommand();
                command.VDPartCode = this.VDPartyCode;
                command.VDUniqueCode = this.VDUniqueCode;
                command.VDAgentCode = this.VDAgentCode;
                command.VDResults = this.VDResults.ToString();
                var returnVal = await _mediator.Send(command);
                return RedirectToPage(ReturnUrl);
            }

            this.PoliticalPartiesSL = new SelectList(PoliticalParties(), "Value", "Value");


            // If we got this far, something failed, redisplay form
            return Page();
        }


        public static List<SelectListItem> PoliticalParties()
        {
            var LS = new List<string> { "ANC", "IFP", "EFF", "DA", "ACA", "ACDP", "APC", "FF+", "HNP", "MF",
                "SACP", "UCDP", "Action SA", "UDM", "ATM", "GOOD", "NFP", "AIC", "COPE", "PAC", "ALJAMA",
                "RISE","Other" };
            var ls = LS.Select(s => new SelectListItem { Value = s })
                .ToList();
            return ls;
        }

    }
}
