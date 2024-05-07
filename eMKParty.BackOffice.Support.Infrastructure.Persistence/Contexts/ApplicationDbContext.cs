using System;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Common.Interfaces;
using eMKParty.BackOffice.Support.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;
using System.Data;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<MemberRegister> Memberships { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<VotingStation> VotingStations { get; set; }
        public DbSet<Ward> Wards { get; set; }
        //public DbSet<MemberRegister> Memberships => Set<MemberRegister>();
        //public DbSet<Province> Provinces => Set<Province>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Branch_Leadership> Branch_Leaderships => Set<Branch_Leadership>();
        public DbSet<Branch_Ward> Branch_Wards => Set<Branch_Ward>();
        public DbSet<Asset> Assets => Set<Asset>();

        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Stadium> Stadiums => Set<Stadium>();
        public DbSet<Country> Countries => Set<Country>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("lst_Province", "support");
                entity.Property<int>("Province_ID");
                entity.HasKey("Province_ID");
            });

            //modelBuilder.Entity<Municipality>().ToTable("lst_Municipality", "support");
            //modelBuilder.Entity<Municipality>()
            //    .HasOne(p => p.Province)
            //            .WithMany(b => b.Municipalities)
            //            .HasForeignKey(p => p.FkProvince_ID);

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.ToTable("lst_Municipality", "support");
                entity.Property<int>("Municipality_ID");
                entity.HasKey("Municipality_ID");
                entity.Property(e => e.MunicipalityName).HasColumnName("Municipality");
                entity.Property(e => e.FkProvince_ID).HasColumnName("FkProvince_ID");
                //entity.WithOne(e => e.Blog)

            });

            //modelBuilder.Entity<Municipality>()
            //  .HasOne(p => p.Province)
            //  .WithOne()
            //  //.WithMany(b => b.Municipalities)
            //  .HasForeignKey(p => p.);

            modelBuilder.Entity<VotingStation>(entity =>
            {
                entity.ToTable("lst_VotingStation", "support");
                entity.Property<int>("VotingStation_ID");
                entity.HasKey("VotingStation_ID");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("lst_Ward", "support");
                entity.Property<int>("Ward_ID");
                entity.HasKey("Ward_ID");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("assets", "backoffice");
                //entity.Property<int>("id");
                //entity.HasKey("id");
            });

            modelBuilder.Entity<MemberRegister>(entity =>
            {
                entity.ToTable("membership", "backoffice");
                //entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd(); // <-- This fixed it for me
                //entity.HasKey(x => x.id);
                //entity.Property(x => x.id).ValueGeneratedOnAdd();
                //entity.Property<int>("id");
                //entity.HasKey("id");


                //entity.Property(e => e.sub_region).HasColumnName("sub_region");
                //entity.Property(e => e.updatedby).HasColumnName("modifiedby");
                //entity.Property(e => e.updateddate).HasColumnName("modifieddate");

                //entity.Property(e => e.first_name).HasMaxLength(100).HasColumnName("firstname");
                //entity.Property(e => e.last_name).HasMaxLength(100).HasColumnName("lastname");
                //entity.Property(e => e.dob).HasColumnType("datetime").HasColumnName("birth_date");
                //entity.Property(e => e.InstitutionId).HasColumnName("InstitutionId");
                //entity.Property(e => e.address).HasColumnName("address");
                //entity.Property(e => e.city).HasColumnName("city");
                //entity.Property(e => e.ProvinceId).HasColumnName("ProvinceId");
                //entity.Property(e => e.telephone).HasColumnName("phonenumber");
                //entity.Property(e => e.mobilet).HasColumnName("mobitel");
                //entity.Property(e => e.emailAd).HasColumnName("email");
                //entity.Property(e => e.gender).HasColumnName("gender");
                //entity.Property(e => e.jobtitle).HasColumnName("jobtitle");
                //entity.Property(e => e.username).HasColumnName("username");
                //entity.Property(e => e.password).HasColumnName("PasswordHash");
                //entity.Property(e => e.passwordSalt).HasColumnName("PasswordSalt");
                //entity.Property(e => e.security_vrae).HasColumnName("security_question");
                //entity.Property(e => e.security_antiwoord).HasColumnName("security_answer");
                //entity.Property(e => e.emergency_name).HasColumnName("emergency_contact_name");
                //entity.Property(e => e.emergency_tel).HasColumnName("emergency_contact_tel");
                //entity.Property(e => e.created_by).HasColumnName("createdby");
                //entity.Property(e => e.modified_by).HasColumnName("modifiedby");
                //entity.Property(e => e.created_date).HasColumnType("datetime").HasColumnName("createddate").HasDefaultValueSql("(getdate())");
                //entity.Property(e => e.modified_date).HasColumnType("datetime").HasColumnName("modifieddate").HasDefaultValueSql("(getdate())");
                //entity.Property(e => e.active).HasColumnName("active");
                //entity.Property(e => e.deleted).HasColumnName("deleted");
                //entity.Property(e => e.guid).HasColumnName("Guid");

                //modelBuilder.Entity<Actor_Movie>().HasKey(am => new
                //{
                //    am.ActorID,
                //    am.MovieID
                //});

                //modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie)
                //.WithMany(am => am.Actor_Movie).HasForeignKey(m => m.MovieID);

                //modelBuilder.Entity<Actor_Movie>().HasOne(a => a.Actor)
                //.WithMany(at => at.Actors_Movies).HasForeignKey(a => a.ActorID);
            });

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