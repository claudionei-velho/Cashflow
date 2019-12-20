using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class DepartamentoMap : IEntityTypeConfiguration<Departamento> {
    public void Configure(EntityTypeBuilder<Departamento> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Departamentos");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.SetorId).HasColumnName("SetorId").IsRequired();
      builder.Property(t => t.Codigo).HasColumnName("Codigo")
          .IsRequired().HasMaxLength(8);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Descricao).HasColumnName("Descricao").HasMaxLength(256);
      builder.Property(t => t.ResponsavelId).HasColumnName("ResponsavelId");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Cargo)
          .WithMany(f => f.Departamentos).HasForeignKey(k => k.ResponsavelId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Setor)
          .WithMany(f => f.Departamentos).HasForeignKey(k => k.SetorId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
