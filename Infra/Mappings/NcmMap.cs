using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class NcmMap : IEntityTypeConfiguration<Ncm> {
    public void Configure(EntityTypeBuilder<Ncm> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Ncm", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Classificacao).HasColumnName("Classificacao")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Descricao).HasColumnName("Descricao")
          .IsRequired().HasMaxLength(256);

      builder.Property(t => t.Ipi).HasColumnName("Ipi").HasColumnType("decimal(9, 6)");
      builder.Property(t => t.Vigente).HasColumnName("Vigente").IsRequired();
      builder.Property(t => t.GrupoId).HasColumnName("GrupoId");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Agrupamento)
          .WithMany(f => f.Agrupamentos).HasForeignKey(k => k.GrupoId)
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
