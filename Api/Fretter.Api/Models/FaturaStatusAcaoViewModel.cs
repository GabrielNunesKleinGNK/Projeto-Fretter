using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class FaturaStatusAcaoViewModel : IViewModel<FaturaStatusAcao>
    {
        public int Id { get; set; }
        public int FaturaStatusId { get; set; }
        public int FaturaAcaoId { get; set; }
        public int FaturaStatusResultadoId { get; set; }
        public bool Visivel { get; set; }
        public bool InformaMotivo { get; set; }

        public FaturaAcaoViewModel FaturaAcao { get; set; }

        public FaturaStatusAcao Model()
        {
            return new FaturaStatusAcao(Id, FaturaStatusId, FaturaAcaoId, FaturaStatusResultadoId, Visivel, InformaMotivo);
        }
    }
}
