﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class CstCarroceriaMap : IEntityTypeConfiguration<CstCarroceria> {
    public void Configure(EntityTypeBuilder<CstCarroceria> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);

      // Table, Properties & Column Mappings
      builder.ToTable("CstCarrocerias", "cst");

      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.EmpresaId).HasColumnName("EmpresaId").IsRequired();
      builder.Property(t => t.Ano).HasColumnName("Ano").IsRequired();
      builder.Property(t => t.Mes).HasColumnName("Mes").IsRequired();
      builder.Property(t => t.ClasseId).HasColumnName("ClasseId").IsRequired();

      builder.Property(t => t.Marca).HasColumnName("Marca")
        .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Modelo).HasColumnName("Modelo")
        .IsRequired().HasMaxLength(64);

      builder.Property(t => t.UnitAr).HasColumnName("UnitAr");
      builder.Property(t => t.UnitElevatoria).HasColumnName("UnitElevatoria");
      builder.Property(t => t.Unitario).HasColumnName("Unitario")
        .IsRequired().HasColumnType("money");

      builder.Property(t => t.Cadastro).HasColumnName("Cadastro");

      // Foreign Keys (Relationships)
      builder.HasOne(t => t.Empresa)
        .WithMany(f => f.CstCarrocerias).HasForeignKey(k => k.EmpresaId).IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(t => t.CVeiculo)
        .WithMany(f => f.CstCarrocerias).HasForeignKey(k => k.ClasseId).IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

    }
  }
}
