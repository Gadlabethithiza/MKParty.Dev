using System;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.RegisterMember
{
	public class MemberCreatedEvent : BaseEvent
    {
        public MemberRegister Member { get; }

        public MemberCreatedEvent(MemberRegister member)
        {
            Member = member;
        }
    }
}