using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class FaturaStatusMap : IEntityTypeConfiguration<FaturaStatus>
    {
        public void Configure(EntityTypeBuilder<FaturaStatus> builder)
        {
            builder.ToTable("FaturaStatus", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(FaturaStatus)}Id").HasColumnType("int");
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Icon).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.IconColor).HasColumnType("varchar").HasMaxLength(16);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
