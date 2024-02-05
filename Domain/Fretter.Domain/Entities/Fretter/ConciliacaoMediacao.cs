using System;

namespace Fretter.Domain.Entities
{
    public class ConciliacaoMediacao : EntityBase
    {
		#region "Construtores"
		public ConciliacaoMediacao (int Id,int ImportacaoArquivoId,int ImportacaoArquivoCategoriaId,int? TipoServico,string Chave,string Codigo,string Numero,int? DigitoVerificador,string Serie,DateTime? DataEmissao,decimal? ValorPrestacaoServico)
		{
			this.Ativar();
			this.Id = Id;
			this.ImportacaoArquivoId = ImportacaoArquivoId;
			this.ImportacaoArquivoCategoriaId = ImportacaoArquivoCategoriaId;
			this.TipoServico = TipoServico;
			this.Chave = Chave;
			this.Codigo = Codigo;
			this.Numero = Numero;
			this.DigitoVerificador = DigitoVerificador;
			this.Serie = Serie;
			this.DataEmissao = DataEmissao;
			this.ValorPrestacaoServico = ValorPrestacaoServico;
		}
		#endregion

		#region "Propriedades"
        public int ImportacaoArquivoId { get; protected set; }
        public int ImportacaoArquivoCategoriaId { get; protected set; }
        public int? TipoServico { get; protected set; }
        public string Chave { get; protected set; }
        public string Codigo { get; protected set; }
        public string Numero { get; protected set; }
        public int? DigitoVerificador { get; protected set; }
        public string Serie { get; protected set; }
        public DateTime? DataEmissao { get; protected set; }
        public decimal? ValorPrestacaoServico { get; protected set; }
		#endregion

		#region "Referencias"
		public ImportacaoArquivo ImportacaoArquivo{get; protected set;}
		public ImportacaoArquivoCategoria ImportacaoArquivoCategoria{get; protected set;}
		#endregion

		#region "Métodos"
		public void AtualizarImportacaoArquivoId(int ImportacaoArquivoId) => this.ImportacaoArquivoId = ImportacaoArquivoId;
		public void AtualizarImportacaoArquivoCategoriaId(int ImportacaoArquivoCategoriaId) => this.ImportacaoArquivoCategoriaId = ImportacaoArquivoCategoriaId;
		public void AtualizarTipoServico(int? TipoServico) => this.TipoServico = TipoServico;
		public void AtualizarChave(string Chave) => this.Chave = Chave;
		public void AtualizarCodigo(string Codigo) => this.Codigo = Codigo;
		public void AtualizarNumero(string Numero) => this.Numero = Numero;
		public void AtualizarDigitoVerificador(int? DigitoVerificador) => this.DigitoVerificador = DigitoVerificador;
		public void AtualizarSerie(string Serie) => this.Serie = Serie;
		public void AtualizarDataEmissao(DateTime? DataEmissao) => this.DataEmissao = DataEmissao;
		public void AtualizarValorPrestacaoServico(decimal? ValorPrestacaoServico) => this.ValorPrestacaoServico = ValorPrestacaoServico;
		#endregion
    }
}      
