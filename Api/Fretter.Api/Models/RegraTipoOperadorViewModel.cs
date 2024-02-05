using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Api.Models
{
    public class RegraTipoOperadorViewModel : IViewModel<RegraTipoOperador>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public RegraTipoOperador Model()
        {
            return new RegraTipoOperador(Id, Nome);
        }
    }
}
