using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaTransporteConfiguracaoItemMap : IEntityTypeConfiguration<EmpresaTransporteConfiguracaoItem>
    {
        public void Configure(EntityTypeBuilder<EmpresaTransporteConfiguracaoItem> builder)
        {
            builder.ToTable("Tb_Adm_EmpresaTransporteConfiguracaoItem", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaTransporteConfiguracaoId).HasColumnName("Id_EmpresaTransporteConfiguracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CodigoServico).HasColumnName("Cd_CodigoServico").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CodigoServicoCategoria).HasColumnName("Cd_CodigoServicoCategoria").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.DataCadastroServico).HasColumnName("Dt_CadastroServico").HasColumnType("datetime").HasMaxLength(4);
            builder.Property(e => e.VigenciaInicial).HasColumnName("Dt_VigenciaInicial").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.VigenciaFinal).HasColumnName("Dt_VigenciaFinal").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("Ds_UsuarioAtualizacao").HasColumnType("smallint").HasMaxLength(4);
            builder.Property(e => e.DataAtualizacao).HasColumnName("Dt_Atualizacao").HasColumnType("datetime").HasMaxLength(4);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.UsuarioCadastro);
        }
    }
}
