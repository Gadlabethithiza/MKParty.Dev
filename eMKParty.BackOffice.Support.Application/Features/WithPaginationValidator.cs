using System;
using eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination;
using eMKParty.BackOffice.Support.Application.Features.VotingStations.Queries;
using eMKParty.BackOffice.Support.Application.Features.Wards.Queries;
using FluentValidation;

namespace eMKParty.BackOffice.Support.Application.Features
{
    public class GetWardsWithPaginationValidator : AbstractValidator<GetWardsWithPaginationQuery>
    {
        public GetWardsWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");
        }
    }


    public class GetVotingStationsWithPaginationValidator : AbstractValidator<GetVotingStationsWithPaginationQuery>
    {
        public GetVotingStationsWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}