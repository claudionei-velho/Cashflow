using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class PCoeficienteMap : IEntityTypeConfiguration<PCoeficiente> {
    public void Configure(EntityTypeBuilder<PCoeficiente> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("PCoeficientes", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.Reserva).HasColumnName("Reserva");
      builder.Property(t => t.Improdutiva).HasColumnName("Improdutiva");
      builder.Property(t => t.Lubrificante).HasColumnName("Lubrificante");
      builder.Property(t => t.UtilPneus).HasColumnName("UtilPneus");
      builder.Property(t => t.Recapagens).HasColumnName("Recapagens");
      builder.Property(t => t.Pecas).HasColumnName("Pecas");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.Empresa)
          .WithMany(f => f.PCoeficientes).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
