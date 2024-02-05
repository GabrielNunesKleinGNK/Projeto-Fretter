using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaIntegracaoMap : IEntityTypeConfiguration<EmpresaIntegracao>
    {
        public void Configure(EntityTypeBuilder<EmpresaIntegracao> builder)
        {
            builder.ToTable("Tb_Adm_EmpresaIntegracao", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.CanalVendaId).HasColumnName("Id_CanalVenda").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.URLBase).HasColumnName("Ds_URLBase").HasColumnType("varchar").HasMaxLength(2048);
			builder.Property(e => e.URLToken).HasColumnName("Ds_URLToken").HasColumnType("varchar").HasMaxLength(2048);
			builder.Property(e => e.ApiKey).HasColumnName("Ds_ApiKey").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.Usuario).HasColumnName("Ds_Usuario").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.Senha).HasColumnName("Ds_Senha").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.ClientId).HasColumnName("Ds_ClientId").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.ClientSecret).HasColumnName("Ds_ClientSecret").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.ClientScope).HasColumnName("Ds_ClientScope").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.EntregaOrigemImportacaoId).HasColumnName("Id_EntregaOrigemImportacao").HasColumnType("int").HasMaxLength(4);

			builder.HasMany<EmpresaIntegracaoItem>(e => e.ListaIntegracoes).WithOne(s => s.EmpresaIntegracao);

            builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
		}
    }
}