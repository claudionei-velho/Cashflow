using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class AAlmoxMap : IEntityTypeConfiguration<AAlmox> {
    public void Configure(EntityTypeBuilder<AAlmox> builder) {
      // Primary Key
      builder.HasKey(x => x.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("AAlmox", "opc");
      builder.Property(x => x.Id).HasColumnName("Id");
      builder.Property(x => x.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(x => x.Area).HasColumnName("Area").HasColumnType("decimal(18, 3)");
      builder.Property(x => x.Especifico).HasColumnName("Especifico").IsRequired();
      builder.Property(x => x.Estoque).HasColumnName("Estoque").IsRequired();

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.EInstalacao)
          .WithMany(f => f.Almoxarifados).HasForeignKey(c => c.InstalacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
