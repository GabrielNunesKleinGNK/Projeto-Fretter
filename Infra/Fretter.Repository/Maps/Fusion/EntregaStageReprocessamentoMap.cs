using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaStageReprocessamentoMap : IEntityTypeConfiguration<EntregaStageReprocessamento>
    {
        public void Configure(EntityTypeBuilder<EntregaStageReprocessamento> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStageReprocessamento", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Entrega).HasColumnName("Cd_Entrega").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Processado).HasColumnName("Flg_Processado").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DataProcessamento).HasColumnName("Dt_Processamento").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.JsonEnviadoParaFila).HasColumnName("Ds_JsonEnviadoParaFila").HasColumnType("varchar(max)");
            builder.Property(e => e.EntregaStageStatusProcessamentoId).HasColumnName("Id_EntregaStageStatusProcessamento").HasColumnType("int").HasMaxLength(4);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);
        }
    }
}
