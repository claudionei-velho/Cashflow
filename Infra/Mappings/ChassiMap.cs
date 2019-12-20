using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ChassiMap : IEntityTypeConfiguration<Chassi> {
    public void Configure(EntityTypeBuilder<Chassi> builder) {
      // Primary Key
      builder.HasKey(t => t.VeiculoId);

      // Table, Properties & Column Mappings
      builder.ToTable("Chassis", "opc");
      builder.Property(t => t.VeiculoId).HasColumnName("VeiculoId").IsRequired();
      builder.Property(t => t.Fabricante).HasColumnName("Fabricante").HasMaxLength(64);
      builder.Property(t => t.Modelo).HasColumnName("Modelo").HasMaxLength(64);
      builder.Property(t => t.ChassiNo).HasColumnName("ChassiNo").HasMaxLength(32);
      builder.Property(t => t.Ano).HasColumnName("Ano");
      builder.Property(t => t.Aquisicao).HasColumnName("Aquisicao");
      builder.Property(t => t.Fornecedor).HasColumnName("Fornecedor").HasMaxLength(64);
      builder.Property(t => t.NotaFiscal).HasColumnName("NotaFiscal").HasMaxLength(16);
      builder.Property(t => t.Valor).HasColumnName("Valor");
      builder.Property(t => t.ChaveNfe).HasColumnName("ChaveNfe").HasMaxLength(64);
      builder.Property(t => t.MotorId).HasColumnName("MotorId");
      builder.Property(t => t.Potencia).HasColumnName("Potencia").HasMaxLength(32);
      builder.Property(t => t.PosMotor).HasColumnName("PosMotor");
      builder.Property(t => t.EixosFrente).HasColumnName("EixosFrente").IsRequired();
      builder.Property(t => t.EixosTras).HasColumnName("EixosTras").IsRequired();
      builder.Property(t => t.PneusFrente).HasColumnName("PneusFrente").HasMaxLength(16);
      builder.Property(t => t.PneusTras).HasColumnName("PneusTras").HasMaxLength(16);
      builder.Property(t => t.TransmiteId).HasColumnName("TransmiteId");
      builder.Property(t => t.DirecaoId).HasColumnName("DirecaoId");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Veiculo).WithOne(f => f.Chassis).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
