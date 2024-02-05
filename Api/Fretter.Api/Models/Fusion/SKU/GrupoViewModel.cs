using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fusion.SKU;
using System;

namespace Fretter.Api.Models.Fusion
{
    public class GrupoViewModel : IViewModel<Grupo>
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public Grupo Model()
        {
            return new Grupo(Id, EmpresaId, Codigo, Descricao);
        }
    }
}
