using System;
using AutoMapper.Execution;
using eMKParty.BackOffice.Support.Application.Features.Memberships.Commands.ModifyMember;
using System.Threading;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories
{
	public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IGenericRepository<Config> _repository;
        private readonly ApplicationMySqlDbContext _context;
        private readonly ILogger<ConfigurationRepository> _logger;

        public ConfigurationRepository(ApplicationMySqlDbContext context, IGenericRepository<Config> repository, ILogger<ConfigurationRepository> logger)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
        }


        public async Task<Config> GetConfigarationsByTypeAsync(string configType)
        {
            var commandText = string.Format(@$"SELECT configType,configValue,configDesc FROM config WHERE configType= '{{0}}'",configType);
            //var myConfigType = new SqlParameter("@configType", configType);

            var item = await _context.Database.ExecuteSqlRawAsync(commandText);



            //return await _context.Configurations.Where(x => x.configType == configType).SingleOrDefaultAsync();
            //return await _repository.Entities.Where(x => x.configType == configType).SingleOrDefaultAsync();
            return null;
        }

        public async Task<bool> UpdateConfigValue(string configType)
        {
            bool erroroccured = false;

            try
            {
                //var commandText = "UPDATE config SET configValue = configValue+1 WHERE configType= @configType";
                //var myConfigType = new SqlParameter("@configType", configType);

                await _context.Database.ExecuteSqlRawAsync($@"UPDATE config SET configValue = configValue+1 WHERE configType={configType}");

            }
            catch (Exception ex)
            {
                erroroccured = true;
                _logger.LogError(ex.Message);
            }

            return erroroccured;
        }
    }
}