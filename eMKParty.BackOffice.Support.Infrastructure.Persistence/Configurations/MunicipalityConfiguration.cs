using System;
using System.Data;
using System.Net;
using System.Reflection.Emit;
using eMKParty.BackOffice.Support.Domain.Common.Interfaces;
using eMKParty.BackOffice.Support.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMKParty.BackOffice.Support.Infrastructure.Persistence.Configurations
{
	//public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
 //   {
 //       public void Configure(EntityTypeBuilder<Municipality> builder)
 //       {
 //           //// Other entity configurations
 //           //builder.ToTable("lst_Municipality", "support");
 //           //builder.HasKey(b => b.Municipality_ID);
 //           //builder.Property(e => e.MunicipalityName).HasColumnName("Municipality");
 //           ////builder.Property(e => e.FkProvince_ID).HasColumnName("FkProvince_ID");

 //           //builder
 //           //  .HasOne(p => p.Province)
 //           //  .WithMany(b => b.Municipalities)
 //           //  .HasForeignKey(p => p.FkProvince_ID);
 //       }
 //   }
}