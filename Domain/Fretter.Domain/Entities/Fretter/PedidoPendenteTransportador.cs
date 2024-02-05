using System;

namespace Fretter.Domain.Entities.Fretter
{
    public class PedidoPendenteTransportador : EntityBase
    {
        public PedidoPendenteTransportador() { }
        public PedidoPendenteTransportador(int? empresaId, int? entregaId, int? transportadorId)
        {
            EmpresaId = empresaId ?? 0;
            EntregaId = entregaId ?? 0;
            TransportadorId = transportadorId ?? 0;
            DataProcessamento = DateTime.Now;
        }

        public int EmpresaId { get; protected set; }
        public int EntregaId { get; protected set; }
        public int TransportadorId { get; protected set; }
        public string StatusTransportadora { get; protected set; }
        public string NomeTransportadora { get; protected set; }
        public DateTime? DataStatusTransportadora { get; protected set; }
        public DateTime? DataAtualizacaoTransportadora { get; protected set; }
        public bool Sucesso { get; protected set; }
        public string ListaIntegracaoEnviada { get; protected set; }
        public DateTime DataProcessamento { get; protected set; }

        public void AtualizarStatusTransportadora(string statusTransportadora) => this.StatusTransportadora = statusTransportadora;
        public void AtualizarNomeTransportadora(string nomeTransportadora) => this.NomeTransportadora = nomeTransportadora;
        public void AtualizarIntegracaoEnviada(string listaIntegracaoEnviada) => this.ListaIntegracaoEnviada = listaIntegracaoEnviada;
        public void AtualizarDataStatusTransportadora(DateTime dataStatusTransportadora) => this.DataStatusTransportadora = dataStatusTransportadora;
        public void AtualizarDataAtualizacaoTransportadora(DateTime dataAtualizacaoTransportadora) => this.DataAtualizacaoTransportadora = dataAtualizacaoTransportadora;
        public void ProcessadoComSucesso() => this.Sucesso = true;
    }
}
