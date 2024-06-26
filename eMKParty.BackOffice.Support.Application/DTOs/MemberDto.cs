﻿using System;
using eMKParty.BackOffice.Support.Application.Common.Mappings;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.DTOs
{
    public class MemberDto : IMapFrom<MemberRegister>
    {
        public string? province_name { get; set; }
        public string? branch_name { get; set; }
        public string? ward_name { get; set; }
        public string? municipality_name { get; set; }
        public DateTime? membership_date { get; set; }
        public string? membership_no { get; set; }
        public Boolean? membership_card_required { get; set; } = true;
        public Boolean? membership_card_printed { get; set; } = false;
        public DateTime? BirthDate { get; set; }

        public string name { get; set; }
        public string surname { get; set; }
        public string id_no { get; set; }
        public string? email { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public string prefered_lang { get; set; }
        public string building_site_no { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string race { get; set; }

        public string? employment_status { get; set; }
        public string? occupation { get; set; }
        public string? region { get; set; }
        public string? sub_region { get; set; }
        public string? tel { get; set; }
        public Boolean? mobile_use_whatsapp { get; set; } = true;
        public Boolean? member_in_good_standing { get; set; } = true;
        public Boolean? elections_agent { get; set; } = false;
        public string? role { get; set; }
        public string? username { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? security_question { get; set; }
        public string? security_answer { get; set; }
        public string? creationby { get; set; }
        public DateTime? creationdate { get; set; }
        public string? updatedby { get; set; }
        public DateTime? updateddate { get; set; }
        public Guid Guid { get; set; }
        public string Token { get; set; }
    }

    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public MemberDto MemberDetail { get; set; }
    }
}