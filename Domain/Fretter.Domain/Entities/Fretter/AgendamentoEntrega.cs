using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class AgendamentoEntrega : EntityBase
    {
        #region Construtores

        public  AgendamentoEntrega()
        {}

        public AgendamentoEntrega(int id, int idCanal, int? idEmpresa, int idRegiaoCEPCapacidade, int? idTransportador, int? idTransportadorCnpj, string cdNotaFiscal,
            string cdSerie, string cdSro, string cdEntrega, string cdPedido, string cdCepOrigem, string cdCepDestino, string cdProtocolo, int? nrPrazoTransportador,
            int vlQuantidade, DateTime dtAgendamento, string dsObservacao, AgendamentoEntregaDestinatario agendamentoEntregaDestinatario, IList<AgendamentoEntregaProduto> produtos)
        {
            this.Id = id;
            this.IdCanal = idCanal;
            this.IdEmpresa = idEmpresa;
            this.IdRegiaoCEPCapacidade = idRegiaoCEPCapacidade;
            this.IdTransportador = idTransportador;
            this.IdTransportadorCnpj = idTransportadorCnpj;
            this.CdNotaFiscal = cdNotaFiscal;
            this.CdSerie = cdSerie;
            this.CdSro = cdSro;
            this.CdEntrega = cdEntrega;
            this.CdPedido = cdPedido;
            this.CdCepOrigem = cdCepOrigem;
            this.CdCepDestino = cdCepDestino;
            this.CdProtocolo = cdProtocolo;
            this.NrPrazoTransportador = nrPrazoTransportador;
            this.VlQuantidade = vlQuantidade;
            this.DtAgendamento = dtAgendamento;
            this.DsObservacao = dsObservacao;
            this.Destinatarios = new List<AgendamentoEntregaDestinatario>();
            this.Destinatarios.Add(agendamentoEntregaDestinatario);
            this.Produtos= produtos;

           
        }
        #endregion

        #region "Propriedades"
        public int IdCanal { get; protected set; }
        public int? IdEmpresa { get; protected set; }
        public int? IdRegiaoCEPCapacidade { get; protected set; }
        public int? IdTransportador { get; protected set; }
        public int? IdTransportadorCnpj { get; protected set; }
        public string CdNotaFiscal { get; protected set; }
        public string CdSerie { get; protected set; }
        public string CdSro { get; protected set; }
        public string CdEntrega { get; protected set; }
        public string CdPedido { get; protected set; }
        public string CdCepOrigem { get; protected set; }
        public string CdCepDestino { get; protected set; }
        public string CdProtocolo { get; protected set; }
        public int? NrPrazoTransportador { get; protected set; }
        public int? VlQuantidade { get; protected set; }
        public DateTime? DtAgendamento { get; protected set; }
        public string DsObservacao { get; protected set; }
        #endregion

        #region Referencias
        public virtual IList<AgendamentoEntregaDestinatario> Destinatarios { get; set; }
        public virtual IList<AgendamentoEntregaProduto> Produtos { get; set; }
        public virtual Canal Canal { get; set; }
        public virtual MenuFreteRegiaoCepCapacidade MenuFreteRegiaoCepCapacidade { get; set; }
        #endregion

        public void AtualizarEmpresaId(int id) => this.IdEmpresa = id;
    }
}
