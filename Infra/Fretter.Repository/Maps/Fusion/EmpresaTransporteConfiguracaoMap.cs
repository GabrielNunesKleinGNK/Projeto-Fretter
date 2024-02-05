using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaTransporteConfiguracaoMap : IEntityTypeConfiguration<EmpresaTransporteConfiguracao>
    {
        public void Configure(EntityTypeBuilder<EmpresaTransporteConfiguracao> builder)
        {
            builder.ToTable("Tb_Adm_EmpresaTransporteConfiguracao", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaTransporteTipoItemId).HasColumnName("Id_EmpresaTransporteTipoItem").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CodigoContrato).HasColumnName("Cd_CodigoContrato").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CodigoCartao).HasColumnName("Cd_CodigoCartao").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Usuario).HasColumnName("Ds_Usuario").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.Senha).HasColumnName("Ds_Senha").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.VigenciaInicial).HasColumnName("Dt_VigenciaInicial").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.VigenciaFinal).HasColumnName("Dt_VigenciaFinal").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.DiasValidade).HasColumnName("Nr_DiasValidade").HasColumnType("smallint").HasMaxLength(4);
            builder.Property(e => e.RetornoValidacao).HasColumnName("Ds_RetornoValidacao").HasColumnType("varchar").HasMaxLength(2048);
            builder.Property(e => e.DataValidacao).HasColumnName("Dt_Validacao").HasColumnType("datetime").HasMaxLength(4);
            builder.Property(e => e.Valido).HasColumnName("Flg_Valido").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Producao).HasColumnName("Flg_Producao").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.HasMany(d => d.EmpresaTransporteConfiguracaoItems).WithOne().HasForeignKey(d => d.EmpresaTransporteConfiguracaoId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.UsuarioCadastro);
        }
    }
}
