using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class FaturaAcaoMap: IEntityTypeConfiguration<FaturaAcao>
    {
        public void Configure(EntityTypeBuilder<FaturaAcao> builder)
        {
            builder.ToTable("FaturaAcao", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(FaturaAcao)}Id").HasColumnType("int");
            builder.Property(e => e.Descricao).HasColumnName("Descricao").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
