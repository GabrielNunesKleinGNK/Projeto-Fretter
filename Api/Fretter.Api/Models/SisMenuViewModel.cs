using Fretter.Domain.Entities;
using Fretter.Api.Helpers;

namespace Fretter.Api.Models
{

    public class SisMenuViewModel : IViewModel<SisMenu>
    {
        public int Id { get; set; }

        public string DsMenu { get;  set; }
        public int? IdPai { get;  set; }

        public bool FlgAtivo { get;  set; }
        public bool FlgFretter { get;  set; }
        public string DsLink { get;  set; }
        public int NrOrdem { get;  set; }
        public string TpPerfil { get;  set; }

        public SisMenu Model()
        {
            return new SisMenu(Id, DsMenu, DsLink, IdPai,NrOrdem,TpPerfil,FlgAtivo,FlgFretter);
        }
    }
}
