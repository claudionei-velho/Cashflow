using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class ATrafegoMap : IEntityTypeConfiguration<ATrafego> {
    public void Configure(EntityTypeBuilder<ATrafego> builder) {
      // Primary Key
      builder.HasKey(x => x.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("ATrafego", "opc");
      builder.Property(x => x.Id).HasColumnName("Id");
      builder.Property(x => x.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(x => x.Plantao).HasColumnName("Plantao");
      builder.Property(x => x.Reserva).HasColumnName("Reserva");
      builder.Property(x => x.Equipamentos).HasColumnName("Equipamentos");
      builder.Property(x => x.Mobiliario).HasColumnName("Mobiliario");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.EInstalacao)
          .WithMany(f => f.Trafegos).HasForeignKey(k => k.InstalacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
