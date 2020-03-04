using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class TarifaMap : IEntityTypeConfiguration<Tarifa> {
    public void Configure(EntityTypeBuilder<Tarifa> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Tarifas", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Referencia).HasColumnName("Referencia").IsRequired();
      builder.Property(t => t.Valor).HasColumnName("Valor")
          .IsRequired().HasColumnType("decimal(24, 6)");

      builder.Property(t => t.Decreto).HasColumnName("Decreto").HasMaxLength(128);

      // Relationships
      builder.HasOne(p => p.Empresa)
          .WithMany(f => f.Tarifas).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
