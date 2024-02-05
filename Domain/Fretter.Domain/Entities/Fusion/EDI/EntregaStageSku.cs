using Fretter.Domain.Dto.Carrefour.Mirakl;
using System;

namespace Fretter.Domain.Entities
{
    public class EntregaStageSku : EntityBase
    {

        #region "Construtores"
        public EntregaStageSku (int Id,int EntregaStageItem,string DescricaoItem, string Sku,string CodigoIntegracao,decimal? ValorProduto, decimal? ValorProdutoUnitario, decimal? Altura,decimal? Comprimento,decimal? Largura,decimal? Peso,decimal? Cubagem,decimal? Diametro,int? Quantidade,DateTime? DataAlteracao)
		{
			this.Ativar();
			this.Id = Id;
			this.EntregaStageItem = EntregaStageItem;
			this.DescricaoItem = DescricaoItem;
			this.Sku = Sku;
			this.CodigoIntegracao = CodigoIntegracao;
			this.ValorProduto = ValorProduto;
			this.ValorProdutoUnitario = ValorProdutoUnitario;
			this.Altura = Altura;
			this.Comprimento = Comprimento;
			this.Largura = Largura;
			this.Peso = Peso;
			this.Cubagem = Cubagem;
			this.Diametro = Diametro;
			this.Quantidade = Quantidade;
			this.DataAlteracao = DataAlteracao;
			this.Ativo = Ativo;
		}

        public EntregaStageSku(OrderLineDTO orderLine, OrderDTO order)
        {
			this.AtualizarSku(orderLine.product_sku);
			this.AtualizarCodigoIntegracao(orderLine.order_line_id);
			this.AtualizarQuantidade(orderLine.quantity);
			this.AtualizarValorProduto(orderLine.total_price);
			this.AtualizarValorProdutoUnitario(orderLine.price_unit);
			this.AtualizarDescricaoItem(orderLine.product_title);
			this.Ativar();
        }
        #endregion

        #region "Propriedades"
        public int EntregaStageItem { get; protected set; }
        public string DescricaoItem { get; protected set; }
        public string Sku { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public decimal? ValorProduto { get; protected set; }
        public decimal? ValorProdutoUnitario { get; protected set; }
        public decimal? Altura { get; protected set; }
        public decimal? Comprimento { get; protected set; }
        public decimal? Largura { get; protected set; }
        public decimal? Peso { get; protected set; }
        public decimal? Cubagem { get; protected set; }
        public decimal? Diametro { get; protected set; }
        public int? Quantidade { get; protected set; }
        public DateTime? DataAlteracao { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarEntregaStageItem(int EntregaStageItem) => this.EntregaStageItem = EntregaStageItem;
		public void AtualizarDescricaoItem(string Item) => this.DescricaoItem = Item;
		public void AtualizarSku(string Sku) => this.Sku = Sku;
		public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
		public void AtualizarValorProduto(decimal? Produto) => this.ValorProduto = Produto;
		public void AtualizarValorProdutoUnitario(decimal? ProdutoUnitario) => this.ValorProdutoUnitario = ProdutoUnitario;
		public void AtualizarAltura(decimal? Altura) => this.Altura = Altura;
		public void AtualizarComprimento(decimal? Comprimento) => this.Comprimento = Comprimento;
		public void AtualizarLargura(decimal? Largura) => this.Largura = Largura;
		public void AtualizarPeso(decimal? Peso) => this.Peso = Peso;
		public void AtualizarCubagem(decimal? Cubagem) => this.Cubagem = Cubagem;
		public void AtualizarDiametro(decimal? Diametro) => this.Diametro = Diametro;
		public void AtualizarQuantidade(int? Quantidade) => this.Quantidade = Quantidade;
		public void AtualizarAlteracao(DateTime? Alteracao) => this.DataAlteracao = Alteracao;
		#endregion
    }
}      
