using System;
using eMKParty.BackOffice.Support.Application.DTOs;
using eMKParty.BackOffice.Support.Domain.Entities;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface ITokenService
	{
        string CreateToken(MemberRegister user);
        string LoginToken(LoginDto user);
    }
}