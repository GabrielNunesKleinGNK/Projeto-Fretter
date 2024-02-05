using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{
    public class EntregaOrigemImportacaoViewModel : IViewModel<EntregaOrigemImportacao>
    {
        public int Id { get; set; }
        public string Nome { get;  set; }

        public EntregaOrigemImportacao Model()
        {
            return new EntregaOrigemImportacao(this.Id, Nome);
        }
    }
}
