using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ConciliacaoTipoMap : IEntityTypeConfiguration<ConciliacaoTipo>
    {
        public void Configure(EntityTypeBuilder<ConciliacaoTipo> builder)
        {
            builder.ToTable("ConciliacaoTipo", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ConciliacaoTipo)}Id").HasColumnType("int");
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(64);            
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(256);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
