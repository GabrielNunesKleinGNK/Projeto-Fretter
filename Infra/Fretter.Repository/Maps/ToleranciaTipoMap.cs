using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ToleranciaTipoMap : IEntityTypeConfiguration<ToleranciaTipo>
    {
        public void Configure(EntityTypeBuilder<ToleranciaTipo> builder)
        {
            builder.ToTable(nameof(ToleranciaTipo), "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ToleranciaTipo)}Id").HasColumnType("int");
			builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.Ativo).HasColumnType("bit");

            //BaseMapping
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao); 
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

        }
    }
}
