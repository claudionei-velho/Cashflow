using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class NfEntregaMap : IEntityTypeConfiguration<NfEntrega> {
    public void Configure(EntityTypeBuilder<NfEntrega> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("NfEntregas", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.NotaId).HasColumnName("NotaId").IsRequired();      
      builder.Property(t => t.Endereco).HasColumnName("Endereco")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.EnderecoNo).HasColumnName("EnderecoNo").HasMaxLength(8);
      builder.Property(t => t.Complemento).HasColumnName("Complemento").HasMaxLength(64);
      builder.Property(t => t.Bairro).HasColumnName("Bairro").HasMaxLength(32);
      builder.Property(t => t.MunicipioId).HasColumnName("MunicipioId").IsRequired();
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Municipio)
          .WithMany(f => f.NfEntregas).HasForeignKey(k => k.MunicipioId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.NFiscal)
          .WithMany(f => f.NfEntregas).HasForeignKey(k => k.NotaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);      
    }
  }
}
