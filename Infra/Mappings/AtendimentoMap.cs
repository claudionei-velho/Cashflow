using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class AtendimentoMap : IEntityTypeConfiguration<Atendimento> {
    public void Configure(EntityTypeBuilder<Atendimento> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Atendimentos", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.LinhaId).HasColumnName("LinhaId").IsRequired();
      builder.Property(t => t.Prefixo).HasColumnName("Prefixo")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(128);

      builder.Property(t => t.Uteis).HasColumnName("Uteis").IsRequired();
      builder.Property(t => t.Sabados).HasColumnName("Sabados").IsRequired();
      builder.Property(t => t.Domingos).HasColumnName("Domingos").IsRequired();
      builder.Property(t => t.ExtensaoAB).HasColumnName("ExtensaoAB").HasColumnType("decimal(18, 3)");
      builder.Property(t => t.ExtensaoBA).HasColumnName("ExtensaoBA").HasColumnType("decimal(18, 3)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Linha)
          .WithMany(f => f.Atendimentos).HasForeignKey(k => k.LinhaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
