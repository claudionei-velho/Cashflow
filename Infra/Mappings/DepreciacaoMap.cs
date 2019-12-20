using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class DepreciacaoMap : IEntityTypeConfiguration<Depreciacao> {
    public void Configure(EntityTypeBuilder<Depreciacao> builder) {
      // Primary Key
      builder.HasKey(t => new { t.ClasseId, t.EtariaId });

      // Table, Properties & Column Mappings
      builder.ToTable("Depreciacoes", "opc");

      builder.Property(t => t.ClasseId).HasColumnName("ClasseId").IsRequired();
      builder.Property(t => t.EtariaId).HasColumnName("EtariaId").IsRequired();
      builder.Property(t => t.Anos).HasColumnName("Anos");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.ECVeiculo)
          .WithMany(f => f.Depreciacoes).HasForeignKey(k => k.ClasseId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.FxEtaria)
          .WithMany(f => f.Depreciacoes).HasForeignKey(k => k.EtariaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
