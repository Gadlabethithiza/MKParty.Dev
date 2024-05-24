using System;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using eMKParty.BackOffice.Support.Application.Interfaces.Repositories;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Repositories;
using eMKParty.BackOffice.Support.Application.Interfaces;
using eMKParty.BackOffice.Support.Infrastructure.Services;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMappings();
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //var connectionString = configuration.GetSection("ConnectionStrings")["DefaultMKPartyMySqlConnection"];

            services.AddDbContext<ApplicationMySqlDbContext>(options =>
               options.UseMySQL(configuration.GetConnectionString("DefaultMKPartyMySqlConnection").ToString(),
               builder => builder.MigrationsAssembly(typeof(ApplicationMySqlDbContext).Assembly.FullName)));
        }


        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddCors(); //This will allow Angular app to call this api
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient(typeof(IMembershipRepository),typeof(MembershipRepository))
                .AddTransient(typeof(IMunicipalityRepository), typeof(MunicipalityRepository))
                .AddTransient(typeof(IVotingResultRepository), typeof(VotingResultRepository))
                .AddTransient(typeof(IProvinceRepository), typeof(ProvinceRepository))
                .AddTransient(typeof(IConfigurationRepository), typeof(ConfigurationRepository))
                .AddTransient<ITokenService, TokenService>()

                .AddTransient(typeof(IDatabaseUnitOfWork), typeof(DatabaseUnitOfWork))               

                .AddTransient<IPlayerRepository, PlayerRepository>()
                .AddTransient<IClubRepository, ClubRepository>()
                .AddTransient<IStadiumRepository, StadiumRepository>()
                .AddTransient<ICountryRepository, CountryRepository>();
                //.AddTransient(typeof(IAesOperation), typeof(AesOperation))

        }
    }
}