using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Api.Models
{
    public class RegraGrupoItemViewModel : IViewModel<RegraGrupoItem>
    {
        public int Id { get; set; }
        public string Grupo { get; set; }
        public string Tipo { get; set; }

        public RegraGrupoItem Model()
        {
            return new RegraGrupoItem(Id, Grupo, Tipo);
        }
    }
}
