using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Api.Models
{
    public class RegraItemViewModel : IViewModel<RegraItem>
    {
        public int Id { get; set; }
        public int RegraId { get; set; }
        public int RegraGrupoItemId { get; set; }
        public int RegraTipoItemId { get; set; }
        public int RegraTipoOperadorId { get; set; }
        public string Valor { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }

        public RegraGrupoItemViewModel RegraGrupoItem { get; set; }

        public RegraItem Model()
        {
            var model = new RegraItem(Id, RegraId, RegraGrupoItemId, RegraTipoItemId, RegraTipoOperadorId, Valor, ValorInicial, ValorFinal);

            model.AtualizarRegraGrupoItem(RegraGrupoItem.Model());

            return model;
        }
    }
}
