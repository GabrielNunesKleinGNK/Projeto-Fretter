
namespace Fretter.Domain.Entities
{
    public class SisMenu : EntityBase
    {
        #region "Construtores"
        public SisMenu(int id, string dsMenu, string dsLink, int? idPai , int nrOrdem, string tpPerfil, bool flgAtivo, bool flgFretter)
        {
            Id = id;
            DsMenu = dsMenu;
            DsLink = dsLink;
            IdPai = idPai;
            NrOrdem = nrOrdem;
            TpPerfil = tpPerfil;
            FlgAtivo = flgAtivo;
            FlgFretter = flgFretter;
        }

        public string DsMenu { get; protected set; }
        public int? IdPai { get; protected set; }

        public bool FlgAtivo { get; protected set; }
        public bool FlgFretter { get; protected set; }
        public string DsLink { get; protected set; }
        public int NrOrdem { get; protected set; }
        public string TpPerfil { get; protected set; }

        #endregion
    }
}
