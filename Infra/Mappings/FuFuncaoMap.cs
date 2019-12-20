using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class FuFuncaoMap : IEntityTypeConfiguration<FuFuncao> {
    public void Configure(EntityTypeBuilder<FuFuncao> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("FuFuncoes", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.FuncaoId).HasColumnName("FuncaoId").IsRequired();
      builder.Property(t => t.Titular).HasColumnName("Titular").IsRequired();
      builder.Property(t => t.Ferista).HasColumnName("Ferista");
      builder.Property(t => t.Folguista).HasColumnName("Folguista");
      builder.Property(t => t.Reserva).HasColumnName("Reserva");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.FuFuncoes).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Funcao)
          .WithMany(f => f.FuFuncoes).HasForeignKey(k => k.FuncaoId)
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
