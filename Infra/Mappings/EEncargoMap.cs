using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class EEncargoMap : IEntityTypeConfiguration<EEncargo> {
    public void Configure(EntityTypeBuilder<EEncargo> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("EEncargos");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.EncargoId).HasColumnName("EncargoId").IsRequired();
      builder.Property(t => t.Formula).HasColumnName("Formula").HasMaxLength(1024);
      builder.Property(t => t.Coeficiente).HasColumnName("Coeficiente")
          .IsRequired().HasColumnType("decimal(24, 12)");

      builder.Property(t => t.Vigente).HasColumnName("Vigente").IsRequired();
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.EEncargos).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Encargo)
          .WithMany(f => f.EEncargos).HasForeignKey(k => k.EncargoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);      
    }
  }
}
