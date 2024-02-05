using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaDevolucaoLogMap : IEntityTypeConfiguration<EntregaDevolucaoLog>
    {
        public void Configure(EntityTypeBuilder<EntregaDevolucaoLog> builder)
        {
            builder.ToTable("Tb_Edi_EntregaDevolucaoLog", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaDevolucaoTipoId).HasColumnName("Id_EntregaDevolucaoLogTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaDevolucaoId).HasColumnName("Id_EntregaDevolucao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.JsonEnvio).HasColumnName("Ds_JsonEnvio").HasColumnType("varchar(max)");
            builder.Property(e => e.JsonRetorno).HasColumnName("Ds_JsonRetorno").HasColumnType("varchar(max)");
            builder.Property(e => e.Observacao).HasColumnName("Ds_Observacao").HasColumnType("varchar").HasMaxLength(1024);
            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Sucesso).HasColumnName("Flg_Sucesso").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
