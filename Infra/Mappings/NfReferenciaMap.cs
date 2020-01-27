using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class NfReferenciaMap : IEntityTypeConfiguration<NfReferencia> {
    public void Configure(EntityTypeBuilder<NfReferencia> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("NfReferencias", "nfe");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.NotaId).HasColumnName("NotaId").IsRequired();      
      builder.Property(t => t.ChaveNfeRef).HasColumnName("ChaveNfeRef")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.AnoMes).HasColumnName("AnoMes").IsRequired();
      builder.Property(t => t.Emitente).HasColumnName("Emitente")
          .IsRequired().HasMaxLength(16);

      builder.Property(t => t.FornecedorId).HasColumnName("FornecedorId");
      builder.Property(t => t.Modelo).HasColumnName("Modelo").IsRequired();
      builder.Property(t => t.Serie).HasColumnName("Serie").IsRequired();
      builder.Property(t => t.Numero).HasColumnName("Numero").IsRequired();

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Fornecedor)
          .WithMany(f => f.NfReferencias).HasForeignKey(k => k.FornecedorId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.NFiscal)
          .WithMany(f => f.NfReferencias).HasForeignKey(k => k.NotaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
