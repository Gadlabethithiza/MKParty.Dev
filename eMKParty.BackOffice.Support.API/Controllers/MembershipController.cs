using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.DeactivateMember;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.LoginMember;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.ModifyMember;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Queries;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetAllMembers;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetMembersByBranch;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eMKParty.BackOffice.Support.API.Controllers
{
    [Authorize]
    public class MembershipController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public MembershipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("FindAll")]
        public async Task<ActionResult<Result<List<MemberDto>>>> Get()
        {
            return await _mediator.Send(new GetAllMembersQuery());
        }

        [HttpGet("FindByBranch/{branchId}")]
        public async Task<ActionResult<Result<List<MemberDto>>>> GetMembersByBranch(string branch)
        {
            return await _mediator.Send(new GetAllMembersByBranchQuery(branch));
        }

        [HttpGet]
        [Route("FindByProvince/{provinceId}")]
        public async Task<ActionResult<Result<List<MemberDto>>>> GetMembersByProvince(string province)
        {
            return await _mediator.Send(new GetAllMembersByProvinceQuery(province));
        }

        [HttpGet]
        [Route("FindByWard/{wardId}")]
        public async Task<ActionResult<Result<List<MemberDto>>>> GetMembersByWard(string ward)
        {
            return await _mediator.Send(new GetAllMembersByWardQuery(ward));
        }

        [HttpGet]
        [Route("FindByRegion/{name}")]
        public async Task<ActionResult<Result<List<MemberDto>>>> GetMembersByRegionAsync(string name)
        {
            return await _mediator.Send(new GetMembersByRegionQuery(name));
        }

        [HttpGet]
        [Route("FindBySubregion/{name}")]
        public async Task<ActionResult<Result<List<MemberDto>>>> GetMembersBySubResionAsync(string name)
        {
            return await _mediator.Send(new GetMembersBySubRegionQuery(name));
        }

        //[HttpPost("Register")]
        //public async Task<ActionResult<Result<MemberDto>>> Create(CreateMemberCommand command)
        //{
        //    return await _mediator.Send(command);
        //}

        //[HttpPost("Login")]
        //public async Task<ActionResult<Result<UserDto>>> Login(LoginMemberCommand command)
        //{
        //    return await _mediator.Send(command);
        //}

        [HttpPut("Modify_profile/{id}")]
        public async Task<ActionResult<Result<int>>> Update(int id, UpdateMemberCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return await _mediator.Send(command);
        }

        [HttpDelete("Deactivate_member_profile/{id}")]
        public async Task<ActionResult<Result<int>>> Delete(int id)
        {
            return await _mediator.Send(new DeactivateMemberCommand(id));
        }

        [HttpPost("Activate_member_profile/{id}")]
        public async Task<ActionResult<Result<MemberDto>>> Update(CreateMemberCommand command)
        {
            return await _mediator.Send(command);
        }

        //[HttpGet]
        //[Route("paged")]
        //public async Task<ActionResult<PaginatedResult<GetPlayersWithPaginationDto>>> GetPlayersWithPagination([FromQuery] GetPlayersWithPaginationQuery query)
        //{
        //    var validator = new GetPlayersWithPaginationValidator();

        //    // Call Validate or ValidateAsync and pass the object which needs to be validated
        //    var result = validator.Validate(query);

        //    if (result.IsValid)
        //    {
        //        return await _mediator.Send(query);
        //    }

        //    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        //    return BadRequest(errorMessages);
        //}
    }
}