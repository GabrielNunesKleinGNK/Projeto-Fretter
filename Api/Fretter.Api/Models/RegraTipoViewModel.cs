using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Enum;

namespace Fretter.Api.Models
{
    public class RegraTipoViewModel : IViewModel<RegraTipo>
    {
        public int Id { get; set; }
        public string DescricaoRegraTipo { get; set; }

        public RegraTipo Model()
        {
            return new RegraTipo(Id, DescricaoRegraTipo);
        }
    }
}
