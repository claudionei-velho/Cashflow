using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class VCatalogoMap : IEntityTypeConfiguration<VCatalogo> {
    public void Configure(EntityTypeBuilder<VCatalogo> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("VCatalogos", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.ClasseId).HasColumnName("ClasseId").IsRequired();
      builder.Property(t => t.FornecedorId).HasColumnName("FornecedorId").IsRequired();
      builder.Property(t => t.Unitario).HasColumnName("Unitario").HasColumnType("money");
      builder.Property(t => t.UnitarioAr).HasColumnName("UnitarioAr").HasColumnType("money");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.VCatalogos).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.CVeiculo)
          .WithMany(f => f.VCatalogos).HasForeignKey(k => k.ClasseId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Fornecedor)
          .WithMany(f => f.VCatalogos).HasForeignKey(k => k.FornecedorId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
