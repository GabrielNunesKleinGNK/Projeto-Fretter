using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities
{
    public class SistemaMenu : EntityBase
    {
        public SistemaMenu(int id, string descricao, string rota, string icone)
        {
            Id = id;
            Descricao = descricao;
            Rota = rota;
            Icone = icone;
        }

        public int? ParentId { get; protected set; }
        public string Descricao { get; protected set; }
        public string Rota { get; protected set; }
        public string Icone { get; protected set; }
        public int Ordem { get; protected set; }
    }
}
