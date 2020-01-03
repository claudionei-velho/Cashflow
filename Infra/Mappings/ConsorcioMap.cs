using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class ConsorcioMap : IEntityTypeConfiguration<Consorcio> {
    public void Configure(EntityTypeBuilder<Consorcio> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);        

      // Table, Properties & Column Mappings
      builder.ToTable("Consorcios");

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
      builder.Property(t => t.MunicipioId).HasColumnName("MunicipioId").IsRequired();
      builder.Property(t => t.UfId).HasColumnName("UfId")
          .IsRequired().IsFixedLength().HasMaxLength(2);

      builder.Property(t => t.PaisId).HasColumnName("PaisId")
          .IsRequired().HasMaxLength(8);

      builder.Property(t => t.Telefone).HasColumnName("Telefone").HasMaxLength(32);
      builder.Property(t => t.Email).HasColumnName("Email").HasMaxLength(256);
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Municipio)
          .WithMany(f => f.Consorcios).HasForeignKey(k => k.MunicipioId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Pais)
          .WithMany(f => f.Consorcios).HasForeignKey(k => k.PaisId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
