using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class SistemaMap : IEntityTypeConfiguration<Sistema> {
    public void Configure(EntityTypeBuilder<Sistema> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Sistemas");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Codigo).HasColumnName("Codigo")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");
    }
  }
}
