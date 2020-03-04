using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class EConsorcioMap : IEntityTypeConfiguration<EConsorcio> {
    public void Configure(EntityTypeBuilder<EConsorcio> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("EConsorcios");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.ConsorcioId).HasColumnName("ConsorcioId").IsRequired();
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ratio).HasColumnName("Ratio")
          .IsRequired().HasColumnType("decimal(9, 6)");

      builder.Property(t => t.Integracao).HasColumnName("Integracao").IsRequired();
      builder.Property(t => t.Documento).HasColumnName("Documento");
      builder.Property(t => t.Desintegracao).HasColumnName("Desintegracao");
      builder.Property(t => t.Responsavel).HasColumnName("Responsavel");
      builder.Property(t => t.CpfResponsavel).HasColumnName("CpfResponsavel");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Consorcio)
          .WithMany(f => f.EConsorcios).HasForeignKey(k => k.ConsorcioId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.EConsorcios).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
