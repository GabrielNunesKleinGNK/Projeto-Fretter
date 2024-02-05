using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class FaturaPeriodoViewModel : IViewModel<FaturaPeriodo>
    {
        public int  Id { get; set; }
        public string Descricao { get; set; }

        public int? FaturaCicloId { get;  set; }
        public int? DiaVencimento { get;  set; }
        public DateTime DataInicio { get;  set; }
        public DateTime DataFim { get;  set; }
        public bool Vigente { get;  set; }
        public bool Processado { get;  set; }
        public DateTime DataProcessamento { get;  set; }
        public int? QuantidadeProcessado { get;  set; }
        public int? DuracaoProcessamento { get;  set; }

        public FaturaPeriodo Model()
        {
            return new FaturaPeriodo( Id,   FaturaCicloId,  DiaVencimento,  DataInicio,  DataFim,  Vigente,  Processado,
             DataProcessamento,   QuantidadeProcessado,   DuracaoProcessamento);
        }
    }
}
