using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class VeiculoMap : IEntityTypeConfiguration<Veiculo> {
    public void Configure(EntityTypeBuilder<Veiculo> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Veiculos", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Numero).HasColumnName("Numero")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Cor).HasColumnName("Cor")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Classe).HasColumnName("Classe").IsRequired();
      builder.Property(t => t.Categoria).HasColumnName("Categoria");
      builder.Property(t => t.Placa).HasColumnName("Placa")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Renavam).HasColumnName("Renavam")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Antt).HasColumnName("Antt").HasMaxLength(16);
      builder.Property(t => t.Inicio).HasColumnName("Inicio");
      builder.Property(t => t.Inativo).HasColumnName("Inativo");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.CVeiculo)
          .WithMany(f => f.Veiculos).HasForeignKey(k => k.Classe).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Veiculos).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
