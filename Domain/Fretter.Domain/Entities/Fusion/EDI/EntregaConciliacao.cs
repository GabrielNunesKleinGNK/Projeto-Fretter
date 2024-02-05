using System;

namespace Fretter.Domain.Entities
{
    public class EntregaConciliacao : EntityBase
    {
        #region "Construtores"
        public EntregaConciliacao(long id, long entregaId)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaId = entregaId;           
        }
        #endregion

        #region "Propriedades"
        public long EntregaId { get; set; }
        public DateTime? DataProcessamento { get; protected set; }
        public decimal? ValorCobrado { get; protected set; }
        public decimal? Altura { get; protected set; }
        public decimal? Largura { get; protected set; }
        public decimal? Comprimento { get; protected set; }
        public decimal? Diametro { get; protected set; }
        public decimal? Peso { get; protected set; }
        public decimal? Cubagem { get; protected set; }
        public string Json { get; set; }
        public string RetornoProcessamento { get; set; }
        public bool Processado { get; set; }
        public bool Sucesso { get; set; }

        #endregion

        #region "Referencias"		
        //public Entrega Entrega { get; set; }
        #endregion

        #region "Métodos"  
        public void AtualizarEntrega(long entregaId) => this.EntregaId = entregaId;
        public void AtualizarDataProcessamento(DateTime? dataProcessamento) => this.DataProcessamento = dataProcessamento;
        public void AtualizarValorCobrado(decimal? valorCobrado) => this.ValorCobrado = valorCobrado;
        public void AtualizarAltura(decimal? valorCobrado) => this.ValorCobrado = valorCobrado;
        public void AtualizarLargura(decimal? Largura) => this.Largura = Largura;
        public void AtualizarComprimento(decimal? Comprimento) => this.Comprimento = Comprimento;
        public void AtualizarDiametro(decimal? Diametro) => this.Diametro = Diametro;
        public void AtualizarPeso(decimal? Peso) => this.Peso = Peso;
        public void AtualizarCubagem(decimal? Cubagem) => this.Cubagem = Cubagem;
        public void AtualizarJson(string Json) => this.Json = Json;        
        public void AtualizarProcessado(bool processado) => this.Processado = processado;
        public void AtualizarSucesso(bool sucesso) => this.Sucesso = sucesso;
        #endregion
    }
}
