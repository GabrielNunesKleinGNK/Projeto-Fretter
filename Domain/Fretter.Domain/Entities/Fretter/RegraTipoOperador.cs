using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class RegraTipoOperador : EntityBase
    {
        public RegraTipoOperador(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
