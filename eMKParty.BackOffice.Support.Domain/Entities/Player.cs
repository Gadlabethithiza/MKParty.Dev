using System;
using System.Diagnostics.Metrics;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class Player : BaseAuditableEntity
	{
        public string Name { get; set; }
        public int? ShirtNo { get; set; }
        public int? ClubId { get; set; }
        public int? PlayerPositionId { get; set; }
        public string PhotoUrl { get; set; }
        public int? CountryId { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? HeightInCm { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public int? DisplayOrder { get; set; }

        public Club Club { get; set; }
        public Country Country { get; set; }
    }
}