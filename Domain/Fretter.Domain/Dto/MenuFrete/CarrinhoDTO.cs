using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.MenuFrete
{
    public class CarrinhoDTO
    {
        public string cdPedido { get; set; }
        public string cepDestino { get; set; }
        public string cepOrigem { get; set; }
        public int seller_id { get; set; }
        public string bandeira { get; set; }
        public string tipoServico { get; set; }
        public decimal? vlrCarrinho { get; set; }
        public string canalVenda { get; set; }
        public string cdCliente { get; set; }
        public virtual CarrinhoItemDTO[] itens { get; set; }
        public int? microServicoId { get; set; }
    }
    public class CarrinhoItemDTO
    {
        public string cdItem { get; set; }
        public string cnpjCanal { get; set; }
        public string cdCanal { get; set; }
        public string sku { get; set; }
        public decimal? peso { get; set; }
        public decimal? altura { get; set; }
        public decimal? largura { get; set; }
        public decimal? comprimento { get; set; }
        public decimal? m3 { get; set; }
        public string categoria { get; set; }
        public string departamento { get; set; }
        public decimal? qtdItem { get; set; }
        public decimal? vlrItem { get; set; }
    }
}
