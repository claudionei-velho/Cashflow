using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class PSinteseMap : IEntityTypeConfiguration<PSintese> {
    public void Configure(EntityTypeBuilder<PSintese> builder) {
      // Primary Key
      builder.HasKey(t => new { t.EmpresaId, t.DiaId });

      // Table, Properties & Column Mappings
      builder.ToTable("PlanoSintese", "opc");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.DiaId).HasColumnName("DiaId").IsRequired();
      builder.Property(t => t.Dias).HasColumnName("Dias");
      builder.Property(t => t.Viagens).HasColumnName("Viagens");
      builder.Property(t => t.Percurso).HasColumnName("Percurso").HasColumnType("decimal(36, 3)");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.PSinteses).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
