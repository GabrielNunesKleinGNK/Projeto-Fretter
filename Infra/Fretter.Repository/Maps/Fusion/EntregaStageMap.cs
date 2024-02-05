using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaStageMap : IEntityTypeConfiguration<EntregaStage>
    {
        public void Configure(EntityTypeBuilder<EntregaStage> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStage", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.CanalVenda).HasColumnName("Id_CanalVenda").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Empresa).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Canal).HasColumnName("Id_Canal").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaMarketplace).HasColumnName("Id_EmpresaMarketplace").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Transportador).HasColumnName("Id_Transportador").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.TransportadorCnpj).HasColumnName("Id_TransportadorCnpj").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.MicroServico).HasColumnName("Id_MicroServico").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.TipoServico).HasColumnName("Id_TipoServico").HasColumnType("tinyint").HasMaxLength(1);
			builder.Property(e => e.Lojista).HasColumnName("Id_Lojista").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.PLP).HasColumnName("Id_PLP").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Danfe).HasColumnName("Cd_Danfe").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.EntregaEntrada).HasColumnName("Cd_EntregaEntrada").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.EntregaSaida).HasColumnName("Cd_EntregaSaida").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.Sro).HasColumnName("Cd_Sro").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.PedidoCriacao).HasColumnName("Dt_PedidoCriacao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Postagem).HasColumnName("Dt_Postagem").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.PrevistaEntrega).HasColumnName("Dt_PrevistaEntrega").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.PrazoTransportadorEstatico).HasColumnName("Vl_PrazoTransportadorEstatico").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.PrazoTransportadorDinamico).HasColumnName("Vl_PrazoTransportadorDinamico").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.PrazoCliente).HasColumnName("Vl_PrazoCliente").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Custo).HasColumnName("Vl_Custo").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Cobrado).HasColumnName("Vl_Cobrado").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Global).HasColumnName("Vl_Global").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.ServicoDisponivel).HasColumnName("Flg_ServicoDisponivel").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.ServicoDisponivelDescricao).HasColumnName("Ds_ServicoDisponivel").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.PostagemVerificada).HasColumnName("Flg_PostagemVerificada").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.EntregaImportada).HasColumnName("Flg_EntregaImportada").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Tabela).HasColumnName("Id_Tabela").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Tomador).HasColumnName("Ds_Tomador").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.Altura).HasColumnName("Vl_Altura").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Comprimento).HasColumnName("Vl_Comprimento").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Largura).HasColumnName("Vl_Largura").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Peso).HasColumnName("Vl_Peso").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Cubagem).HasColumnName("Vl_Cubagem").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Diametro).HasColumnName("Vl_Diametro").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Declarado).HasColumnName("Vl_Declarado").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Receita).HasColumnName("Vl_Receita").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Total).HasColumnName("Vl_Total").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.EtiquetaGerada).HasColumnName("Flg_EtiquetaGerada").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.DataEtiquetaGerada).HasColumnName("Dt_EtiquetaGerada").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.ValidadeInicioEtiqueta).HasColumnName("Dt_ValidadeInicioEtiqueta").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.ValidadeFimEtiqueta).HasColumnName("Dt_ValidadeFimEtiqueta").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Rastreada).HasColumnName("Flg_Rastreada").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Volume).HasColumnName("Nr_Volume").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.LinkEtiquetaPDF).HasColumnName("Ds_LinkEtiquetaPDF").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.LinkEtiquetaPNG).HasColumnName("Ds_LinkEtiquetaPNG").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.LinkEtiquetaZPL).HasColumnName("Ds_LinkEtiquetaZPL").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.UltimaAlteracao).HasColumnName("Dt_UltimaAlteracao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);


			builder.Ignore(e => e.DataAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.Destinatario);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.UsuarioCadastro);
		}
    }
}
