using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ConfiguracaoCteTipoMap : IEntityTypeConfiguration<ConfiguracaoCteTipo>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoCteTipo> builder)
        {
            builder.ToTable(nameof(ConfiguracaoCteTipo), "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ConfiguracaoCteTipo)}Id").HasColumnType("int");
			builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.Chave).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Ativo).HasColumnType("bit");

            //BaseMapping
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao); 
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

        }
    }
}
