using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ConciliacaoTipoViewModel : IViewModel<ConciliacaoTipo>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool? Ativo { get; set; }
        public ConciliacaoTipo Model()
        {
            return new ConciliacaoTipo(Id, Nome);
        }
    }
}
