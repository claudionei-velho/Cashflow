using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class UComercialMap : IEntityTypeConfiguration<UComercial> {
    public void Configure(EntityTypeBuilder<UComercial> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("UComercial", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id")
          .IsRequired().HasMaxLength(8);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(32);
    }
  }
}
