using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;

namespace eMKParty.BackOffice.Support.Web.Razor.Pages.BackOffice
{
	public class DashboardModel : PageModel
    {
        private readonly ILogger<DashboardModel> _logger;
        private readonly IMediator _mediator;

        //[BindProperty]
        //public LoginViewModel User { get; set; }

        public DashboardModel(IMediator mediator, ILogger<DashboardModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/BackOffice/Dashboard");
            }

            return Page();
        }
    }
}
