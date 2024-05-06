using System;
using eMKParty.BackOffice.Support.Application.Interfaces;

namespace eMKParty.BackOffice.Support.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}

