using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class RegraTipo : EntityBase
    {
        public RegraTipo(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
