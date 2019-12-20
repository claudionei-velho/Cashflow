using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class AAbasteceMap : IEntityTypeConfiguration<AAbastece> {
    public void Configure(EntityTypeBuilder<AAbastece> builder) {
      // Primary Key
      builder.HasKey(x => x.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("AAbastece", "opc");
      builder.Property(x => x.Id).HasColumnName("Id");
      builder.Property(x => x.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(x => x.PavimentoId).HasColumnName("PavimentoId").IsRequired();
      builder.Property(x => x.Bombas).HasColumnName("Bombas").IsRequired();

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.EInstalacao)
          .WithMany(f => f.Abastecimentos).HasForeignKey(k => k.InstalacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Via)
          .WithMany(f => f.Abastecimentos).HasForeignKey(c => c.PavimentoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
