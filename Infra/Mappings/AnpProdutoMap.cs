using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class AnpProdutoMap : IEntityTypeConfiguration<AnpProduto> {
    public void Configure(EntityTypeBuilder<AnpProduto> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("AnpProdutos", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Informar).HasColumnName("Informar").IsRequired();
    }
  }
}
