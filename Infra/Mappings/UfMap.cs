using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class UfMap : IEntityTypeConfiguration<Uf> {
    public void Configure(EntityTypeBuilder<Uf> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("IbgeUf");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Sigla).HasColumnName("Sigla")
          .IsRequired().IsFixedLength().HasMaxLength(2);

      builder.Property(t => t.Estado).HasColumnName("Estado")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Capital).HasColumnName("Capital")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Regiao).HasColumnName("Regiao")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Unidades).HasColumnName("Unidades");
    }
  }
}
