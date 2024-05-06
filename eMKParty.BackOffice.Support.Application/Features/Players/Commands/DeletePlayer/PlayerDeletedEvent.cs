using System;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Features.Players.Commands.DeletePlayer
{
    public class PlayerDeletedEvent : BaseEvent
    {
        public Player Player { get; }

        public PlayerDeletedEvent(Player player)
        {
            Player = player;
        }
    }
}