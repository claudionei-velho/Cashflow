using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class PCombustivelMap : IEntityTypeConfiguration<PCombustivel> {
    public void Configure(EntityTypeBuilder<PCombustivel> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("PCombustiveis", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.ClasseId).HasColumnName("ClasseId").IsRequired();
      builder.Property(t => t.CombustivelId).HasColumnName("CombustivelId").IsRequired();
      builder.Property(t => t.CoeficienteComAr).HasColumnName("CoeficienteComAr").HasColumnType("decimal(24, 6)");
      builder.Property(t => t.CoeficienteSemAr).HasColumnName("CoeficienteSemAr").HasColumnName("decimal(24, 6)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.Empresa)
          .WithMany(f => f.PCombustiveis).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.CVeiculo)
          .WithMany(f => f.PCombustiveis).HasForeignKey(k => k.ClasseId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Combustivel)
          .WithMany(f => f.PCombustiveis).HasForeignKey(k => k.CombustivelId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
