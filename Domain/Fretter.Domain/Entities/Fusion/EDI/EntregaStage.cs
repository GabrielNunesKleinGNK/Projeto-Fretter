using Fretter.Domain.Dto.Carrefour.Mirakl;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EntregaStage : EntityBase
    {
		#region "Construtores"


        public EntregaStage (int Id, int? CanalVenda, int? Empresa, int? Canal, int? EmpresaMarketplace,
							 int? Transportador, int? TransportadorCnpj, int? MicroServico, byte? TipoServico,
							 int? Lojista, int? PLP, string Danfe, string CodigoIntegracao, string EntregaEntrada, string EntregaSaida, 
							 string Sro, DateTime? PedidoCriacao,DateTime? Postagem,DateTime? PrevistaEntrega,int? PrazoTransportadorEstatico,
							 int? PrazoTransportadorDinamico,int? PrazoCliente,decimal? Custo,decimal? Cobrado,decimal? Global,
							 bool? ServicoDisponivel,string ServicoDisponivelDescricao,bool? PostagemVerificada,bool? EntregaImportada,
							 int? Tabela,string Tomador,decimal? Altura,decimal? Comprimento,decimal? Largura,decimal? Peso,decimal? Cubagem,
							 decimal? Diametro,decimal? Declarado,decimal? Receita,decimal? Total,bool? EtiquetaGerada,
							 DateTime? DataEtiquetaGerada, DateTime? ValidadeInicioEtiqueta,DateTime? ValidadeFimEtiqueta,
							 bool? Rastreada,int? Volume,string LinkEtiquetaPDF,string LinkEtiquetaPNG,string LinkEtiquetaZPL,
							 DateTime Inclusao,DateTime? UltimaAlteracao)
		{
			this.Ativar();
			this.Id = Id;
			this.CanalVenda = CanalVenda;
			this.Empresa = Empresa;
			this.Canal = Canal;
			this.EmpresaMarketplace = EmpresaMarketplace;
			this.Transportador = Transportador;
			this.TransportadorCnpj = TransportadorCnpj;
			this.MicroServico = MicroServico;
			this.TipoServico = TipoServico;
			this.Lojista = Lojista;
			this.PLP = PLP;
			this.Danfe = Danfe;
			this.CodigoIntegracao = CodigoIntegracao;
			this.EntregaEntrada = EntregaEntrada;
			this.EntregaSaida = EntregaSaida;
			this.Sro = Sro;
			this.PedidoCriacao = PedidoCriacao;
			this.Postagem = Postagem;
			this.PrevistaEntrega = PrevistaEntrega;
			this.PrazoTransportadorEstatico = PrazoTransportadorEstatico;
			this.PrazoTransportadorDinamico = PrazoTransportadorDinamico;
			this.PrazoCliente = PrazoCliente;
			this.Custo = Custo;
			this.Cobrado = Cobrado;
			this.Global = Global;
			this.ServicoDisponivel = ServicoDisponivel;
			this.ServicoDisponivelDescricao = ServicoDisponivelDescricao;
			this.PostagemVerificada = PostagemVerificada;
			this.EntregaImportada = EntregaImportada;
			this.Tabela = Tabela;
			this.Tomador = Tomador;
			this.Altura = Altura;
			this.Comprimento = Comprimento;
			this.Largura = Largura;
			this.Peso = Peso;
			this.Cubagem = Cubagem;
			this.Diametro = Diametro;
			this.Declarado = Declarado;
			this.Receita = Receita;
			this.Total = Total;
			this.EtiquetaGerada = EtiquetaGerada;
			this.DataEtiquetaGerada = DataEtiquetaGerada;
			this.ValidadeInicioEtiqueta = ValidadeInicioEtiqueta;
			this.ValidadeFimEtiqueta = ValidadeFimEtiqueta;
			this.Rastreada = Rastreada;
			this.Volume = Volume;
			this.LinkEtiquetaPDF = LinkEtiquetaPDF;
			this.LinkEtiquetaPNG = LinkEtiquetaPNG;
			this.LinkEtiquetaZPL = LinkEtiquetaZPL;
			this.Inclusao = Inclusao;
			this.UltimaAlteracao = UltimaAlteracao;
			this.Ativo = Ativo;
		}

        public EntregaStage(OrderDTO order)
        {
			//this.AtualizarId(0);
			this.AtualizarCodigoIntegracao(order.order_id);
			this.AtualizarEntregaEntrada(order.order_id);
			this.AtualizarEntregaSaida(order.order_id);
			this.AtualizarCusto(order.total_price);
			this.AtualizarPostagem(order.created_date);
			this.AtualizarPrevistaEntrega(order.delivery_date.latest);
			this.AtualizarDataCriacao();

            foreach (OrderLineDTO orderLine in order.order_lines)
            {
				EntregaStageItem item = new EntregaStageItem(orderLine, order);
				this.AdicionarEntregaStageItem(item);

				EntregaStageSku sku = new EntregaStageSku(orderLine, order);
				this.AdicionarEntregaStageSku(sku);
			}

			EntregaStageDestinatario destinatario = new EntregaStageDestinatario(order.customer);
			this.AdicionarDestinatario(destinatario);
		}

        #endregion

        #region "Propriedades"
        public int? CanalVenda { get; protected set; }
        public int? Empresa { get; protected set; }
        public int? Canal { get; protected set; }
        public int? EmpresaMarketplace { get; protected set; }
        public int? Transportador { get; protected set; }
        public int? TransportadorCnpj { get; protected set; }
        public int? MicroServico { get; protected set; }
        public byte? TipoServico { get; protected set; }
        public int? Lojista { get; protected set; }
        public int? PLP { get; protected set; }
        public string Danfe { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public string EntregaEntrada { get; protected set; }
        public string EntregaSaida { get; protected set; }
        public string Sro { get; protected set; }
        public DateTime? PedidoCriacao { get; protected set; }
        public DateTime? Postagem { get; protected set; }
        public DateTime? PrevistaEntrega { get; protected set; }
        public int? PrazoTransportadorEstatico { get; protected set; }
        public int? PrazoTransportadorDinamico { get; protected set; }
        public int? PrazoCliente { get; protected set; }
        public decimal? Custo { get; protected set; }
        public decimal? Cobrado { get; protected set; }
        public decimal? Global { get; protected set; }
        public bool? ServicoDisponivel { get; protected set; }
        public string ServicoDisponivelDescricao { get; protected set; }
        public bool? PostagemVerificada { get; protected set; }
        public bool? EntregaImportada { get; protected set; }
        public int? Tabela { get; protected set; }
        public string Tomador { get; protected set; }
        public decimal? Altura { get; protected set; }
        public decimal? Comprimento { get; protected set; }
        public decimal? Largura { get; protected set; }
        public decimal? Peso { get; protected set; }
        public decimal? Cubagem { get; protected set; }
        public decimal? Diametro { get; protected set; }
        public decimal? Declarado { get; protected set; }
        public decimal? Receita { get; protected set; }
        public decimal? Total { get; protected set; }
        public bool? EtiquetaGerada { get; protected set; }
        public DateTime? DataEtiquetaGerada { get; protected set; }
        public DateTime? ValidadeInicioEtiqueta { get; protected set; }
        public DateTime? ValidadeFimEtiqueta { get; protected set; }
        public bool? Rastreada { get; protected set; }
        public int? Volume { get; protected set; }
        public string LinkEtiquetaPDF { get; protected set; }
        public string LinkEtiquetaPNG { get; protected set; }
        public string LinkEtiquetaZPL { get; protected set; }
        public DateTime Inclusao { get; protected set; }
        public DateTime? UltimaAlteracao { get; protected set; }
		#endregion

		#region "Referencias"
		public virtual List<EntregaStageItem> Itens { get; set; }
		public virtual List<EntregaStageSku> Skus { get; set; }
		public virtual EntregaStageDestinatario Destinatario { get; set; }
		#endregion

		#region "Métodos"
		public void AtualizarCanalVenda(int? CanalVenda) => this.CanalVenda = CanalVenda;
		public void AtualizarEmpresa(int? Empresa) => this.Empresa = Empresa;
		public void AtualizarCanal(int? Canal) => this.Canal = Canal;
		public void AtualizarEmpresaMarketplace(int? EmpresaMarketplace) => this.EmpresaMarketplace = EmpresaMarketplace;
		public void AtualizarTransportador(int? Transportador) => this.Transportador = Transportador;
		public void AtualizarTransportadorCnpj(int? TransportadorCnpj) => this.TransportadorCnpj = TransportadorCnpj;
		public void AtualizarMicroServico(int? MicroServico) => this.MicroServico = MicroServico;
		public void AtualizarTipoServico(byte? TipoServico) => this.TipoServico = TipoServico;
		public void AtualizarLojista(int? Lojista) => this.Lojista = Lojista;
		public void AtualizarPLP(int? PLP) => this.PLP = PLP;
		public void AtualizarDanfe(string Danfe) => this.Danfe = Danfe;
		public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
		public void AtualizarEntregaEntrada(string EntregaEntrada) => this.EntregaEntrada = EntregaEntrada;
		public void AtualizarEntregaSaida(string EntregaSaida) => this.EntregaSaida = EntregaSaida;
		public void AtualizarSro(string Sro) => this.Sro = Sro;
		public void AtualizarPedidoCriacao(DateTime? PedidoCriacao) => this.PedidoCriacao = PedidoCriacao;
		public void AtualizarPostagem(DateTime? Postagem) => this.Postagem = Postagem;
		public void AtualizarPrevistaEntrega(DateTime? PrevistaEntrega) => this.PrevistaEntrega = PrevistaEntrega;
		public void AtualizarPrazoTransportadorEstatico(int? PrazoTransportadorEstatico) => this.PrazoTransportadorEstatico = PrazoTransportadorEstatico;
		public void AtualizarPrazoTransportadorDinamico(int? PrazoTransportadorDinamico) => this.PrazoTransportadorDinamico = PrazoTransportadorDinamico;
		public void AtualizarPrazoCliente(int? PrazoCliente) => this.PrazoCliente = PrazoCliente;
		public void AtualizarCusto(decimal? Custo) => this.Custo = Custo;
		public void AtualizarCobrado(decimal? Cobrado) => this.Cobrado = Cobrado;
		public void AtualizarGlobal(decimal? Global) => this.Global = Global;
		public void AtualizarServicoDisponivel(bool ServicoDisponivel) => this.ServicoDisponivel = ServicoDisponivel;
		public void AtualizarServicoDisponivel(string ServicoDisponivel) => this.ServicoDisponivelDescricao = ServicoDisponivel;
		public void AtualizarPostagemVerificada(bool PostagemVerificada) => this.PostagemVerificada = PostagemVerificada;
		public void AtualizarEntregaImportada(bool EntregaImportada) => this.EntregaImportada = EntregaImportada;
		public void AtualizarTabela(int? Tabela) => this.Tabela = Tabela;
		public void AtualizarTomador(string Tomador) => this.Tomador = Tomador;
		public void AtualizarAltura(decimal? Altura) => this.Altura = Altura;
		public void AtualizarComprimento(decimal? Comprimento) => this.Comprimento = Comprimento;
		public void AtualizarLargura(decimal? Largura) => this.Largura = Largura;
		public void AtualizarPeso(decimal? Peso) => this.Peso = Peso;
		public void AtualizarCubagem(decimal? Cubagem) => this.Cubagem = Cubagem;
		public void AtualizarDiametro(decimal? Diametro) => this.Diametro = Diametro;
		public void AtualizarDeclarado(decimal? Declarado) => this.Declarado = Declarado;
		public void AtualizarReceita(decimal? Receita) => this.Receita = Receita;
		public void AtualizarTotal(decimal? Total) => this.Total = Total;
		public void AtualizarEtiquetaGerada(bool EtiquetaGerada) => this.EtiquetaGerada = EtiquetaGerada;
		public void AtualizarDataEtiquetaGerada(DateTime? data) => this.DataEtiquetaGerada = data;
		public void AtualizarValidadeInicioEtiqueta(DateTime? ValidadeInicioEtiqueta) => this.ValidadeInicioEtiqueta = ValidadeInicioEtiqueta;
		public void AtualizarValidadeFimEtiqueta(DateTime? ValidadeFimEtiqueta) => this.ValidadeFimEtiqueta = ValidadeFimEtiqueta;
		public void AtualizarRastreada(bool Rastreada) => this.Rastreada = Rastreada;
		public void AtualizarVolume(int? Volume) => this.Volume = Volume;
		public void AtualizarLinkEtiquetaPDF(string LinkEtiquetaPDF) => this.LinkEtiquetaPDF = LinkEtiquetaPDF;
		public void AtualizarLinkEtiquetaPNG(string LinkEtiquetaPNG) => this.LinkEtiquetaPNG = LinkEtiquetaPNG;
		public void AtualizarLinkEtiquetaZPL(string LinkEtiquetaZPL) => this.LinkEtiquetaZPL = LinkEtiquetaZPL;
		public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
		public void AtualizarUltimaAlteracao(DateTime? UltimaAlteracao) => this.UltimaAlteracao = UltimaAlteracao;

		private void AdicionarEntregaStageItem(EntregaStageItem item)
		{
			if (this.Itens == null)
				this.Itens = new List<EntregaStageItem>();
			this.Itens.Add(item);
		}
		private void AdicionarEntregaStageSku(EntregaStageSku sku)
		{
			if (this.Skus == null)
				this.Skus = new List<EntregaStageSku>();
			this.Skus.Add(sku);
		}
		private void AdicionarDestinatario(EntregaStageDestinatario destinatario)
		{
			this.Destinatario = destinatario;
		}
		#endregion
	}
}      
