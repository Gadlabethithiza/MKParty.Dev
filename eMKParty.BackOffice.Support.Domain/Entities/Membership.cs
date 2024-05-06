using System;
using System.Diagnostics.Metrics;
using eMKParty.BackOffice.Support.Domain.Common;

namespace eMKParty.BackOffice.Support.Domain.Entities
{
	public class MemberRegister : BaseAuditableEntity
    {
        public int id { get; set; }
        public int? province_id { get; set; }
        public int? branch_id { get; set; }
        public int? ward_id { get; set; }
        public DateTime membership_date { get; set; }
        public string membership_no { get; set; }
        public Boolean? membership_card_required { get; set; }
        public Boolean? membership_card_printed { get; set; }
        public DateTime? BirthDate { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string id_no { get; set; }
        public string gender { get; set; }
        public string prefered_lang { get; set; }
        public string building_site_no { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string mobile { get; set; }
        public Boolean mobile_use_whatsapp { get; set; }
        public Boolean member_in_good_standing { get; set; }
        public string role { get; set; }
        public string username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string security_question { get; set; }
        public string security_answer { get; set; }
        public string createdby { get; set; }
        public DateTime createddate { get; set; }
        public string modifiedby { get; set; }
        public DateTime modifieddate { get; set; }
        public Guid Guid { get; set; }
        //public Branch Branch { get; set; }
        //public Branch_Ward Ward { get; set; }
    }
}