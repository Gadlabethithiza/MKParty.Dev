using System;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.ModifyMember;
using System.Threading;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Shared;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;
using eMKParty.BackOffice.Support.Application.DTOs;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
	public class MembershipRepository : IMembershipRepository
    {
        private readonly IGenericRepository<MemberRegister> _repository;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;

        //public MembershipRepository(IUnitOfWork unitOfWork, IGenericRepository<MemberRegister> repository, IMapper mapper)
        //{
        //    _unitOfWork = unitOfWork;
        //    _repository = repository;
        //    _mapper = mapper;
        //}

        public MembershipRepository(IGenericRepository<MemberRegister> repository)
        {
            _repository = repository;
        }

        public async Task<List<MemberRegister>> GetMembersByBranchAsync(int branchId)
        {
            return await _repository.Entities.Where(x => x.branch_id == branchId).ToListAsync();
        }

        public async Task<List<MemberRegister>> GetMembersByProvinceAsync(int province_id)
        {
            return await _repository.Entities.Where(x => x.province_id == province_id).ToListAsync();
        }

        public async Task<List<MemberRegister>> GetMembersByWardAsync(int ward_id)
        {
            return await _repository.Entities.Where(x => x.ward_id == ward_id).ToListAsync();
        }

        public async Task<List<MemberRegister>> GetMembersByRegionAsync(string region)
        {
            return await _repository.Entities.Where(x => x.region == region).ToListAsync();
        }

        public async Task<List<MemberRegister>> GetMembersBySubResionAsync(string subresion)
        {
            return await _repository.Entities.Where(x => x.sub_region == subresion).ToListAsync();
        }

        public async Task<List<MemberRegister>> GetMembersByIDNoAsync(string idno)
        {
            return await _repository.Entities.Where(x => x.id_no == idno).ToListAsync();
        }

        public async Task<MemberRegister> GetMembersByUsernameAsync(string username)
        {
            return await _repository.Entities.Where(x => x.username == username).SingleOrDefaultAsync();
        }

        public async Task<Result<string>> ActivateMemberAsync(int Id)
        {
            var member = _repository.Entities.Where(ie=> ie.Id == Id).FirstOrDefault();
            if (member != null)
            {
                member.member_in_good_standing = true;
                await _repository.UpdateAsync(member);
                return await Result<string>.SuccessAsync(member.membership_no, "Member Detail Activated.");
            }
            else
            {
                return await Result<string>.FailureAsync("Member Not Found.");
            }
        }

        public async Task<Result<string>> DeactivateMemberAsync(int Id, string reason)
        {
            var member = _repository.Entities.Where(ie => ie.Id == Id).FirstOrDefault();
            if (member != null)
            {
                member.member_in_good_standing = false;
                await _repository.UpdateAsync(member);
                return await Result<string>.SuccessAsync(member.membership_no, "Member Detail Deactivated.");
            }
            else
            {
                return await Result<string>.FailureAsync("Member Not Found.");
            }
        }
    }
}