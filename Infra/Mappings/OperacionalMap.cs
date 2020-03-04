using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class OperacionalMap : IEntityTypeConfiguration<Operacional> {
    public void Configure(EntityTypeBuilder<Operacional> builder) {
      // Primary Key
      builder.HasKey(t => new { t.LinhaId, t.Prefixo, t.Sentido });

      // Table, Properties & Column Mappings
      builder.ToTable("PlanOperacional", "opc");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.LinhaId).HasColumnName("LinhaId").IsRequired();
      builder.Property(t => t.AtendimentoId).HasColumnName("AtendimentoId");
      builder.Property(t => t.Prefixo).HasColumnName("Prefixo").IsRequired();
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao");
      builder.Property(t => t.Sentido).HasColumnName("Sentido")
          .IsRequired().IsFixedLength().HasMaxLength(2);

      builder.Property(t => t.DiaOperacao).HasColumnName("DiaOperacao");
      builder.Property(t => t.Funcao).HasColumnName("Funcao");
      builder.Property(t => t.Extensao).HasColumnName("Extensao").HasColumnType("decimal(18, 3)");
      builder.Property(t => t.ViagensUtil).HasColumnName("ViagensUtil");
      builder.Property(t => t.ViagensSab).HasColumnName("ViagensSab");
      builder.Property(t => t.ViagensDom).HasColumnName("ViagensDom");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.Atendimento)
          .WithMany(f => f.Operacionais).HasForeignKey(k => k.AtendimentoId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Empresa)
          .WithMany(f => f.Operacionais).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Linha)
          .WithMany(f => f.Operacionais).HasForeignKey(k => k.LinhaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
