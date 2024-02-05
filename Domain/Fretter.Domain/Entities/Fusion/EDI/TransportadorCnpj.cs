using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fusion
{
    public class TransportadorCnpj : EntityBase
    {
        public TransportadorCnpj() { }

        public int Id { get; protected set; }
        public int TransportadorId { get; protected set; }
        public string CNPJ { get; protected set; }
        public string Nome { get; protected set; }
        public string Apelido { get; protected set; }
        public bool Ativo { get; protected set; }
    }
}
