using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class TarifaModMap : IEntityTypeConfiguration<TarifaMod> {
    public void Configure(EntityTypeBuilder<TarifaMod> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("TarifaMod", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Gratuidade).HasColumnName("Gratuidade").IsRequired();
      builder.Property(t => t.Rateio).HasColumnName("Rateio").HasColumnType("decimal(9, 3)");
      builder.Property(t => t.Tarifa).HasColumnName("Tarifa").HasColumnType("decimal(24, 6)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Relationships
      builder.HasOne(p => p.Empresa)
          .WithMany(f => f.TarifasMod).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
