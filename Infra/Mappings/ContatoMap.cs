using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ContatoMap : IEntityTypeConfiguration<Contato> {
    public void Configure(EntityTypeBuilder<Contato> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Contatos");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Nome).HasColumnName("Nome")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Cargo).HasColumnName("Cargo").HasMaxLength(32);

      builder.Property(t => t.Telefone).HasColumnName("Telefone")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.Email).HasColumnName("Email").HasMaxLength(256);
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Contatos).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
