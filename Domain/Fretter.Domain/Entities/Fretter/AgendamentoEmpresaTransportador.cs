namespace Fretter.Domain.Entities
{
    public class AgendamentoEmpresaTransportador : EntityBase
    {
        #region Construtores
        public AgendamentoEmpresaTransportador(int idEmpresa, int idTransportador, int? idTransportadorCnpj, bool flgExpedicaoAutomatica, byte nrPrazoComercial)
        {
            this.Id_Empresa = idEmpresa;
            this.Id_Transportador = idTransportador;
            this.Id_TransportadorCnpj = idTransportadorCnpj;
            this.Flg_ExpedicaoAutomatica = flgExpedicaoAutomatica;
            this.Nr_PrazoComercial = nrPrazoComercial;
        }
        #endregion

        #region "Propriedades"
        public int Id_Empresa { get; set; }
        public int Id_Transportador { get; set; }
        public int? Id_TransportadorCnpj { get; set; }
        public bool Flg_ExpedicaoAutomatica { get; set; }
        public byte Nr_PrazoComercial { get; set; }
        #endregion
    }
}
