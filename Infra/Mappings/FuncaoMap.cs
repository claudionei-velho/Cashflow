using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class FuncaoMap : IEntityTypeConfiguration<Funcao> {
    public void Configure(EntityTypeBuilder<Funcao> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Funcoes");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.CargoId).HasColumnName("CargoId").IsRequired();
      builder.Property(t => t.DepartamentoId).HasColumnName("DepartamentoId").IsRequired();
      builder.Property(t => t.Titulo).HasColumnName("Titulo")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.CentroId).HasColumnName("CentroId");
      builder.Property(t => t.ContaId).HasColumnName("ContaId");
      builder.Property(t => t.Desvinculado).HasColumnName("Desvinculado");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Cargo)
          .WithMany(f => f.Funcoes).HasForeignKey(k => k.CargoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Centro)
          .WithMany(f => f.Funcoes).HasForeignKey(k => k.CentroId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Conta)
          .WithMany(f => f.Funcoes).HasForeignKey(k => k.ContaId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Departamento)
          .WithMany(f => f.Funcoes).HasForeignKey(k => k.DepartamentoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
