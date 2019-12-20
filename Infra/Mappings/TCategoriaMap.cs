using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class TCategoriaMap : IEntityTypeConfiguration<TCategoria> {
    public void Configure(EntityTypeBuilder<TCategoria> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("TCategorias", "opc");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Gratuidade).HasColumnName("Gratuidade").IsRequired();
      builder.Property(t => t.Rateio).HasColumnName("Rateio").HasColumnType("decimal(9, 6)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.TCategorias).HasForeignKey(k => k.EmpresaId).IsRequired()       
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
