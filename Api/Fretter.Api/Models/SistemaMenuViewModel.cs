using Fretter.Domain.Entities;
using Fretter.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{


    public class SistemaMenuViewModel : IViewModel<SistemaMenu>
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Descricao { get; set; }
        public string Rota { get; set; }
        public string Icone { get; set; }

        public SistemaMenu Model()
        {
            return new SistemaMenu(Id, Descricao, Rota, Icone);
        }
    }
}
