using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models.Fusion
{
    public class TransportadorViewModel : IViewModel<Transportador>
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }

        public Transportador Model()
        {
            return new Transportador(Id, NomeFantasia);
        }
    }
}
