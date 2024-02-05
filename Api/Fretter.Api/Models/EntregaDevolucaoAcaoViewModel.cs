using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class EntregaDevolucaoAcaoViewModel : IViewModel<EntregaDevolucaoAcao>
    {
        public int Id { get; set; }
        public string Nome { get;  set; }
       
        public EntregaDevolucaoAcao Model()
        {
			return new EntregaDevolucaoAcao(Id,  Nome);

		}
    }
}      
