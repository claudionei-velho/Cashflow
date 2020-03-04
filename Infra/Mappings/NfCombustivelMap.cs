using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class NfCombustivelMap : IEntityTypeConfiguration<NfCombustivel> {
    public void Configure(EntityTypeBuilder<NfCombustivel> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("NfCombustiveis", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.NotaId).HasColumnName("NotaId").IsRequired();
      builder.Property(t => t.ItemId).HasColumnName("ItemId").IsRequired();
      builder.Property(t => t.ProdutoId).HasColumnName("ProdutoId").IsRequired();
      builder.Property(t => t.MixGN).HasColumnName("MixGN").HasColumnType("decimal(9, 4)");
      builder.Property(t => t.Codif).HasColumnName("Codif");
      builder.Property(t => t.Quantidade).HasColumnName("Quantidade")
          .IsRequired().HasColumnType("decimal(24, 12)");

      builder.Property(t => t.UfConsumo).HasColumnName("UfConsumo")
          .IsRequired().IsFixedLength().HasMaxLength(2);

      builder.Property(t => t.BaseCide).HasColumnName("BaseCide")
          .IsRequired().HasColumnType("money");

      builder.Property(t => t.AliquotaCide).HasColumnName("AliquotaCide")
          .IsRequired().HasColumnType("decimal(12, 4)");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.AnpProduto)
          .WithMany(f => f.NfCombustiveis).HasForeignKey(k => k.ProdutoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.NFiscal)
          .WithMany(f => f.NfCOmbustiveis).HasForeignKey(k => k.NotaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
