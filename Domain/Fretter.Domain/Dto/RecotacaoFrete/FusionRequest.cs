using System;
using System.Collections.Generic;

namespace Fretter.Domain.Dto.RecotacaoFrete
{
    public class FusionRequest
    {
        public FusionRequest(MenuFreteCotacao cotacao, string tipoServico)
        {
            canalVenda = cotacao.Ds_Canal;
            cdPedido = cotacao.Cd_CodigoPedido ?? "102030";
            cepOrigem = cotacao.Ds_CepOrigem;
            seller_Id = Convert.ToInt32(cotacao.Cd_SellerId);
            cepDestino = cotacao.Ds_CepDestino;
            this.tipoServico = tipoServico;
            serviceModality = cotacao.Ds_Modalidade;
            microServicoId = cotacao.Id_MicroServico;
            itens = new List<RequestItem>();
        }

        public string cepOrigem;
        public int seller_Id;
        public string serviceModality;
        public string canalVenda { get; set; }
        public string tipoServico { get; set; }
        public string cepDestino { get; set; }
        public string cdPedido { get; set; }
        public List<RequestItem> itens { get; set; }
        public decimal vlrCarrinho { get; set; }
        public int? microServicoId { get; set; }
    }

    public class RequestItem
    {

        public RequestItem(MenuFreteCotacao cotacao, int item = 0)
        {
            qtdItem = cotacao.Nr_Quantidade>0? cotacao.Nr_Quantidade:1;
            vlrItem = cotacao.Nr_Valor;
            sku = item == 0 ? cotacao.Ds_SKU ?? "1" : $"{cotacao.Ds_SKU}_{item}";
            cdItem = item == 0 ? cotacao.Ds_SKU?? "1" : $"{cotacao.Ds_SKU}_{item}";
            altura = cotacao.Nr_Altura;
            largura = cotacao.Nr_Largura;
            comprimento = cotacao.Nr_Comprimento;
            peso = cotacao.Nr_Peso;
        }

        public string sku { get; set; }
        public string cdItem { get; set; }
        public int qtdItem { get; set; }
        public decimal vlrItem { get; set; }
        public decimal peso { get; set; }
        public decimal comprimento { get; set; }
        public decimal largura { get; set; }
        public decimal altura { get; set; }
    }

}
