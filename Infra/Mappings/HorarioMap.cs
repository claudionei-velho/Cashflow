using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class HorarioMap : IEntityTypeConfiguration<Horario> {
    public void Configure(EntityTypeBuilder<Horario> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Horarios", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.LinhaId).HasColumnName("LinhaId").IsRequired();
      builder.Property(t => t.DiaId).HasColumnName("DiaId").IsRequired();
      builder.Property(t => t.Sentido).HasColumnName("Sentido")
          .IsRequired().IsFixedLength().HasMaxLength(2);

      builder.Property(t => t.Inicio).HasColumnName("Inicio").IsRequired();
      builder.Property(t => t.AtendimentoId).HasColumnName("AtendimentoId");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Atendimento)
          .WithMany(f => f.Horarios).HasForeignKey(k => k.AtendimentoId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Linha)
          .WithMany(f => f.Horarios).HasForeignKey(k => k.LinhaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
