using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Api.Models
{
    public class ImportacaoArquivoCriticaViewModel : IViewModel<ImportacaoArquivoCritica>
    {
		public int Id { get; set; }
        public string Descricao { get; set; }
        public int Linha { get; set; }
        public string Campo { get; set; }

        public ImportacaoArquivoCritica Model()
		{
			return new ImportacaoArquivoCritica(Id, Descricao, Linha, Campo);
		}
    }
}      
