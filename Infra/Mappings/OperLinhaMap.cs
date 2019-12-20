using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Models;

namespace Infra.Mappings {
  internal class OperLinhaMap : IEntityTypeConfiguration<OpLinha> {
    public void Configure(EntityTypeBuilder<OpLinha> builder) {
      // Primary Key
      builder.HasKey(t => t.Id);        

      // Table, Properties & Column Mappings
      builder.ToTable("OperLinhas");
      builder.Property(t => t.Id).HasColumnName("Id");
      builder.Property(t => t.Denominacao).HasColumnName("Denominacao")
          .IsRequired().HasMaxLength(64);

      builder.Property(t => t.Descricao).HasColumnName("Descricao").HasMaxLength(512);      
    }
  }
}
