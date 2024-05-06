using System;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.LoginMember
{
    public class MemberLoginEvent : BaseEvent
    {
        public LoginDto Member { get; }

        public MemberLoginEvent(LoginDto member)
        {
            Member = member;
        }
    }
}