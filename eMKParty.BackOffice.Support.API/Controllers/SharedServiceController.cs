using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Application.Features;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetAllMembers;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Queries.GetMembersByBranch;
using eMKParty.BackOffice.Support.Application.Features.Municipalities.Queries;
using eMKParty.BackOffice.Support.Application.Features.Provinces.Queries;
using eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries;
using eMKParty.BackOffice.Support.Application.Features.Wards.Queries;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Domain.Entities.Feedback;
using eMKParty.BackOffice.Support.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eMKParty.BackOffice.Support.API.Controllers
{
    public class SharedServicesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public SharedServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Municipalities")]
        public async Task<ActionResult<Result<List<MunicipalityFeedDto>>>> GetMunicipalities()
        {
            return await _mediator.Send(new GetAllMunicipalitiesQuery());
        }

        [HttpGet("Provinces")]
        public async Task<ActionResult<Result<List<ProvinceDto>>>> GetProvinces()
        {
            return await _mediator.Send(new GetAllProvincesQuery());
        }

        [HttpGet("Provinces/{Id}")]
        public async Task<ActionResult<Result<ProvinceDto>>> GetProvinceById(int Id)
        {
            return await _mediator.Send(new GetAllProvinceByIdQuery(Id));
        }

        //[HttpGet("Wards")]
        //public async Task<ActionResult<Result<List<WardDto>>>> GetWards()
        //{
        //    return await _mediator.Send(new GetAllWardsQuery());
        //}

        //[HttpGet("VotingStations")]
        //public async Task<ActionResult<Result<List<VotingStationDto>>>> GetVotingStations()
        //{
        //    return await _mediator.Send(new GetAllVotingStationsQuery());
        //}


        [HttpGet]
        [Route("VotingStations")]
        public async Task<ActionResult<PaginatedResult<VotingStationDto>>> GetVotingStationsWithPagination([FromQuery] GetVotingStationsWithPaginationQuery query)
        {
            var validator = new GetVotingStationsWithPaginationValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = validator.Validate(query);

            if (result.IsValid)
            {
                return await _mediator.Send(query);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        [HttpGet]
        [Route("Wards")]
        public async Task<ActionResult<PaginatedResult<WardDto>>> GetWardsWithPagination([FromQuery] GetWardsWithPaginationQuery query)
        {
            var validator = new GetWardsWithPaginationValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = validator.Validate(query);

            if (result.IsValid)
            {
                return await _mediator.Send(query);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }
    }
}