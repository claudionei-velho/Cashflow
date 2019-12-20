using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class CargoMap : IEntityTypeConfiguration<Cargo> {
    public void Configure(EntityTypeBuilder<Cargo> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Cargos");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Codigo).HasColumnName("Codigo")
          .IsRequired().HasMaxLength(8);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Titulo).HasColumnName("Titulo")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Cbo).HasColumnName("Cbo").HasMaxLength(8);
      builder.Property(t => t.Descricao).HasColumnName("Descricao").HasMaxLength(512);
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Cargos).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
