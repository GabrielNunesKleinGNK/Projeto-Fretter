using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ConfiguracaoCteTipoViewModel : IViewModel<ConfiguracaoCteTipo>
    {
		public int Id { get; set; }
        public string Descricao { get; set; }
        public string Chave { get; set; }
        public ConfiguracaoCteTipo Model()
		{
			return new ConfiguracaoCteTipo(Id,Descricao, Chave);
		}
    }
}      
