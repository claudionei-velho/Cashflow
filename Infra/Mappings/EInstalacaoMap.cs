using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class EInstalacaoMap : IEntityTypeConfiguration<EInstalacao> {
    public void Configure(EntityTypeBuilder<EInstalacao> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("EInstalacoes", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(t => t.PropositoId).HasColumnName("PropositoId").IsRequired();
      builder.Property(t => t.AreaCoberta).HasColumnName("AreaCoberta");
      builder.Property(t => t.AreaTotal).HasColumnName("AreaTotal");
      builder.Property(t => t.Empregados).HasColumnName("Empregados");
      builder.Property(t => t.ContaId).HasColumnName("ContaId");
      builder.Property(t => t.Inicio).HasColumnName("Inicio");
      builder.Property(t => t.Termino).HasColumnName("Termino");
      builder.Property(t => t.Efluentes).HasColumnName("Efluentes").IsRequired();
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Relationships (Foreign Keys)
      builder.HasOne(p => p.Conta)
          .WithMany(f => f.EInstalacoes).HasForeignKey(k => k.ContaId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.FInstalacao)
          .WithMany(f => f.EInstalacoes).HasForeignKey(k => k.PropositoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Instalacao)
          .WithMany(f => f.EInstalacoes).HasForeignKey(k => k.InstalacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
