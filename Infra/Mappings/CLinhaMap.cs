using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class CLinhaMap : IEntityTypeConfiguration<CLinha> {
    public void Configure(EntityTypeBuilder<CLinha> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("CLinhas", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.ClassLinhaId).HasColumnName("ClassLinhaId").IsRequired();
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Relationships
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.CLinhas).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.ClassLinha)
          .WithMany(f => f.CLinhas).HasForeignKey(k => k.ClassLinhaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
