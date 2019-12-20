using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class MunicipioMap : IEntityTypeConfiguration<Municipio> {
    public void Configure(EntityTypeBuilder<Municipio> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Municipios");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.UfId).HasColumnName("UfId").IsRequired();
      builder.Property(t => t.Nome).HasColumnName("Nome")
          .IsRequired().HasMaxLength(64);

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Uf)
          .WithMany(f => f.Municipios).HasForeignKey(k => k.UfId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
