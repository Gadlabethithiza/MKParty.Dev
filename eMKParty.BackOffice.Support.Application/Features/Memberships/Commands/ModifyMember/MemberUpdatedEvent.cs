using System;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.ModifyMember
{
    public class MemberUpdatedEvent : BaseEvent
    {
        public MemberRegister Member { get; }

        public MemberUpdatedEvent(MemberRegister member)
        {
            Member = member;
        }
    }
}