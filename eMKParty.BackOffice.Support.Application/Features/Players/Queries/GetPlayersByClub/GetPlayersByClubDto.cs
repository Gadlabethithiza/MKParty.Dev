using System;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Players.Queries.GetPlayersWithPagination
{
    public class GetPlayersByClubDto : IMapFrom<Player>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int ShirtNo { get; init; }
        public int HeightInCm { get; init; }
        public string FacebookUrl { get; init; }
        public string TwitterUrl { get; init; }
        public string InstagramUrl { get; init; }
        public int DisplayOrder { get; init; }
    }
}