using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class CstCombustivelMap : IEntityTypeConfiguration<CstCombustivel> {
    public void Configure(EntityTypeBuilder<CstCombustivel> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("CstCombustiveis", "cst");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.CombustivelId).HasColumnName("CombustivelId").IsRequired();
      builder.Property(t => t.Unitario).HasColumnName("Unitario")
          .IsRequired().HasColumnName("money");

      builder.Property(t => t.Frete).HasColumnName("Frete").HasColumnType("numeric(9, 4)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.Empresa)
        .WithMany(f => f.CstCombustiveis).HasForeignKey(k => k.EmpresaId).IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Combustivel)
        .WithMany(f => f.CstCombustiveis).HasForeignKey(k => k.CombustivelId).IsRequired()
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
