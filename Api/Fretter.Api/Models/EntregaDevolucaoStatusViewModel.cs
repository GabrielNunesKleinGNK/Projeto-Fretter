using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class EntregaDevolucaoStatusViewModel : IViewModel<EntregaDevolucaoStatus>
    {
        public int Id { get; set; }
        public int EntregaTransporteTipoId { get;  set; }
        public string Nome { get;  set; }
        public string CodigoIntegracao { get; set; }
        public string Alias { get; set; }

        public EntregaDevolucaoStatus Model()
        {
			return new EntregaDevolucaoStatus(Id, EntregaTransporteTipoId,  Nome);

		}
    }
}      
