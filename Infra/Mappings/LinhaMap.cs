using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class LinhaMap : IEntityTypeConfiguration<Linha> {
    public void Configure(EntityTypeBuilder<Linha> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Linhas", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Prefixo).HasColumnName("Prefixo")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(128);

      builder.Property(t => t.Uteis).HasColumnName("Uteis");
      builder.Property(t => t.Sabados).HasColumnName("Sabados");
      builder.Property(t => t.Domingos).HasColumnName("Domingos");
      builder.Property(t => t.DominioId).HasColumnName("DominioId").IsRequired();
      builder.Property(t => t.OperacaoId).HasColumnName("OperacaoId").IsRequired();
      builder.Property(t => t.Classificacao).HasColumnName("Classificacao").IsRequired();
      builder.Property(t => t.Captacao).HasColumnName("Captacao");
      builder.Property(t => t.Transporte).HasColumnName("Transporte");
      builder.Property(t => t.Distribuicao).HasColumnName("Distribuicao");
      builder.Property(t => t.ExtensaoAB).HasColumnName("ExtensaoAB").HasColumnType("decimal(18, 3)");
      builder.Property(t => t.ExtensaoBA).HasColumnName("ExtensaoBA").HasColumnType("decimal(18, 3)");
      builder.Property(t => t.LoteId).HasColumnName("LoteId");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.CLinha)
          .WithMany(f => f.Linhas).HasForeignKey(k => k.Classificacao).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.EDominio)
          .WithMany(f => f.Linhas).HasForeignKey(k => k.DominioId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Linhas).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Lote)
          .WithMany(f => f.Linhas).HasForeignKey(k => k.LoteId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Operacao)
          .WithMany(f => f.Linhas).HasForeignKey(k => k.OperacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
