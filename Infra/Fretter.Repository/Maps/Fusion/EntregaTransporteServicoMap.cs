using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaTransporteServicoMap : IEntityTypeConfiguration<EntregaTransporteServico>
    {
        public void Configure(EntityTypeBuilder<EntregaTransporteServico> builder)
        {
            builder.ToTable("Tb_Edi_EntregaTransporteServico", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Empresa).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Transportador).HasColumnName("Id_Transportador").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Usuario).HasColumnName("Ds_Usuario").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Senha).HasColumnName("Ds_Senha").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.CodigoContrato).HasColumnName("Cd_CodigoContrato").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.CodigoCartao).HasColumnName("Cd_CodigoCartao").HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.URLBase).HasColumnName("Ds_URLBase").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
