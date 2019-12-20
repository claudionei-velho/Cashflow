using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class PremissaMap : IEntityTypeConfiguration<Premissa> {
    public void Configure(EntityTypeBuilder<Premissa> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Premissas", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.KmProdutivo).HasColumnName("KmProdutivo")
          .IsRequired().HasColumnType("decimal(24, 6)");

      builder.Property(t => t.KmImprodutivo).HasColumnName("KmImprodutivo").HasColumnType("decimal(24, 6)");
      builder.Property(t => t.FrotaOperacao).HasColumnName("FrotaOperacao").IsRequired();
      builder.Property(t => t.FrotaReserva).HasColumnName("FrotaReserva");
      builder.Property(t => t.Demanda).HasColumnName("Demanda").IsRequired();
      builder.Property(t => t.Equivalente).HasColumnName("Equivalente").IsRequired();
      builder.Property(t => t.IPKe).HasColumnName("IPKe").HasColumnType("decimal(24, 6)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.Empresa)
          .WithMany(f => f.Premissas).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
