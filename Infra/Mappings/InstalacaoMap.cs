using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings {
  internal class InstalacaoMap : IEntityTypeConfiguration<Instalacao> {
    public void Configure(EntityTypeBuilder<Instalacao> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Instalacoes", "opc");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Prefixo).HasColumnName("Prefixo").HasMaxLength(16);
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Endereco).HasColumnName("Endereco")
          .IsRequired().HasMaxLength(128);

      builder.Property(t => t.EnderecoNo).HasColumnName("EnderecoNo").HasMaxLength(8);
      builder.Property(t => t.Complemento).HasColumnName("Complemento").HasMaxLength(64);
      builder.Property(t => t.Cep).HasColumnName("Cep");
      builder.Property(t => t.Bairro).HasColumnName("Bairro").HasMaxLength(32);
      builder.Property(t => t.Municipio).HasColumnName("Municipio")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.UfId).HasColumnName("UfId").IsRequired()
          .IsFixedLength().HasMaxLength(2);

      builder.Property(t => t.Telefone).HasColumnName("Telefone").HasMaxLength(32);
      builder.Property(t => t.Email).HasColumnName("Email").HasMaxLength(256);
      builder.Property(t => t.Latitude).HasColumnName("Latitude").HasColumnType("decimal(24, 12)");
      builder.Property(t => t.Longitude).HasColumnName("Longitude").HasColumnType("decimal(24, 12)");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(p => p.Empresa)
          .WithMany(f => f.Instalacoes).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
