using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class FrotaEtariaMap : IEntityTypeConfiguration<FrotaEtaria> {
    public void Configure(EntityTypeBuilder<FrotaEtaria> builder) {
      // Primary Key
      builder.HasKey(t => new { t.EmpresaId, t.EtariaId });

      // Table, Properties & Column Mappings
      builder.ToTable("FrotaEtaria", "opc");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.EtariaId).HasColumnName("EtariaId").IsRequired();
      builder.Property(t => t.Micro).HasColumnName("Micro");
      builder.Property(t => t.Mini).HasColumnName("Mini");
      builder.Property(t => t.Midi).HasColumnName("Midi");
      builder.Property(t => t.Basico).HasColumnName("Basico");
      builder.Property(t => t.Padron).HasColumnName("Padron");
      builder.Property(t => t.Especial).HasColumnName("Especial");
      builder.Property(t => t.Articulado).HasColumnName("Articulado");
      builder.Property(t => t.BiArticulado).HasColumnName("BiArticulado");
      builder.Property(t => t.Frota).HasColumnName("Frota");
      builder.Property(t => t.Ratio).HasColumnName("Ratio").HasColumnType("decimal(24, 6)");
      builder.Property(t => t.EqvIdade).HasColumnName("EqvIdade").HasColumnType("decimal(24, 3)");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.FrotaEtarias).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.FxEtaria)
          .WithMany(f => f.FrotaEtarias).HasForeignKey(k => k.EtariaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
