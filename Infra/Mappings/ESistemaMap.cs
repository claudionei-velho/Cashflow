using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ESistemaMap : IEntityTypeConfiguration<ESistema> {
    public void Configure(EntityTypeBuilder<ESistema> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("ESistemas");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.SistemaId).HasColumnName("SistemaId").IsRequired();
      builder.Property(t => t.Codigo).HasColumnName("Codigo")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.ResponsavelId).HasColumnName("ResponsavelId");
      builder.Property(t => t.Util).HasColumnName("Util");
      builder.Property(t => t.Depreciacao).HasColumnName("Depreciacao");
      builder.Property(t => t.Residual).HasColumnName("Residual");      
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.Cargo)
          .WithMany(f => f.ESistemas).HasForeignKey(k => k.ResponsavelId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Empresa)
          .WithMany(f => f.ESistemas).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Sistema)
          .WithMany(f => f.ESistemas).HasForeignKey(k => k.SistemaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
