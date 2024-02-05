using Fretter.Domain.Dto.Carrefour.Mirakl;
using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageItem : EntityBase
    {
        #region "Construtores"
        public EntregaStageItem (int Id,DateTime Inclusao,int EntregaStage,string EntregaEntrada,string EntregaSaida,string Sro,string SroReversa,DateTime? Postagem,DateTime? PostagemReversa,DateTime? PrevistaEntrega,DateTime? PrevistaEntregaReversa,decimal? Item,decimal? ItemTotal,decimal? Frete,decimal? FreteCobrado,bool? PostagemVerificada,bool? Rastreada,string Informacao,DateTime? SoliticacaoReversa,DateTime? ValidadeReversa,bool? PendenteReversa)
		{
			this.Ativar();
			this.Id = Id;
			this.Inclusao = Inclusao;
			this.EntregaStage = EntregaStage;
			this.EntregaEntrada = EntregaEntrada;
			this.EntregaSaida = EntregaSaida;
			this.Sro = Sro;
			this.SroReversa = SroReversa;
			this.Postagem = Postagem;
			this.PostagemReversa = PostagemReversa;
			this.PrevistaEntrega = PrevistaEntrega;
			this.PrevistaEntregaReversa = PrevistaEntregaReversa;
			this.Item = Item;
			this.ItemTotal = ItemTotal;
			this.Frete = Frete;
			this.FreteCobrado = FreteCobrado;
			this.PostagemVerificada = PostagemVerificada;
			this.Rastreada = Rastreada;
			this.Informacao = Informacao;
			this.SoliticacaoReversa = SoliticacaoReversa;
			this.ValidadeReversa = ValidadeReversa;
			this.PendenteReversa = PendenteReversa;
			this.Ativo = Ativo;
		}

        public EntregaStageItem(OrderLineDTO item, OrderDTO order)
        {

        }
        #endregion

        #region "Propriedades"
        public DateTime Inclusao { get; protected set; }
        public int EntregaStage { get; protected set; }
        public string EntregaEntrada { get; protected set; }
        public string EntregaSaida { get; protected set; }
        public string Sro { get; protected set; }
        public string SroReversa { get; protected set; }
        public DateTime? Postagem { get; protected set; }
        public DateTime? PostagemReversa { get; protected set; }
        public DateTime? PrevistaEntrega { get; protected set; }
        public DateTime? PrevistaEntregaReversa { get; protected set; }
        public decimal? Item { get; protected set; }
        public decimal? ItemTotal { get; protected set; }
        public decimal? Frete { get; protected set; }
        public decimal? FreteCobrado { get; protected set; }
        public bool? PostagemVerificada { get; protected set; }
        public bool? Rastreada { get; protected set; }
        public string Informacao { get; protected set; }
        public DateTime? SoliticacaoReversa { get; protected set; }
        public DateTime? ValidadeReversa { get; protected set; }
        public bool? PendenteReversa { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
		public void AtualizarEntregaStage(int EntregaStage) => this.EntregaStage = EntregaStage;
		public void AtualizarEntregaEntrada(string EntregaEntrada) => this.EntregaEntrada = EntregaEntrada;
		public void AtualizarEntregaSaida(string EntregaSaida) => this.EntregaSaida = EntregaSaida;
		public void AtualizarSro(string Sro) => this.Sro = Sro;
		public void AtualizarSroReversa(string SroReversa) => this.SroReversa = SroReversa;
		public void AtualizarPostagem(DateTime? Postagem) => this.Postagem = Postagem;
		public void AtualizarPostagemReversa(DateTime? PostagemReversa) => this.PostagemReversa = PostagemReversa;
		public void AtualizarPrevistaEntrega(DateTime? PrevistaEntrega) => this.PrevistaEntrega = PrevistaEntrega;
		public void AtualizarPrevistaEntregaReversa(DateTime? PrevistaEntregaReversa) => this.PrevistaEntregaReversa = PrevistaEntregaReversa;
		public void AtualizarItem(decimal? Item) => this.Item = Item;
		public void AtualizarItemTotal(decimal? ItemTotal) => this.ItemTotal = ItemTotal;
		public void AtualizarFrete(decimal? Frete) => this.Frete = Frete;
		public void AtualizarFreteCobrado(decimal? FreteCobrado) => this.FreteCobrado = FreteCobrado;
		public void AtualizarPostagemVerificada(bool? PostagemVerificada) => this.PostagemVerificada = PostagemVerificada;
		public void AtualizarRastreada(bool? Rastreada) => this.Rastreada = Rastreada;
		public void AtualizarInformacao(string Informacao) => this.Informacao = Informacao;
		public void AtualizarSoliticacaoReversa(DateTime? SoliticacaoReversa) => this.SoliticacaoReversa = SoliticacaoReversa;
		public void AtualizarValidadeReversa(DateTime? ValidadeReversa) => this.ValidadeReversa = ValidadeReversa;
		public void AtualizarPendenteReversa(bool? PendenteReversa) => this.PendenteReversa = PendenteReversa;
		#endregion
    }
}      
