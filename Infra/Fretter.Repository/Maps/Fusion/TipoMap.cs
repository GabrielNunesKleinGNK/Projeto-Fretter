using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Repository.Maps
{
    class TipoMap : IEntityTypeConfiguration<Tipo>
    {
        public void Configure(EntityTypeBuilder<Tipo> builder)
        {
            builder.ToTable("Tb_Adm_TipoServico", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("tinyint").HasMaxLength(4);
            builder.Property(e => e.Descricao).HasColumnName("Ds_Tipo").HasColumnType("varchar").HasMaxLength(100);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);
        }
    }
}
