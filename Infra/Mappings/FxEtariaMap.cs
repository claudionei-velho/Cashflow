using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class FxEtariaMap : IEntityTypeConfiguration<FxEtaria> {
    public void Configure(EntityTypeBuilder<FxEtaria> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("FxEtarias", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Minimo).HasColumnName("Minimo").IsRequired();
      builder.Property(t => t.Maximo).HasColumnName("Maximo").IsRequired();
    }
  }
}
