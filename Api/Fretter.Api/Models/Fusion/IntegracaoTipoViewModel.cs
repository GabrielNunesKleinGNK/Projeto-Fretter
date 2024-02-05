using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{
    public class IntegracaoTipoViewModel : IViewModel<IntegracaoTipo>
    {
        public int Id { get; set; }
        public string Nome { get;  set; }

        public IntegracaoTipo Model()
        {
            return new IntegracaoTipo(this.Id, Nome);
        }
    }
}
