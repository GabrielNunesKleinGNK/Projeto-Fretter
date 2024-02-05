using System;

namespace Fretter.Domain.Entities
{
    public class ImportacaoArquivoCategoria : EntityBase
    {
		#region "Construtores"
		public ImportacaoArquivoCategoria (int Id,string Nome,int ImportacaoArquivoTipoId,string Codigo)
		{
			this.Ativar();
			this.Id = Id;
			this.Nome = Nome;
			this.ImportacaoArquivoTipoId = ImportacaoArquivoTipoId;
			this.Codigo = Codigo;
		}
		#endregion

		#region "Propriedades"
        public string Nome { get; protected set; }
        public int ImportacaoArquivoTipoId { get; protected set; }
        public string Codigo { get; protected set; }
		#endregion

		#region "Referencias"
		public ImportacaoArquivoTipo ImportacaoArquivoTipo{get; protected set;}
		#endregion

		#region "Métodos"
		public void AtualizarNome(string Nome) => this.Nome = Nome;
		public void AtualizarImportacaoArquivoTipoId(int ImportacaoArquivoTipoId) => this.ImportacaoArquivoTipoId = ImportacaoArquivoTipoId;
		public void AtualizarCodigo(string Codigo) => this.Codigo = Codigo;
		#endregion
    }
}      
