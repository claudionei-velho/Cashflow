using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class BaciaMap : IEntityTypeConfiguration<Bacia> {
    public void Configure(EntityTypeBuilder<Bacia> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Bacias");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.MunicipioId).HasColumnName("MunicipioId").IsRequired();
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Descricao).HasColumnName("Descricao").HasMaxLength(256);
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Relationships
      builder.HasOne(t => t.Municipio)
          .WithMany(f => f.Bacias).HasForeignKey(k => k.MunicipioId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
