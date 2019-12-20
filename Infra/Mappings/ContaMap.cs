using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ContaMap : IEntityTypeConfiguration<Conta> {
    public void Configure(EntityTypeBuilder<Conta> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Contas");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Classificacao).HasColumnName("Classificacao")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.VinculoId).HasColumnName("VinculoId");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Relationships
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Contas).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Vinculo)
          .WithMany(f => f.Contas).HasForeignKey(k => k.VinculoId)
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
