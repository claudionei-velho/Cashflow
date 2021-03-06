﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class SetorMap : IEntityTypeConfiguration<Setor> {
    public void Configure(EntityTypeBuilder<Setor> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("Setores");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Codigo).HasColumnName("Codigo")
          .IsRequired().HasMaxLength(8);

      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Descricao).HasColumnName("Descricao").HasMaxLength(256);
      builder.Property(t => t.ResponsavelId).HasColumnName("ResponsavelId");
      builder.Property(t => t.VinculoId).HasColumnName("VinculoId");
      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Cargo)
          .WithMany(f => f.Setores).HasForeignKey(k => k.ResponsavelId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Empresa)
          .WithMany(f => f.Setores).HasForeignKey(k => k.EmpresaId).IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.Vinculo)
          .WithMany(f => f.Setores).HasForeignKey(k => k.VinculoId)
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
