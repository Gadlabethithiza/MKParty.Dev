using System;
using System.Numerics;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class Province : BaseAuditableEntity
	{
        public Province()
        {
            Branches = new List<Branch>();
        }

        public string Name { get; set; }
        //public DateTime? BirthDate { get; set; }
        //public string PhotoUrl { get; set; }
        //public string WebsiteUrl { get; set; }
        //public string FacebookUrl { get; set; }
        //public string TwitterUrl { get; set; }
        //public string YoutubeUrl { get; set; }
        //public string InstagramUrl { get; set; }
        //public int? StadiumId { get; set; }
        //public Stadium Stadium { get; set; }
        public IList<Branch> Branches { get; set; }
    }
}

