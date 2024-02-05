using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class RegraItem : EntityBase
    {
        public RegraItem(int id, int regraId, int regraGrupoItemId, int regraTipoItemId, int regraTipoOperadorId, string valor, string valorInicial, string valorFinal)
        {
            Id = id;
            RegraId = regraId;
            RegraGrupoItemId = regraGrupoItemId;
            RegraTipoItemId = regraTipoItemId;
            RegraTipoOperadorId = regraTipoOperadorId;
            Valor = valor;
            ValorInicial = valorInicial;
            ValorFinal = valorFinal;
        }

        public int RegraId { get; set; }
        public int RegraGrupoItemId { get; set; }
        public int RegraTipoItemId { get; set; }
        public int RegraTipoOperadorId { get; set; }
        public string Valor { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }

        public AgendamentoRegra AgendamentoRegra { get; set; }
        public RegraGrupoItem RegraGrupoItem { get; set; }

        public void AtualizarRegraGrupoItem(RegraGrupoItem regraGrupoItem) => RegraGrupoItem= regraGrupoItem;
    }
}
