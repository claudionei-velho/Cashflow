using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class RhIndiceMap : IEntityTypeConfiguration<RhIndice> {
    public void Configure(EntityTypeBuilder<RhIndice> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("RhIndices");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Indice).HasColumnName("Indice")
          .IsRequired().HasMaxLength(8);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(128);

      builder.Property(t => t.Unidade).HasColumnName("Unidade").HasMaxLength(16);
      builder.Property(t => t.Referencia).HasColumnName("Referencia");
    }
  }
}
