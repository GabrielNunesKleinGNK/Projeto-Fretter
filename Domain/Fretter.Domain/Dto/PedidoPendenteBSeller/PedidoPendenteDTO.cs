using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Dto.PedidoPendenteBSeller
{
    public class PedidoPendenteDTO
    {
		public string ContratoExternoBSeller { get;  set; }
		public string DtEmissaoBSeller { get;  set; }
		public string TranspNomeBSeller { get;  set; }
		public string NotaBSeller { get;  set; }
		public string SerieBSeller { get;  set; }
		public string IdTransportadoraBSeller { get;  set; }
		public string UltPontoBSeller { get;  set; }
		public string FilialBSeller { get;  set; }
		public string DataPrometidaBSeller { get;  set; }
		public string DataETRBSeller { get;  set; }
		public string StatusBSeller { get;  set; }
		public string NomePontoBSeller { get;  set; }
		public string EntregaBSeller { get;  set; }
		public int IdContratoBSeller { get;  set; }
		public string DataAjustadaBSeller { get;  set; }
		public string DtUltPontoBSeller { get;  set; }


		public int? EntregaId { get;  set; }
		public int? TransportadorId { get;  set; }
		public int EmpresaId { get;  set; }
		public string StatusFusion { get;  set; }
		public string NotaFiscal { get; set; }
		public string NotaFiscalDRS { get; set; }
		public string CnpjCanal { get; set; }
		public string CnpjCanalDRS { get; set; }
		public string Danfe { get; set; }
		public string DanfeDRS { get; set; }
		public string Sro { get; set; }
		public DateTime DataProcessamento { get;  set; }

		public int PedidoPendenteIntegracaoId { get; set; }

		public IList<EnumPedidoIntegracao> ListaIntegracaoEnviada { get; set; }
	}
}
