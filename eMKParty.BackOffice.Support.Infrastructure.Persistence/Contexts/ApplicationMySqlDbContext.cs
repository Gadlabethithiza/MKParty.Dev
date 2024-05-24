using eMKParty.BackOffice.Support.Domain.Common;
using System.Reflection;
using eMKParty.BackOffice.Support.Domain.Common.Interfaces;
using eMKParty.BackOffice.Support.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts
{
    public class ApplicationMySqlDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public ApplicationMySqlDbContext(DbContextOptions<ApplicationMySqlDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<MemberRegister> Memberships { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<VotingStation> VotingStations { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<VotingResult> VotingResults { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        //static readonly string connectionString = "Server=102.211.28.103:3306;User ID=root; Password=Pr0v1d3nce@MK; Database=mkpartymasterdb";
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL(connectionString, ServerVersion.AutoDetect(connectionString));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.ToTable("VD_Incidents");
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<VotingResult>(entity =>
            {
                entity.ToTable("VD_VotingResults");
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.ToTable("config");
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("lst_Province");
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.ToTable("lst_Municipality");
                entity.Property(e => e.Municipality_ID).HasColumnName("id");
                entity.Property(e => e.MunicipalityName).HasColumnName("Municipality");
            });

            modelBuilder.Entity<VotingStation>(entity =>
            {
                entity.ToTable("lst_VotingStation");
                entity.Property(e => e.VotingStation_ID).HasColumnName("id");

                //entity.Property(x => x.VotingDistrict)
                //      .HasConversion<string>();
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("lst_Ward");
                entity.Property(e => e.Ward_ID).HasColumnName("id");

            });

            modelBuilder.Entity<MemberRegister>(entity =>
            {
                entity.ToTable("membership");
            });


            //modelBuilder.Entity<MemberRegister>()
            //  .HasMany(p => p.Roles)
            //  .WithMany(b => b.Users)
            //  .Map(p =>
            //  {
                  
            //  });

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}

