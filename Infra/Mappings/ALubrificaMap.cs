using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class ALubrificaMap : IEntityTypeConfiguration<ALubrifica> {
    public void Configure(EntityTypeBuilder<ALubrifica> builder) {
      // Primary Key
      builder.HasKey(x => x.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("ALubrifica", "opc");
      builder.Property(x => x.Id).HasColumnName("Id");
      builder.Property(x => x.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(x => x.Lavacao).HasColumnName("Lavacao");
      builder.Property(x => x.Ceramico).HasColumnName("Ceramico").IsRequired();
      builder.Property(x => x.Protecao).HasColumnName("Protecao").IsRequired();

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.EInstalacao)
          .WithMany(f => f.Lubrificacoes).HasForeignKey(k => k.InstalacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
