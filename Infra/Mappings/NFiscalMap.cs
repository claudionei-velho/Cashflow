using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class NFiscalMap : IEntityTypeConfiguration<NFiscal> {
    public void Configure(EntityTypeBuilder<NFiscal> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("NFiscais", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.FornecedorId).HasColumnName("FornecedorId").IsRequired();
      builder.Property(t => t.ChaveNfe).HasColumnName("ChaveNfe")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Natureza).HasColumnName("Natureza")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.FPagamentoId).HasColumnName("FPagamentoId").IsRequired();
      builder.Property(t => t.Modelo).HasColumnName("Modelo").IsRequired();
      builder.Property(t => t.Serie).HasColumnName("Serie").IsRequired();
      builder.Property(t => t.Numero).HasColumnName("Numero").IsRequired();
      builder.Property(t => t.Emissao).HasColumnName("Emissao").IsRequired();
      builder.Property(t => t.DataHora).HasColumnName("DataHora");
      builder.Property(t => t.Operacao).HasColumnName("Operacao").IsRequired();
      builder.Property(t => t.Digito).HasColumnName("Digito").IsRequired();
      builder.Property(t => t.Finalidade).HasColumnName("Finalidade").IsRequired();
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.NFiscais).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Fornecedor)
          .WithMany(f => f.NFiscais).HasForeignKey(k => k.FornecedorId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
