using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class FaturaCicloMap : IEntityTypeConfiguration<FaturaCiclo>
    {
        public void Configure(EntityTypeBuilder<FaturaCiclo> builder)
        {
            builder.ToTable("FaturaCiclo", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(FaturaCiclo)}Id").HasColumnType("int");
			builder.Property(e => e.FaturaCicloTipoId).HasColumnType("int");
            builder.Property(e => e.DiaVencimento).HasColumnType("smallint");
            builder.Property(e => e.DiaFechamento).HasColumnType("smallint");

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnType("bit").HasMaxLength(1);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
