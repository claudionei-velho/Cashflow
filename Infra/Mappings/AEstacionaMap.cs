using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class AEstacionaMap : IEntityTypeConfiguration<AEstaciona> {
    public void Configure(EntityTypeBuilder<AEstaciona> builder) {
      // Primary Key
      builder.HasKey(x => x.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("AEstaciona", "opc");
      builder.Property(x => x.Id).HasColumnName("Id");
      builder.Property(x => x.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(x => x.Frota).HasColumnName("Frota").IsRequired();
      builder.Property(x => x.Requisitom2).HasColumnName("Requisitom2")
          .IsRequired().HasColumnType("decimal(18, 3)");

      builder.Property(x => x.Minimom2).HasColumnName("Minimom2")
          .IsRequired().HasColumnType("decimal(18, 3)");

      builder.Property(x => x.Disponivelm2).HasColumnName("Disponivelm2")
          .IsRequired().HasColumnType("decimal(18, 3)");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.EInstalacao)
          .WithMany(f => f.Estacionamentos).HasForeignKey(k => k.InstalacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
