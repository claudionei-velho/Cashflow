using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class NfVeiculoMap : IEntityTypeConfiguration<NfVeiculo> {
    public void Configure(EntityTypeBuilder<NfVeiculo> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("NfVeiculos", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.NotaId).HasColumnName("NotaId").IsRequired();
      builder.Property(t => t.ChassiNo).HasColumnName("ChassiNo")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.CorId).HasColumnName("CorId").HasMaxLength(4);
      builder.Property(t => t.Cor).HasColumnName("Cor").HasMaxLength(32);
      builder.Property(t => t.MotorCv).HasColumnName("MotorCv").HasMaxLength(4);
      builder.Property(t => t.Cilindrada).HasColumnName("Cilindrada").HasMaxLength(4);
      builder.Property(t => t.Liquido).HasColumnName("Liquido")
          .IsRequired().HasColumnType("decimal(18, 3)");

      builder.Property(t => t.Bruto).HasColumnName("Bruto")
          .IsRequired().HasColumnType("decimal(18, 3)");

      builder.Property(t => t.Serial).HasColumnName("Serial").HasMaxLength(16);
      builder.Property(t => t.CombustivelId).HasColumnName("CombustivelId").IsRequired();
      builder.Property(t => t.MotorNo).HasColumnName("MotorNo").HasMaxLength(32);
      builder.Property(t => t.Tracao).HasColumnName("Tracao").HasColumnType("decimal(18, 3)");
      builder.Property(t => t.EntreEixos).HasColumnName("EntreEixos").HasColumnType("decimal(9, 3)");
      builder.Property(t => t.AnoModelo).HasColumnName("AnoModelo");
      builder.Property(t => t.AnoFabrica).HasColumnName("AnoFabrica");
      builder.Property(t => t.Pintura).HasColumnName("Pintura").HasMaxLength(16);
      builder.Property(t => t.TVeiculoId).HasColumnName("TVeiculoId").IsRequired();
      builder.Property(t => t.EVeiculoId).HasColumnName("EVeiculoId").IsRequired();
      builder.Property(t => t.CondicaoVin).HasColumnName("CondicaoVin")
          .IsRequired().IsFixedLength().HasMaxLength(1);

      builder.Property(t => t.Modelo).HasColumnName("Modelo").IsRequired();
      builder.Property(t => t.CorDenatran).HasColumnName("CorDenatram").IsRequired();
      builder.Property(t => t.Lotacao).HasColumnName("Lotacao").IsRequired();
      builder.Property(t => t.RestricaoId).HasColumnName("RestricaoId").IsRequired();

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.NFiscal)
          .WithMany(f => f.NfVeiculos).HasForeignKey(k => k.NotaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
