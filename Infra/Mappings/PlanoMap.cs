using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class PlanoMap : IEntityTypeConfiguration<Plano> {
    public void Configure(EntityTypeBuilder<Plano> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Planos", "opc");
      builder.Property(t => t.LinhaId).HasColumnName("LinhaId").IsRequired();
      builder.Property(t => t.AtendimentoId).HasColumnName("AtendimentoId");
      builder.Property(t => t.Sentido).HasColumnName("Sentido")
          .IsRequired().IsFixedLength().HasMaxLength(2);

      builder.Property(t => t.ViagensUtil).HasColumnName("ViagensUtil");
      builder.Property(t => t.ViagensSab).HasColumnName("ViagensSab");
      builder.Property(t => t.ViagensDom).HasColumnName("ViagensDom");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Atendimento)
          .WithMany(f => f.Planos).HasForeignKey(k => k.AtendimentoId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Linha)
          .WithMany(f => f.Planos).HasForeignKey(k => k.LinhaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);      
    }
  }
}
