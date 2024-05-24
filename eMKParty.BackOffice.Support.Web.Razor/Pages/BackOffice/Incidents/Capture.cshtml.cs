using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.Features.Incidents.Commands;
using eMKParty.BackOffice.Support.Application.Features.VotingResults.Commands;
using eMKParty.BackOffice.Support.Web.Razor.Pages.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eMKParty.BackOffice.Support.Web.Razor.Pages.BackOffice.Incidents
{
	public class CaptureModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IMediator _mediator;

        public SelectList PoliticalPartiesSL { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        public string ReturnUrl { get; set; }

        [BindProperty]
        public string? VDPartyCode { get; set; }

        [BindProperty, Required]
        public string VDUniqueCode { get; set; }

        [BindProperty, Required]
        public string VDAgentCode { get; set; }

        [BindProperty, Required]
        public string Incident_Description { get; set; }

        [BindProperty]
        public string? Category { get; set; }

        [BindProperty]
        public string? Severity { get; set; }

        [BindProperty]
        public string IncStatus { get; set; }

        [BindProperty]
        public bool isIECRelated { get; set; } = false;

        [BindProperty]
        public string? Resolution_Description { get; set; }

        public CaptureModel(IMediator mediator, ILogger<LoginModel> logger)
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
            ReturnUrl = returnUrl ?? Url.Content("/BackOffice/Incidents/Index");

            if (ModelState.IsValid)
            {
                IncidentResultCommand command = new IncidentResultCommand();
                command.VDPartCode = this.VDPartyCode;
                command.VDUniqueCode = this.VDUniqueCode;
                command.VDAgentCode = this.VDAgentCode;
                command.Incident_Description = this.Incident_Description;
                command.Category = this.Category;
                command.Severity = this.Severity;
                command.IncStatus = this.IncStatus;
                command.Resolution_Description = this.Resolution_Description;
                command.IsIECRelated = this.isIECRelated;

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
