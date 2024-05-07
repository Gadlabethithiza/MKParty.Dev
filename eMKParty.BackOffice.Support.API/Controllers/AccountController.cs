using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.LoginMember;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eMKParty.BackOffice.Support.API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Result<MemberDto>>> Create(CreateMemberCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Result<UserDto>>> Login(LoginMemberCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}