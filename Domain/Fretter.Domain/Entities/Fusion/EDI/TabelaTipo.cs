using System;

namespace Fretter.Domain.Entities
{
    public class TabelaTipo : EntityBase
    {
		#region "Construtores"
		public TabelaTipo (int Id,string Tipo,int EmpresaId,DateTime Inclusao)
		{
			this.Ativar();
			this.Id = Id;
			this.Tipo = Tipo;
			this.EmpresaId = EmpresaId;
			this.Inclusao = Inclusao;
			this.Ativo = Ativo;
		}
		#endregion

		#region "Propriedades"
        public string Tipo { get; protected set; }
        public int EmpresaId { get; protected set; }
        public DateTime Inclusao { get; protected set; }
		#endregion

		#region "Referencias"
		#endregion

		#region "Métodos"
		public void AtualizarTipo(string Tipo) => this.Tipo = Tipo;
		public void AtualizarEmpresa(int Empresa) => this.EmpresaId = Empresa;
		public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
		#endregion
    }
}      
