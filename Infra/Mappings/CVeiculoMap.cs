using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class CVeiculoMap : IEntityTypeConfiguration<CVeiculo> {
    public void Configure(EntityTypeBuilder<CVeiculo> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("CVeiculos", "opc");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Categoria).HasColumnName("Categoria")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Classe).HasColumnName("Classe")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Minimo).HasColumnName("Minimo");
      builder.Property(t => t.Maximo).HasColumnName("Maximo");
    }
  }
}
