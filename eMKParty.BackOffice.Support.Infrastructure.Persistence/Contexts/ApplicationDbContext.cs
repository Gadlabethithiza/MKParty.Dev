using System;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection;
using eMKParty.BackOffice.Support.Domain.Common;
using eMKParty.BackOffice.Support.Domain.Common.Interfaces;
using eMKParty.BackOffice.Support.Domain.Entities;

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

        public DbSet<MemberRegister> Memberships => Set<MemberRegister>();
        public DbSet<Province> Provinces => Set<Province>();
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

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("assets", "backoffice");
                entity.Property<int>("id");
                entity.HasKey("id");
            });

            modelBuilder.Entity<MemberRegister>(entity =>
            {
                entity.ToTable("membership", "backoffice");
                entity.Property<int>("Id");
                entity.HasKey("Id");


                entity.Property(e => e.subregion).HasColumnName("sub_region");
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