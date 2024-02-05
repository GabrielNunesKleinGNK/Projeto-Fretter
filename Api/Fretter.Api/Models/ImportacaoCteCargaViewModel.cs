using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ImportacaoCteCargaViewModel : IViewModel<ImportacaoCteCarga>
    {
		public int Id { get; set; }
        public int ImportacaoCteId { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public int? Quantidade { get; set; }
		public ImportacaoCteCarga Model()
		{
			return new ImportacaoCteCarga(Id,ImportacaoCteId, Tipo, Codigo, Quantidade, null);
		}
    }
}      
