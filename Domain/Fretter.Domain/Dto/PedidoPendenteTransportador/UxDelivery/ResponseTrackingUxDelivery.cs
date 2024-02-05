using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.PedidoPendenteTransportador.UxDelivery
{
    public class ResponseTrackingUxDelivery
    {
        public bool FlagErro { get; set; }
        public bool FlagInfo { get; set; }
        public List<string> ListaMensagens { get; set; }
        public List<Resultado> ListaResultados { get; set; }

    }

    public class Resultado
    {
        public int IdOcorrencia { get; set; }
        public int IdNotaFiscal { get; set; }
        public string Cnpj { get; set; }
        public string CnpjRemetente { get; set; }
        public int Servico { get; set; }
        public DateTime DtOcorrencia { get; set; }
        public string CodigoOcorrencia { get; set; }
        public int IdStatusSolicitacao { get; set; }
        public string DescricaoOcorrencia { get; set; }
        public string NroNotaFiscal { get; set; }
        public string SerieNotaFiscal { get; set; }
        public string NroPedido { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string UnidadeOcorrencia { get; set; }
        public string NomeCidade { get; set; }
        public string UF { get; set; }
    }
}
