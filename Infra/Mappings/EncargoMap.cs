using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class EncargoMap : IEntityTypeConfiguration<Encargo> {
    public void Configure(EntityTypeBuilder<Encargo> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Encargos");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Grupo).HasColumnName("Grupo").IsRequired();
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Observacao).HasColumnName("Observacao").HasMaxLength(2048);
    }
  }
}
