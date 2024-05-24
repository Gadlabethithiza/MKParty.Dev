using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember;
using eMKParty.BackOffice.Support.Application.Features.VotingResults.Commands;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMKParty.BackOffice.Support.API.Controllers
{
    //[Authorize]
    public class VotingResultController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public VotingResultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SendVotingResult")]
        public async Task<ActionResult<Result<VotingResultDto>>> Create(CreateVotingResultCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}