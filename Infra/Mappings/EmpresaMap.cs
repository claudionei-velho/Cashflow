using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class EmpresaMap : IEntityTypeConfiguration<Empresa> {
    public void Configure(EntityTypeBuilder<Empresa> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);        

      // Table, Properties & Column Mappings
      builder.ToTable("Empresas");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Razao).HasColumnName("Razao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Fantasia).HasColumnName("Fantasia")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Cnpj).HasColumnName("Cnpj")
          .IsRequired().HasMaxLength(32);

      builder.Property(t => t.IEstadual).HasColumnName("IEstadual").HasMaxLength(16);
      builder.Property(t => t.IMunicipal).HasColumnName("IMunicipal").HasMaxLength(16);
      builder.Property(t => t.Endereco).HasColumnName("Endereco")
          .IsRequired().HasMaxLength(128);

      builder.Property(t => t.EnderecoNo).HasColumnName("EnderecoNo").HasMaxLength(8);
      builder.Property(t => t.Complemento).HasColumnName("Complemento").HasMaxLength(64);
      builder.Property(t => t.Cep).HasColumnName("Cep");
      builder.Property(t => t.Bairro).HasColumnName("Bairro").HasMaxLength(32);
      builder.Property(t => t.Municipio).HasColumnName("Municipio").HasMaxLength(32);
      builder.Property(t => t.MunicipioId).HasColumnName("MunicipioId");
      builder.Property(t => t.UfId).HasColumnName("UfId")
          .IsRequired().IsFixedLength().HasMaxLength(2);

      builder.Property(t => t.PaisId).HasColumnName("PaisId")
          .IsRequired().HasMaxLength(8);

      builder.Property(t => t.Telefone).HasColumnName("Telefone").HasMaxLength(32);
      builder.Property(t => t.Email).HasColumnName("Email").HasMaxLength(256);
      builder.Property(t => t.Inicio).HasColumnName("Inicio");
      builder.Property(t => t.Termino).HasColumnName("Termino");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Cidade)
        .WithMany(f => f.Empresas).HasForeignKey(k => k.MunicipioId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Pais)
          .WithMany(f => f.Empresas).HasForeignKey(k => k.PaisId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
