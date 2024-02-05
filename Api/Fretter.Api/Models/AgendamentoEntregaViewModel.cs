using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fretter.Api.Models
{
    public class AgendamentoEntregaViewModel : IViewModel<AgendamentoEntrega>
    {
        public int Id{ get; set; }
        public int IdCanal { get; set; }
        public int? IdEmpresa { get; set; } = 0;
        public int IdRegiaoCEPCapacidade { get; set; }
        public int? IdTransportador { get; set; } = 0;
        public int? IdTransportadorCnpj { get; set; } = 0;
        public string CdNotaFiscal { get; set; }
        public string CdSerie { get; set; }
        public string CdSro { get; set; }
        public string CdEntrega { get; set; }
        public string CdPedido { get; set; }
        public string CdCepOrigem { get; set; }
        public string CdCepDestino { get; set; }
        public string CdProtocolo { get; set; }
        public int? NrPrazoTransportador { get; set; } = 0;
        public int VlQuantidade { get; set; }
        public DateTime DtAgendamento { get; set; } = DateTime.Now;
        public string DsObservacao { get; set; }

        public AgendamentoEntregaDestinatarioViewModel Destinatario { get; set; }
        public IList<AgendamentoEntregaDestinatarioViewModel> Destinatarios { get; set; }
        public IList<AgendamentoEntregaProdutoViewModel> Produtos { get; set; }
        public Fusion.CanalViewModel Canal { get; set; }
        public MenuFreteRegiaoCepCapacidadeViewModel MenuFreteRegiaoCepCapacidade { get; set; }

        public AgendamentoEntrega Model()
        {
            var produtos = new List<AgendamentoEntregaProduto>();
            foreach(var prod in Produtos)
            {
                produtos.Add(prod.Model());
            }

            return new AgendamentoEntrega(Id, IdCanal, (int)IdEmpresa, IdRegiaoCEPCapacidade, IdTransportador, IdTransportadorCnpj, CdNotaFiscal,
                                          CdSerie, CdSro, CdEntrega, CdPedido, CdCepOrigem, CdCepDestino, CdProtocolo, (int)NrPrazoTransportador,
                                          VlQuantidade, DtAgendamento, DsObservacao, Destinatario.Model(), produtos);
        }
    }
}
