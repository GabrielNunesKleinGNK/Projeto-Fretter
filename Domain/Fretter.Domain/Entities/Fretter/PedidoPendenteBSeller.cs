using Fretter.Domain.Dto.PedidoPendenteBSeller;
using Fretter.Domain.Dto.PedidoPendenteBSeller.BSeller;
using Fretter.Domain.Helpers.Attributes;
using System;

namespace Fretter.Domain.Entities.Fretter
{
    public class PedidoPendenteBSeller : EntityBase
    {

		#region "Construtores"

		public PedidoPendenteBSeller() { }
		public PedidoPendenteBSeller(ResponseTrackingBSeller responseTrackingBSeller, int empresaId, EntregaPedido entregaPedido)
		{
			this.ContratoExternoBSeller = responseTrackingBSeller.Contrato_Externo;
			this.DtEmissaoBSeller = responseTrackingBSeller.Dt_Emissao;
			this.TranspNomeBSeller = responseTrackingBSeller.Transp_Nome;
			this.NotaBSeller = Convert.ToInt32(responseTrackingBSeller.Nota).ToString();
			this.IdTransportadoraBSeller = responseTrackingBSeller.Id_Transportadora;
			this.SerieBSeller = responseTrackingBSeller.Serie;
			this.UltPontoBSeller = responseTrackingBSeller.Ult_Ponto;
			this.FilialBSeller = Convert.ToInt32(responseTrackingBSeller.Filial).ToString();
			this.DataPrometidaBSeller = responseTrackingBSeller.Data_Prometida;
			this.DataETRBSeller = responseTrackingBSeller.Data_Etr;
			this.StatusBSeller = responseTrackingBSeller.Status;
			this.NomePontoBSeller = responseTrackingBSeller.Nome_Ponto;
			this.EntregaBSeller = responseTrackingBSeller.Entrega;
			this.IdContratoBSeller = Convert.ToInt32(responseTrackingBSeller.Id_Contrato);
			this.DataAjustadaBSeller = responseTrackingBSeller.Data_Ajustada;
			this.DtUltPontoBSeller = responseTrackingBSeller.Dt_Ult_Ponto;
			this.EntregaId = entregaPedido?.IdEntrega;
			this.EmpresaId = empresaId;
			this.TransportadorId = entregaPedido?.IdTransportador;
			this.Danfe = entregaPedido?.Danfe;
			this.DanfeDRS = entregaPedido?.DanfeDRS;
			this.NotaFiscal = entregaPedido?.NotaFiscal;
			this.NotaFiscalDRS = entregaPedido?.NotaFiscalDRS;
			this.CnpjCanal = entregaPedido?.CnpjCanal;
			this.CnpjCanalDRS = entregaPedido?.CnpjCanalDRS;
			this.DescricaoMicroServico = entregaPedido?.DescricaoMicroServico;
			this.DataProcessamento = DateTime.Now;
			this.PedidoPendenteIntegracaoId = entregaPedido?.PedidoPendenteIntegracaoId ?? 0;
			this.Sro = entregaPedido?.Sro;
			this.StatusOcorrenciaFusion = entregaPedido?.StatusOcorrencia;
			this.DataOcorrenciaFusion = entregaPedido?.DataOcorrencia;
		}

		#endregion

		#region "Propriedades"

		public string ContratoExternoBSeller { get; protected set; }
		public string DtEmissaoBSeller { get; protected set; }
		public string TranspNomeBSeller { get; protected set; }
		public string NotaBSeller { get; protected set; }
		public string SerieBSeller { get; protected set; }
		public string IdTransportadoraBSeller { get; protected set; }
		public string UltPontoBSeller { get; protected set; }
		public string FilialBSeller { get; protected set; }
		public string DataPrometidaBSeller { get; protected set; }
		public string DataETRBSeller { get; protected set; }
		public string StatusBSeller { get; protected set; }
		public string NomePontoBSeller { get; protected set; }
		public string EntregaBSeller { get; protected set; }
		public int IdContratoBSeller { get; protected set; }
		public string DataAjustadaBSeller { get; protected set; }
		public string DtUltPontoBSeller { get; protected set; }
		

		public int? EntregaId { get; protected set; }
		public int? TransportadorId { get; protected set; }
		public int EmpresaId { get; protected set; }
		public string StatusTratado { get; protected set; }
		
		public string NotaFiscal { get; protected set; }
		public string NotaFiscalDRS { get; protected set; }
		public string CnpjCanal { get; protected set; }
		public string CnpjCanalDRS { get; protected set; }
		public string Danfe { get; protected set; }
		public string DanfeDRS { get; protected set; }
		public string DescricaoMicroServico { get; protected set; }
		public DateTime DataProcessamento { get; protected set; }
		public string Sro { get; protected set; }
		public string StatusOcorrenciaFusion { get; protected set; }
		public DateTime? DataOcorrenciaFusion { get; protected set; }

		[DataTableColumnIgnoreAttribute]
		public int PedidoPendenteIntegracaoId { get; set; }

		#endregion

		#region Metodos
		public void AtualizarStatusTratado(string statusFusion) => this.StatusTratado = statusFusion;

        #endregion

    }
}
