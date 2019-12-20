using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class CarroceriaMap : IEntityTypeConfiguration<Carroceria> {
    public void Configure(EntityTypeBuilder<Carroceria> builder) {
      // Primary Key
      builder.HasKey(t => t.VeiculoId);

      // Table, Properties & Column Mappings
      builder.ToTable("Carrocerias", "opc");
      builder.Property(t => t.VeiculoId).HasColumnName("VeiculoId").IsRequired();
      builder.Property(t => t.Fabricante).HasColumnName("Fabricante").HasMaxLength(64);
      builder.Property(t => t.Modelo).HasColumnName("Modelo").HasMaxLength(64);
      builder.Property(t => t.Referencia).HasColumnName("Referencia").HasMaxLength(32);
      builder.Property(t => t.Ano).HasColumnName("Ano");
      builder.Property(t => t.Aquisicao).HasColumnName("Aquisicao");
      builder.Property(t => t.Fornecedor).HasColumnName("Fornecedor").HasMaxLength(64);
      builder.Property(t => t.NotaFiscal).HasColumnName("NotaFiscal").HasMaxLength(16);
      builder.Property(t => t.Valor).HasColumnName("Valor");
      builder.Property(t => t.ChaveNfe).HasColumnName("ChaveNfe").HasMaxLength(64);
      builder.Property(t => t.Encarrocamento).HasColumnName("Encarrocamento");
      builder.Property(t => t.QuemEncarroca).HasColumnName("QuemEncarroca").HasMaxLength(64);
      builder.Property(t => t.NotaEncarroca).HasColumnName("NotaEncarroca").HasMaxLength(16);
      builder.Property(t => t.ValorEncarroca).HasColumnName("ValorEncarroca");
      builder.Property(t => t.Portas).HasColumnName("Portas").IsRequired();
      builder.Property(t => t.Assentos).HasColumnName("Assentos");
      builder.Property(t => t.Capacidade).HasColumnName("Capacidade");
      builder.Property(t => t.Piso).HasColumnName("Piso").HasMaxLength(32);
      builder.Property(t => t.EscapeV).HasColumnName("EscapeV").IsRequired();
      builder.Property(t => t.EscapeH).HasColumnName("EscapeH").IsRequired();
      builder.Property(t => t.Catraca).HasColumnName("Catraca");
      builder.Property(t => t.PortaIn).HasColumnName("PortaIn").IsRequired();
      builder.Property(t => t.SaidaFrente).HasColumnName("SaidaFrente").IsRequired();
      builder.Property(t => t.SaidaMeio).HasColumnName("SaidaMeio").IsRequired();
      builder.Property(t => t.SaidaTras).HasColumnName("SaidaTras").IsRequired();

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Veiculo)
          .WithOne(t => t.Carrocerias).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
