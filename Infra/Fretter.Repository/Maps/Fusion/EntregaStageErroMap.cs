using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaStageErroMap : IEntityTypeConfiguration<EntregaStageErro>
    {
        public void Configure(EntityTypeBuilder<EntregaStageErro> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStageErro", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Importacao).HasColumnName("Dt_Importacao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Arquivo).HasColumnName("Id_Arquivo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Retorno).HasColumnName("Ds_Retorno").HasColumnType("varchar(MAX)");
			builder.Property(e => e.JsonEntrada).HasColumnName("Ds_JsonEntrada").HasColumnType("varchar(MAX)");
			builder.Property(e => e.JsonProcessamento).HasColumnName("Ds_JsonProcessamento").HasColumnType("varchar(MAX)");
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(x => x.DataAlteracao);
            builder.Ignore(x => x.DataCadastro);
            builder.Ignore(x => x.UsuarioAlteracao);
            builder.Ignore(x => x.UsuarioCadastro);
        }
    }
}
