using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class RegraGrupoItem : EntityBase
    {
        public RegraGrupoItem(int id, string nome, string tipo)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
        }

        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
}
