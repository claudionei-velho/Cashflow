using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ECVeiculoMap : IEntityTypeConfiguration<ECVeiculo> {
    public void Configure(EntityTypeBuilder<ECVeiculo> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("ECVeiculos", "opc");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.ClasseId).HasColumnName("ClasseId").IsRequired();      
      builder.Property(t => t.Minimo).HasColumnName("Minimo");
      builder.Property(t => t.Maximo).HasColumnName("Maximo");
      builder.Property(t => t.Passageirom2).HasColumnName("Passageirom2").IsRequired();
      builder.Property(t => t.Pneus).HasColumnName("Pneus").IsRequired();
      builder.Property(t => t.Util).HasColumnName("Util");
      builder.Property(t => t.Residual).HasColumnName("Residual").HasColumnType("decimal(9, 6)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.CVeiculo)
          .WithMany(f => f.ECVeiculos).HasForeignKey(k => k.ClasseId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.ECVeiculos).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
