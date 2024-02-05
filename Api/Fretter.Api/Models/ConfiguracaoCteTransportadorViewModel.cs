using System;
using Fretter.Api.Helpers;
using Fretter.Api.Models.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;

namespace Fretter.Api.Models
{
    public class ConfiguracaoCteTransportadorViewModel : IViewModel<ConfiguracaoCteTransportador>
    {
		public int Id { get; set; }
        public int TransportadorCnpjId { get; set; }
        public int ConfiguracaoCteTipoId { get; set; }
        public string Alias { get; set; }
        public DateTime DataCadastro { get; set; }

        public TransportadorCnpjViewModel TransportadorCnpj { get; set; }
        public ConfiguracaoCteTipoViewModel ConfiguracaoCteTipo { get; set; }
        public ConfiguracaoCteTransportador Model()
		{
			return new ConfiguracaoCteTransportador(Id, TransportadorCnpjId, (EnumConfiguracaoCteTipo)ConfiguracaoCteTipoId, Alias);
		}
    }
}      
