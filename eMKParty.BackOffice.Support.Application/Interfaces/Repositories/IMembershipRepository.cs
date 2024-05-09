using System;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IMembershipRepository
	{
        Task<List<MemberRegister>> GetMembersByBranchAsync(string branchId);
        Task<List<MemberRegister>> GetMembersByProvinceAsync(string province_id);
        Task<List<MemberRegister>> GetMembersByWardAsync(string wardId);
        Task<Result<string>> ActivateMemberAsync(int Id);
        Task<Result<string>> DeactivateMemberAsync(int Id, string reason);
        Task<List<MemberRegister>> GetMembersByRegionAsync(string region);
        Task<List<MemberRegister>> GetMembersBySubResionAsync(string subresion);

        Task<MemberRegister> GetMembersByUsernameAsync(string username);
    }
}