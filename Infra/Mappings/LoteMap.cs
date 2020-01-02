using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class LoteMap : IEntityTypeConfiguration<Lote> {
    public void Configure(EntityTypeBuilder<Lote> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Lotes");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.BaciaId).HasColumnName("BaciaId").IsRequired();
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Relationships
      builder.HasOne(t => t.Bacia)
          .WithMany(f => f.Lotes).HasForeignKey(k => k.BaciaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
