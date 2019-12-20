using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class DominioMap : IEntityTypeConfiguration<Dominio> {
    public void Configure(EntityTypeBuilder<Dominio> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Dominios");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Descricao).HasColumnName("Descricao").HasMaxLength(256);
    }
  }
}
