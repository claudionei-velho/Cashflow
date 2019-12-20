using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class SistFuncaoMap : IEntityTypeConfiguration<SistFuncao> {
    public void Configure(EntityTypeBuilder<SistFuncao> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("SistFuncoes");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.SistemaId).HasColumnName("SistemaId").IsRequired();
      builder.Property(t => t.Item).HasColumnName("Item").IsRequired();
      builder.Property(t => t.FuncaoId).HasColumnName("FuncaoId").IsRequired();
      builder.Property(t => t.Quantidade).HasColumnName("Quantidade")
          .IsRequired().HasColumnType("decimal(24, 6)");

      builder.Property(t => t.SalBase).HasColumnName("SalBase")
          .IsRequired().HasColumnType("money");

      builder.Property(t => t.Encargos).HasColumnName("Encargos").HasColumnType("decimal(24, 6)");
      builder.Property(t => t.Beneficios).HasColumnName("Beneficios").HasColumnType("decimal(24, 6)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.ESistema)
          .WithMany(f => f.SistFuncoes).HasForeignKey(k => k.SistemaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(p => p.Funcao)
          .WithMany(f => f.SistFuncoes).HasForeignKey(k => k.FuncaoId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
