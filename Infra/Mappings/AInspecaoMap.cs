using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class AInspecaoMap : IEntityTypeConfiguration<AInspecao> {
    public void Configure(EntityTypeBuilder<AInspecao> builder) {
      // Primary Key
      builder.HasKey(x => x.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("AInspecao", "opc");
      builder.Property(x => x.Id).HasColumnName("Id");
      builder.Property(x => x.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(x => x.Rampas).HasColumnName("Rampas").IsRequired();

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.EInstalacao)
          .WithMany(f => f.Inspecoes).HasForeignKey(k => k.InstalacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
