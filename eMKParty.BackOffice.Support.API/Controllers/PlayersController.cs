﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.Features.Players.Commands.CreatePlayer;
using eMKParty.BackOffice.Support.Application.Features.Players.Commands.DeletePlayer;
using eMKParty.BackOffice.Support.Application.Features.Players.Commands.UpdatePlayer;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eMKParty.BackOffice.Support.API.Controllers
{
    public class PlayersController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<ActionResult<Result<List<GetAllPlayersDto>>>> Get()
        //{
        //    return await _mediator.Send(new GetAllPlayersQuery());
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Result<GetPlayerByIdDto>>> GetPlayersById(int id)
        //{
        //    return await _mediator.Send(new GetPlayerByIdQuery(id));
        //}

        //[HttpGet]
        //[Route("club/{clubId}")]
        //public async Task<ActionResult<Result<List<GetPlayersByClubDto>>>> GetPlayersByClub(int clubId)
        //{
        //    return await _mediator.Send(new GetPlayersByClubQuery(clubId));
        //}

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

        //[HttpPost]
        //public async Task<ActionResult<Result<int>>> Create(CreatePlayerCommand command)
        //{
        //    return await _mediator.Send(command);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Result<int>>> Update(int id, UpdatePlayerCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    return await _mediator.Send(command);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Result<int>>> Delete(int id)
        //{
        //    return await _mediator.Send(new DeletePlayerCommand(id));
        //}
    }
}