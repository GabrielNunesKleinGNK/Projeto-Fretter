using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class FaturaCicloViewModel : IViewModel<FaturaCiclo>
    {
        public int Id { get; set; }
        public int FaturaCicloTipoId { get; set; }
        public short DiaFechamento { get; set; }
        public short DiaVencimento { get; set; }
        public string Descricao { get; set; }


        public FaturaCiclo Model()
        {
            return new FaturaCiclo(Id, FaturaCicloTipoId, DiaFechamento, DiaVencimento);
        }
    }
}
