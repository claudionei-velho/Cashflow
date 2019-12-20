using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class SalarioMap : IEntityTypeConfiguration<Salario> {
    public void Configure(EntityTypeBuilder<Salario> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Salarios");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.FuncaoId).HasColumnName("FuncaoId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.SalBase).HasColumnName("SalBase")
          .IsRequired().HasColumnType("money");

      builder.Property(t => t.Encargos).HasColumnName("Encargos");
      builder.Property(t => t.Beneficios).HasColumnName("Beneficios");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Funcao)
          .WithMany(f => f.Salarios).HasForeignKey(k => k.FuncaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
