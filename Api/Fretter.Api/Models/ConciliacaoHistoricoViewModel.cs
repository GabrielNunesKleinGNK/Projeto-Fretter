using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ConciliacaoHistoricoViewModel : IViewModel<ConciliacaoHistorico>
    {
		public int Id { get; set; }
        public Int64? ConciliacaoHistoricoId { get; set; }
        public Int64 ConciliacaoId { get; set; }
        public string Descricao { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
		public ConciliacaoHistorico Model()
		{
			return new ConciliacaoHistorico(Id,ConciliacaoHistoricoId,ConciliacaoId,Descricao);
		}
    }
}      
