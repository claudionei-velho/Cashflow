using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class AFunilariaMap : IEntityTypeConfiguration<AFunilaria> {
    public void Configure(EntityTypeBuilder<AFunilaria> builder) {
      // Primary Key
      builder.HasKey(x => x.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("AFunilaria", "opc");
      builder.Property(x => x.Id).HasColumnName("Id");
      builder.Property(x => x.InstalacaoId).HasColumnName("InstalacaoId").IsRequired();
      builder.Property(x => x.Area).HasColumnName("Area").HasColumnType("decimal(18, 3)");
      builder.Property(x => x.Isolada).HasColumnName("Isolada").IsRequired();
      builder.Property(x => x.PPoluicao).HasColumnName("PPoluicao");
      builder.Property(x => x.Exaustao).HasColumnName("Exaustao");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.EInstalacao)
          .WithMany(f => f.Funilarias).HasForeignKey(k => k.InstalacaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
