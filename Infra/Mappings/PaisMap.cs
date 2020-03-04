using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class PaisMap : IEntityTypeConfiguration<Pais> {
    public void Configure(EntityTypeBuilder<Pais> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Paises");

      builder.Property(t => t.Id).HasColumnName("Id")
          .IsRequired().HasMaxLength(8);

      builder.Property(t => t.Nome).HasColumnName("Nome")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Capital).HasColumnName("Capital").HasMaxLength(32);
      builder.Property(t => t.Continente).HasColumnName("Continente").HasMaxLength(32);
    }
  }
}
