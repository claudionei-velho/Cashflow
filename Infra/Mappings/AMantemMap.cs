using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class AMantenMap : IEntityTypeConfiguration<AMantem> {
    public void Configure(EntityTypeBuilder<AMantem> builder) {
      // Primary Key
      builder.HasKey(x => x.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("AMantem", "opc");
      builder.Property(x => x.Id).HasColumnName("Id");
      builder.Property(x => x.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(x => x.Area).HasColumnName("Area").HasColumnType("decimal(18, 3)");
      builder.Property(x => x.PontosAr).HasColumnName("PontosAr");
      builder.Property(x => x.Eletricidade).HasColumnName("Eletricidade").IsRequired();
      builder.Property(x => x.Elevadores).HasColumnName("Elevadores");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.EInstalacao)
          .WithMany(f => f.Manutencoes).HasForeignKey(k => k.InstalacaoId)
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
