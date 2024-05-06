using System;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.DeactivateMember
{
	public class MemberDeletedEvent : BaseEvent
    {
        public MemberRegister Member { get; }

        public MemberDeletedEvent(MemberRegister member)
        {
            Member = member;
        }
    }
}