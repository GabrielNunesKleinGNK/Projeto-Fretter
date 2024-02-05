using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class FaturaAcaoViewModel : IViewModel<FaturaAcao>
    {
        public int Id { get; set; }
        public string Descricao { get;  set; }
       
        public FaturaAcao Model()
        {
			return new FaturaAcao(Id,  Descricao);

		}
    }
}      
