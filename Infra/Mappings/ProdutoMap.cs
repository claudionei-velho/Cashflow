using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ProdutoMap : IEntityTypeConfiguration<Produto> {
    public void Configure(EntityTypeBuilder<Produto> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Produtos", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Gtin).HasColumnName("Gtin")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Descricao).HasColumnName("Descricao")
          .IsRequired().HasMaxLength(128);

      builder.Property(t => t.NcmId).HasColumnName("NcmId").IsRequired();
      builder.Property(t => t.UnidadeId).HasColumnName("UnidadeId").IsRequired();
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Ncm)
          .WithMany(f => f.Produtos).HasForeignKey(k => k.NcmId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.UComercial)
          .WithMany(f => f.Produtos).HasForeignKey(k => k.UnidadeId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
