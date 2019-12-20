using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ProducaoMap : IEntityTypeConfiguration<Producao> {
    public void Configure(EntityTypeBuilder<Producao> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Producao", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.TarifariaId).HasColumnName("TarifariaId").IsRequired();
      builder.Property(t => t.LinhaId).HasColumnName("LinhaId");
      builder.Property(t => t.Passageiros).HasColumnName("Passageiros").IsRequired();
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Producoes).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Linha)
          .WithMany(f => f.Producoes).HasForeignKey(k => k.LinhaId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.TCategoria)
          .WithMany(f => f.Producoes).HasForeignKey(k => k.TarifariaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
