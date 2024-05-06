using System;
using eMKParty.BackOffice.Support.Application.DTOs;

namespace eMKParty.BackOffice.Support.Application.Interfaces
{
	public interface IEmailService
	{
        Task SendAsync(EmailRequestDto request);
    }
}

