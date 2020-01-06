using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class FrotaHorariaMap : IEntityTypeConfiguration<FrotaHoraria> {
    public void Configure(EntityTypeBuilder<FrotaHoraria> builder) {
      // Primary Key
      builder.HasKey(t => new { t.EmpresaId, t.Hora });

      // Table, Properties & Column Mappings
      builder.ToTable("FrotaHoraria", "opc");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Hora).HasColumnName("Hora").IsRequired();
      builder.Property(t => t.Faixa).HasColumnName("Faixa");
      builder.Property(t => t.Viagens).HasColumnName("Viagens");
      builder.Property(t => t.Veiculos).HasColumnName("Veiculos");
      builder.Property(t => t.KmTotal).HasColumnName("KmTotal");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.FrotaHorarias).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
