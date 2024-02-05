using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Enum;

namespace Fretter.Api.Models
{
    public class RegraTipoItemViewModel : IViewModel<RegraTipoItem>
    {
        public int Id { get; set; }
        public int RegraTipoId { get; set; }
        public string Nome { get; set; }
        public int DadoTipo { get; set; }
        public bool Range { get; set; }

        public RegraTipoItem Model()
        {
            return new RegraTipoItem(Id, RegraTipoId, Nome, DadoTipo, Range);
        }
    }
}
