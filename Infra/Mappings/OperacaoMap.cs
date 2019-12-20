using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class OperacaoMap : IEntityTypeConfiguration<Operacao> {
    public void Configure(EntityTypeBuilder<Operacao> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Operacoes", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.OpLinhaId).HasColumnName("OperLinhaId").IsRequired();
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Operacoes).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.OpLinha)
          .WithMany(f => f.Operacoes).HasForeignKey(k => k.OpLinhaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
