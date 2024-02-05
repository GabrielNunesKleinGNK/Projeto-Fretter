using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ConfiguracaoViewModel : IViewModel<Configuracao>
    {
		public int Id { get; set; }
        public string Chave { get; set; }
        public string Valor { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
		public Configuracao Model()
		{
			return new Configuracao(Id,Chave,Valor);
		}
    }
}      
