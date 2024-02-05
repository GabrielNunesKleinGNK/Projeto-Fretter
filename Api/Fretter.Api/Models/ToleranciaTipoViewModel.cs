using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ToleranciaTipoViewModel : IViewModel<ToleranciaTipo>
    {
		public int Id { get; set; }
        public string Descricao { get; set; }
		public ToleranciaTipo Model()
		{
			return new ToleranciaTipo(Id,Descricao);
		}
    }
}      
