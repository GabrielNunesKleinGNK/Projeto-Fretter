using Fretter.Domain.Dto.Carrefour.Mirakl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Dto.Carrefour
{
    public class StageEtiquetaReciclagemFilaDTO
    {
        public int? Id { get; set; }
        public string Sro { get; set; }
        public int? PLP { get; set; }
        public DateTime? ValidadeInicioEtiqueta { get; set; }
        public DateTime? ValidadeFimEtiqueta { get; set; }
        public int? MicroServico { get; set; }
        public string EntregaEntrada { get; set; }
        public string EntregaSaida { get; set; }
        public bool? EtiquetaGerada { get; set; }
        public DateTime? DataEtiquetaGerada { get; set; }
        public string Danfe { get; set; }
        public int? Lojista { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Declarado { get; set; }
        public decimal? Altura { get; set; }
        public decimal? Largura { get; set; }
        public decimal? Comprimento { get; set; }
        public int? EmpresaMarketplace { get; set; }
        public string EmpresaMarketplaceCnpj { get; set; }
        public int? Tabela { get; set; }
        public decimal? Total { get; set; }
        public string TransportadorDescricao { get; set; }
        public DateTime? PrevistaEntrega { get; set; }


        public string EnderecoRemetente { get; set; }
        public string NumeroRemetente { get; set; }
        public string ComplementoRemetente { get; set; }
        public string BairroRemetente { get; set; }
        public string CepRemetente { get; set; }
        public string CidadeRemetente { get; set; }
        public string UFRemetente { get; set; }

        
        public string NomeDestinatario { get; set; }
        public string EnderecoDestinatario { get; set; }
        public string NumeroDestinatario { get; set; }
        public string ComplementoDestinatario { get; set; }
        public string BairroDestinatario { get; set; }
        public string CepDestinatario { get; set; }
        public string CidadeDestinatario { get; set; }
        public string UFDestinatario { get; set; }
    }
}
