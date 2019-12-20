using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class SistDespesaMap : IEntityTypeConfiguration<SistDespesa> {
    public void Configure(EntityTypeBuilder<SistDespesa> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("SistDespesas");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.SistemaId).HasColumnName("SistemaId").IsRequired();
      builder.Property(t => t.Item).HasColumnName("Item").IsRequired();
      builder.Property(t => t.ContaId).HasColumnName("ContaId").IsRequired();
      builder.Property(t => t.Quantidade).HasColumnName("Quantidade")
          .IsRequired().HasColumnType("decimal(24, 6)");

      builder.Property(t => t.Unidade).HasColumnName("Unidade").HasMaxLength(8);
      builder.Property(t => t.ValorBase).HasColumnName("ValorBase")
          .IsRequired().HasColumnType("money");

      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.ESistema)
          .WithMany(f => f.SistDespesas).HasForeignKey(k => k.SistemaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Conta)
          .WithMany(f => f.SistDespesas).HasForeignKey(k => k.ContaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
