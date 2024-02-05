using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Fretter.Domain.Enum.Webhook.Enums;

namespace Fretter.Domain.Dto.Webhook.Entrega
{
    public class EntregaEnvioFila
    {
        public string Tp_SubidaEntrega { get; set; }
        public int? Id_Arquivo { get; set; }
        public int? Nr_QtdVolumes { get; set; }
        public string Ds_PackingList { get; set; }
        public int? numeroCargaCliente { get; set; }
        public Guid Ds_Hash { get; set; }
        public string Ds_ServicoTipo { get; set; }
        public string Cd_CnpjFilial { get; set; }
        public string Cd_CodigoFilial { get; set; }
        public string Cd_SegmentoFilial { get; set; }
        public string Cd_CnpjTransportador { get; set; }
        public long Nr_Linha { get; set; }
        public string Cd_NotaFiscal { get; set; }
        public string Cd_Serie { get; set; }
        public string Cd_Pedido { get; set; }
        public string Cd_Entrega { get; set; }
        public string Cd_CepOrigem { get; set; }
        public string Cd_CepPostagem { get; set; }
        public string Cd_CepDestino { get; set; }
        public DateTime? Dt_Finalizado { get; set; }
        public DateTime? Dt_Postagem { get; set; }
        public DateTime? Dt_PrevistaEntrega { get; set; }
        public DateTime? Dt_PrazoComercial { get; set; }
        public DateTime? Dt_EmissaoNF { get; set; }
        public string Cd_Rastreio { get; set; }
        public string Ds_Awb { get; set; }
        public string Cd_CodigoPLP { get; set; }
        public string Ds_CanalVendas { get; set; }
        public string Cd_Contrato { get; set; }
        public string Ds_CnpjTerceiro { get; set; }
        public string Cd_NotaTerceiro { get; set; }
        public string Cd_SerieTerceiro { get; set; }
        public decimal? Vl_Frete { get; set; }
        public decimal? Vl_CustoFrete { get; set; }
        [Obsolete]
        public string Ds_ServicoCorreio { get; set; }
        public string Ds_ServicoTransportador { get; set; }
        public string Ds_Cidade { get; set; }
        public string Cd_Uf { get; set; }
        public string Cd_Danfe { get; set; }
        public string Cd_CnpjMarketplace { get; set; }
        public string Ds_CanalVendaMarketplace { get; set; }
        public string Cd_EntregaEntrada { get; set; }
        public string Cd_EntregaSaida { get; set; }
        public int? Id_Lojista { get; set; }
        public string Cd_RastreioMarketplace { get; set; }
        public decimal? Vl_Peso { get; set; }
        public string Ds_FormatoEmbalagemCorreios { get; set; }
        public decimal? Vl_Comprimento { get; set; }
        public decimal? Vl_Altura { get; set; }
        public decimal? Vl_Largura { get; set; }
        public decimal? Vl_Diametro { get; set; }
        public bool? Flg_ServicoMaoPropria { get; set; }
        public bool? Flg_AvisoRecebimento { get; set; }
        public decimal? Vl_Declarado { get; set; }
        public string Ds_NomeDestinatario { get; set; }
        public string Nr_DocDestinatario { get; set; }
        public string Ds_InscricaoEstadual { get; set; }
        public string Ds_EnderecoNumero { get; set; }
        public string Ds_Endereco { get; set; }
        public string Ds_Bairro { get; set; }
        public string Ds_EnderecoComplemento { get; set; }
        public string Ds_EnderecoReferencia { get; set; }
        public string Nr_PreOrder { get; set; }
        public string Ds_Email { get; set; }
        public string Ds_Telefone1 { get; set; }
        public string Ds_Telefone2 { get; set; }
        public string Ds_Telefone3 { get; set; }
        public string Cd_CnpjPagador { get; set; }
        public Dictionary<byte, string> Parametros { get; set; }
        public Responsavel Responsavel { get; set; }
        public int Id_Empresa { get; set; }
        public List<EntregaItens> Itens { get; set; }
        public List<EntregaVolumes> Volumes { get; set; }
    }
    public class EntregaVolumes
    {
        public string Ds_Nome { get; set; }
        public string Ds_Codigo { get; set; }
        public string Ds_CodigoRastreio { get; set; }
        public int? Nr_Volume { get; set; }
        public int? Qtd_Itens { get; set; }
        public string Cd_NotaFiscal { get; set; }
        public string Cd_Serie { get; set; }
        public string Cd_Danfe { get; set; }
        public string Cd_CFOP { get; set; }                
        public decimal? Vl_Altura { get; set; }
        public decimal? Vl_Largura { get; set; }
        public decimal? Vl_Comprimento { get; set; }
        public decimal? Vl_Peso { get; set; }
        public decimal? Vl_PesoCubico { get; set; }
        public decimal? Vl_Declarado { get; set; }
        public decimal? Vl_Total { get; set; }
    }

    public class EntregaItens
    {
        public string Cd_Item { get; set; }
        public string Cd_ItemDetalhe { get; set; }
        public int? Qtd_Itens { get; set; }
        public decimal? Vl_FreteCliente { get; set; }
        public string Cd_Sku { get; set; }
        public decimal? Vl_Altura { get; set; }
        public decimal? Vl_Largura { get; set; }
        public decimal? Vl_Comprimento { get; set; }
        public decimal? Vl_PesoCubico { get; set; }
        public decimal? Vl_FreteCusto { get; set; }
        public decimal? Vl_PrecoItem { get; set; }
        public string Ds_CodigoVolume { get; set; }
        public string Ds_UrlImagem { get; set; }
        public string Ds_UrlNotaFiscal { get; set; }
        public decimal? Vl_Total { get; set; } 
    }

    public class LsEntrega : List<EntregaEnvioFila>
    {
        private int _maxItensPerConnection;

        public bool Enviar => _maxItensPerConnection == Count;

        public LsEntrega(int maxItensPerConnection) => _maxItensPerConnection = maxItensPerConnection;
    }
}
