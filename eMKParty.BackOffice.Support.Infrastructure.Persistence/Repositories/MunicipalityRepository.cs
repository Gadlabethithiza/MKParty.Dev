using System;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using eMKParty.BackOffice.Support.Domain.Entities.Feedback;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
	public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly IGenericRepository<Municipality> _repository;
        //private readonly IGenericRepository<Province> _repositoryProv;

        public MunicipalityRepository(IGenericRepository<Municipality> repository, IGenericRepository<Province> repositoryProv)
        {
            _repository = repository;
            //_repositoryProv = repositoryProv;
        }


        public async Task<List<MunicipalityFeedDto>> GetAllMunicipalitiesAsync()
        {
            List<MunicipalityFeedDto> Listing = new List<MunicipalityFeedDto>();

            try
            {
                var municipalities = await _repository.Entities.ToListAsync();

                if (municipalities.Count > 0)
                {
                    foreach (var item in municipalities)
                    {
                        MunicipalityFeedDto citem = new MunicipalityFeedDto();
                        citem.Municipality_ID = item.Municipality_ID;
                        citem.MunicipalityCode = item.MunicipalityCode;
                        citem.MunicipalityName = item.MunicipalityName;
                        citem.FkProvince_ID = item.FkProvince_ID;
                        citem.createdby = item.createdby;
                        citem.createddate = item.createddate;
                        citem.modifiedby = item.modifiedby;
                        citem.modifieddate = item.modifieddate;
                        citem.Guid = item.Guid;
                        citem.Province = item.Province;
                        //citem.Province = provinces.Where(i => i.Province_ID == item.FkProvince_ID).SingleOrDefault().ProvinceDesc;
                        Listing.Add(citem);
                    }
                }
            }
            catch (Exception ex)
            {

            }


            return Listing;
        }

        //public async Task<List<MemberRegister>> GetMembersByBranchAsync(int branchId)
        //{
        //    return await _repository.Entities.Where(x => x.branch_id == branchId).ToListAsync();
        //}
    }
}

