using System;

namespace Fretter.Domain.Dto.PedidoPendenteBSeller
{
    public class EntregaPedido
    {
        public string CdEntrega { get; set; }
        public int IdEntrega { get; set; }
        public int IdEmpresa { get; set; }
        public int IdTransportador { get; set; }
        public string Danfe { get; set; }
        public string DanfeDRS { get; set; }
        public string NotaFiscal { get; set; }
        public string NotaFiscalDRS { get; set; }
        public string CnpjCanal { get; set; }
        public string CnpjCanalDRS { get; set; }
        public int? PedidoPendenteIntegracaoId { get; set; }
        public string DescricaoMicroServico { get; set; }
        public string Sro { get; set; }
        public string StatusOcorrencia { get; set; }
        public DateTime? DataOcorrencia { get; set; }

    }
}
