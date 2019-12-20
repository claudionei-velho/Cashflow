using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class EmbarcadoMap : IEntityTypeConfiguration<Embarcado> {
    public void Configure(EntityTypeBuilder<Embarcado> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Embarcados", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.VeiculoId).HasColumnName("VeiculoId").IsRequired();
      builder.Property(t => t.EquipamentoId).HasColumnName("EquipamentoId").IsRequired();
      builder.Property(t => t.SerieNo).HasColumnName("SerieNo").HasMaxLength(32);
      builder.Property(t => t.Fabricante).HasColumnName("Fabricante").HasMaxLength(64);
      builder.Property(t => t.Aquisicao).HasColumnName("Aquisicao");
      builder.Property(t => t.ChaveNfe).HasColumnName("ChaveNfe").HasMaxLength(64);
      builder.Property(t => t.Quantidade).HasColumnName("Quantidade")
          .IsRequired().HasColumnType("decimal(18, 3)");

      builder.Property(t => t.Instalacao).HasColumnName("Instalacao");
      builder.Property(t => t.OSInstala).HasColumnName("OSInstala");
      builder.Property(t => t.Localizacao).HasColumnName("Localizacao").HasMaxLength(32);
      builder.Property(t => t.Desinstalacao).HasColumnName("Desinstalacao");
      builder.Property(t => t.OSDesinstala).HasColumnName("OSDesinstala");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Veiculo)
          .WithMany(f => f.Embarcados).HasForeignKey(k => k.VeiculoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Equipamento)
          .WithMany(f => f.Embarcados).HasForeignKey(k => k.EquipamentoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
