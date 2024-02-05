using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class UsuarioTipoViewModel : IViewModel<UsuarioTipo>
    {
		public int Id { get; set; }
        public string Descricao { get; set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
		public UsuarioTipo Model()
		{
			return new UsuarioTipo(Id,Descricao);
		}
    }
}      
