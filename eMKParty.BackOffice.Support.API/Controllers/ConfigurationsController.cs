using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Configurations.Queries;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Queries;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eMKParty.BackOffice.Support.API.Controllers
{
    public class ConfigurationsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //[HttpGet]
        //[Route("FindByType/{config_type}")]
        //public async Task<ActionResult<Result<int>>> GetConfigarationByTypeAsync(string config_type)
        //{
        //    return await _mediator.Send(new GetAllConfigurationsByTypeQuery(config_type));
        //}
    }
}
