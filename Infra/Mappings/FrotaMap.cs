using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class FrotaMap : IEntityTypeConfiguration<Frota> {
    public void Configure(EntityTypeBuilder<Frota> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Frotas", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.CategoriaId).HasColumnName("CategoriaId").IsRequired();
      builder.Property(t => t.EtariaId).HasColumnName("EtariaId").IsRequired();
      builder.Property(t => t.ArCondicionado).HasColumnName("ArCondicionado").IsRequired();
      builder.Property(t => t.Quantidade).HasColumnName("Quantidade").IsRequired();

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.CVeiculo)
          .WithMany(f => f.Frotas).HasForeignKey(k => k.CategoriaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Frotas).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.FxEtaria)
          .WithMany(f => f.Frotas).HasForeignKey(k => k.EtariaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

    }
  }
}
