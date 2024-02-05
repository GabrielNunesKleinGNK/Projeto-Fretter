using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities.Fusion;

namespace Fretter.Repository.Maps.Fusion
{
    class TransportadorMap : IEntityTypeConfiguration<Transportador>
    {
        public void Configure(EntityTypeBuilder<Transportador> builder)
        {
            builder.ToTable("Tb_Adm_Transportador", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.DataInclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime");
            builder.Property(e => e.RazaoSocial).HasColumnName("Ds_RazaoSocial").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.NomeFantasia).HasColumnName("Ds_NomeFantasia").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");
            builder.Property(e => e.DataValidado).HasColumnName("Dt_Validado").HasColumnType("smalldatetime");
            builder.Property(e => e.TokenId).HasColumnName("Id_Token").HasColumnType("uniqueidentifier");
            builder.Property(e => e.RastreamentoConfigTipoId).HasColumnName("Id_RastreamentoConfigTipo").HasColumnType("tinyint");
            builder.Property(e => e.CalculoPrazo).HasColumnName("Flg_WsCalculoPrazo").HasColumnType("bit");
            builder.Property(e => e.OrigemImportacaoId).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint");
            builder.Property(e => e.Hibrido).HasColumnName("Flg_Hibrido").HasColumnType("bit");

            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.UsuarioCadastro);
        }
    }
}
