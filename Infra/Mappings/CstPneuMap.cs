using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class CstPneuMap : IEntityTypeConfiguration<CstPneu> {
    public void Configure(EntityTypeBuilder<CstPneu> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("CstPneus", "cst");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.ClasseId).HasColumnName("ClasseId").IsRequired();
      builder.Property(t => t.UnitPneu).HasColumnName("UnitPneu")
          .IsRequired().HasColumnType("money");

      builder.Property(t => t.UnitRecap).HasColumnName("UnitRecap")
          .IsRequired().HasColumnType("money");

      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
        .WithMany(f => f.CstPneus).HasForeignKey(k => k.EmpresaId).IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.CVeiculo)
        .WithMany(f => f.CstPneus).HasForeignKey(k => k.ClasseId).IsRequired()
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
