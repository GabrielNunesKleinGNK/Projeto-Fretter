using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ConciliacaoStatusViewModel : IViewModel<ConciliacaoStatus>
    {
		public int Id { get; set; }
        public string Nome { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
		public ConciliacaoStatus Model()
		{
			return new ConciliacaoStatus(Id,Nome);
		}
    }
}      
