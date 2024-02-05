using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models.Fusion
{
    public class TransportadorCnpjViewModel : IViewModel<TransportadorCnpj>
    {
        public int Id { get; set; }
        public int TransportadorId { get; set; }
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; }

        public TransportadorCnpj Model()
        {
            return new TransportadorCnpj();
        }
    }
}
