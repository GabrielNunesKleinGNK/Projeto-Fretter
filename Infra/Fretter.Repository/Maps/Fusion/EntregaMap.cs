using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Repository.Maps
{
    class EntregaMap : IEntityTypeConfiguration<Entrega>
    {
        public void Configure(EntityTypeBuilder<Entrega> builder)
        {
			builder.ToTable("Tb_Edi_Entrega", "dbo");
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Importacao).HasColumnName("Dt_Importacao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Canal).HasColumnName("Id_Canal").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaTransportador).HasColumnName("Id_EmpresaTransportador").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Finalizado).HasColumnName("Dt_Finalizado").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.Sro).HasColumnName("Cd_Sro").HasColumnType("varchar").HasMaxLength(60);
			builder.Property(e => e.CodigoEntrega).HasColumnName("Cd_Entrega").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.CodigoPedido).HasColumnName("Cd_Pedido").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.NotaFiscal).HasColumnName("Cd_NotaFiscal").HasColumnType("varchar").HasMaxLength(200);
			builder.Property(e => e.Serie).HasColumnName("Cd_Serie").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.CepOrigem).HasColumnName("Cd_CepOrigem").HasColumnType("char").HasMaxLength(8);
			builder.Property(e => e.CepDestino).HasColumnName("Cd_CepDestino").HasColumnType("char").HasMaxLength(8);
			builder.Property(e => e.CepPostagem).HasColumnName("Cd_CepPostagem").HasColumnType("char").HasMaxLength(8);
			builder.Property(e => e.DataPostagem).HasColumnName("Dt_Postagem").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.PrazoTransportador).HasColumnName("Dt_PrazoTransportador").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.PrevistaEntrega).HasColumnName("Dt_PrevistaEntrega").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.DataPesquisa).HasColumnName("Dt_Pesquisa").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.Contrato).HasColumnName("Ds_Contrato").HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.Postagem).HasColumnName("Tp_Postagem").HasColumnType("varchar").HasMaxLength(20);
			builder.Property(e => e.LocalPostagem).HasColumnName("Ds_LocalPostagem").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.Cidade).HasColumnName("Ds_Cidade").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.Uf).HasColumnName("Cd_Uf").HasColumnType("varchar").HasMaxLength(2);
			builder.Property(e => e.CanalVenda).HasColumnName("Id_CanalVenda").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.CategoriaEntrega).HasColumnName("Ds_CategoriaEntrega").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.TransportadorCnpj).HasColumnName("Id_TransportadorCnpj").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.PI).HasColumnName("Cd_PI").HasColumnType("varchar").HasMaxLength(20);
			builder.Property(e => e.DataAbertura).HasColumnName("Dt_PI_Abertura").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.ServicoCorreios).HasColumnName("Cd_ServicoCorreios").HasColumnType("varchar").HasMaxLength(10);
			builder.Property(e => e.Frete).HasColumnName("Vl_Frete").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Arquivo).HasColumnName("Id_Arquivo").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.PrazoDias).HasColumnName("Nm_PrazoDias").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaSabado).HasColumnName("Flg_EntregaSabado").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.ServicoCorreiosVerificado).HasColumnName("Fl_ServicoCorreiosVerificado").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.IsNULL).HasColumnName("Cd_PI__IsNULL").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Transportador).HasColumnName("Id_Transportador").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaMarketPlace).HasColumnName("Id_EmpresaMarketPlace").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.TipoServico).HasColumnName("Id_TipoServico").HasColumnType("tinyint").HasMaxLength(1);
			builder.Property(e => e.Devolucao).HasColumnName("Dt_Devolucao").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.PrazoDevolucao).HasColumnName("Dt_PrazoDevolucao").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.Ocorrencia).HasColumnName("Ds_Ocorrencia").HasColumnType("varchar").HasMaxLength(200);
			builder.Property(e => e.DataOcorrencia).HasColumnName("Dt_Ocorrencia").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.Sigla).HasColumnName("Nm_Sigla").HasColumnType("varchar").HasMaxLength(5);
			builder.Property(e => e.EntregaOcorrencia).HasColumnName("Id_EntregaOcorrencia").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DtDevolucaoVerificada).HasColumnName("Flg_DtDevolucaoVerificada").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.ImportacaoEmpresa).HasColumnName("Dt_ImportacaoEmpresa").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.ImportacaoMarketplace).HasColumnName("Dt_ImportacaoMarketplace").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.ImportacaoTransportador).HasColumnName("Dt_ImportacaoTransportador").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.CnpjTerceiro).HasColumnName("Cd_CnpjTerceiro").HasColumnType("varchar").HasMaxLength(14);
			builder.Property(e => e.NotaTerceiro).HasColumnName("Cd_NotaTerceiro").HasColumnType("varchar").HasMaxLength(200);
			builder.Property(e => e.SerieTerceiro).HasColumnName("Cd_SerieTerceiro").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.DtOcorrencia).HasColumnName("Nr_Diff_DtPostagem_DtOcorrencia").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.FinalizadoNr).HasColumnName("Dt_FinalizadoNr").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Alteracao).HasColumnName("Dt_Alteracao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.NfSerie).HasColumnName("Cd_NF_Serie").HasColumnType("varchar").HasMaxLength(221);
			builder.Property(e => e.MicroServico).HasColumnName("Id_MicroServico").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.AWB).HasColumnName("Cd_AWB").HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.Tabela).HasColumnName("Id_Tabela").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmissaoNF).HasColumnName("Dt_EmissaoNF").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.Danfe).HasColumnName("Cd_Danfe").HasColumnType("varchar").HasMaxLength(44);
			builder.Property(e => e.PostagemDATE).HasColumnName("Dt_PostagemDATE").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.TransportadorPadrao).HasColumnName("Flg_TransportadorPadrao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.CustoFrete).HasColumnName("Vl_CustoFrete").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.DataDin).HasColumnName("Dt_PrazoTransportador_Din").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.Din).HasColumnName("Nm_PrazoDias_Din").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.SroUnico).HasColumnName("Cd_SroUnico").HasColumnType("varchar").HasMaxLength(60);
			builder.Property(e => e.SroComprimento).HasColumnName("Cd_SroComprimento").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Rastreiar).HasColumnName("Flg_Rastreiar").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.SroTratado).HasColumnName("Cd_SroTratado").HasColumnType("varchar").HasMaxLength(60);
			builder.Property(e => e.Tempos).HasColumnName("Ds_Tempos").HasColumnType("varchar(max)");
			builder.Property(e => e.ImportacaoDATE).HasColumnName("Dt_ImportacaoDATE").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.EnviadoElastic).HasColumnName("Flg_EnviadoElastic").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.TransportadorAlias).HasColumnName("Id_TransportadorAlias").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Volumes).HasColumnName("Qtd_Volumes").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DropShipping).HasColumnName("Flg_DropShipping").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.EmpresaDRS).HasColumnName("Id_EmpresaDRS").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.CanalDRS).HasColumnName("Id_CanalDRS").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.NotaFiscalDRS).HasColumnName("Cd_NotaFiscalDRS").HasColumnType("varchar").HasMaxLength(200);
			builder.Property(e => e.SerieDRS).HasColumnName("Cd_SerieDRS").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.DanfeDRS).HasColumnName("Cd_DanfeDRS").HasColumnType("varchar").HasMaxLength(44);
			builder.Property(e => e.NfSerieDRS).HasColumnName("Cd_NF_SerieDRS").HasColumnType("varchar").HasMaxLength(300);
			builder.Property(e => e.CargaCliente).HasColumnName("Nr_CargaCliente").HasColumnType("int").HasMaxLength(4);

			builder.HasOne(e => e.Empresa).WithMany().HasForeignKey(e => e.EmpresaId).OnDelete(DeleteBehavior.Restrict);

			builder.Ignore(e => e.Ativo);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
		}
    }
}
