using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ProducaoMediaMap : IEntityTypeConfiguration<ProducaoMedia> {
    public void Configure(EntityTypeBuilder<ProducaoMedia> builder) {
      // Primary Key
      builder.HasKey(t => new { t.EmpresaId, t.Ano, t.TarifariaId });

      // Table, Properties & Column Mappings
      builder.ToTable("ProducaoMedia", "opc");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.TarifariaId).HasColumnName("TarifariaId").IsRequired();
      builder.Property(t => t.Passageiros).HasColumnName("Passageiros");
      builder.Property(t => t.Mensal).HasColumnName("Mensal");
      builder.Property(t => t.Equivalente).HasColumnName("Equivalente");
      builder.Property(t => t.MensalEqv).HasColumnName("MensalEqv");
      builder.Property(t => t.Ratio).HasColumnName("Ratio");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.ProducoesMedias).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.TCategoria)
          .WithMany(f => f.ProducoesMensais).HasForeignKey(k => k.TarifariaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
