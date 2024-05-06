using System;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;

namespace eMKParty.BackOffice.Support.Application.Interfaces.Repositories
{
	public interface IMembershipRepository
	{
        Task<List<MemberRegister>> GetMembersByBranchAsync(int branchId);
        Task<List<MemberRegister>> GetMembersByProvinceAsync(int province_id);
        Task<List<MemberRegister>> GetMembersByWardAsync(int wardId);
        Task<Result<string>> ActivateMemberAsync(int Id);
        Task<Result<string>> DeactivateMemberAsync(int Id, string reason);
        Task<List<MemberRegister>> GetMembersByRegionAsync(string region);
        Task<List<MemberRegister>> GetMembersBySubResionAsync(string subresion);
    }
}