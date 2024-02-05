using System;

namespace Fretter.Domain.Entities
{
    public class CanalVendaInterface : EntityBase
    {
        #region "Construtores"
        public CanalVendaInterface(int Id, int CanalVendaId, Int16 TipoInterface, byte DescricaoTransportador, int? TimeoutWS, int EmpresaTokenId, int EmpresaId)
        {
            this.Ativar();
            this.Id = Id;
            this.CanalVendaId = CanalVendaId;
            this.TipoInterface = TipoInterface;
            this.DescricaoTransportador = DescricaoTransportador;
            this.TimeoutWS = TimeoutWS;
            this.EmpresaTokenId = EmpresaTokenId;
            this.EmpresaId = EmpresaId;
        }
        #endregion

        #region "Propriedades"
        public int CanalVendaId { get; protected set; }
        public Int16 TipoInterface { get; protected set; }
        public byte DescricaoTransportador { get; protected set; }
        public int? TimeoutWS { get; protected set; }
        public int EmpresaTokenId { get; protected set; }
        public int EmpresaId { get; protected set; }
        #endregion

        #region "Referencias"        
        public EmpresaToken EmpresaToken { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarCanalVenda(int CanalVenda) => this.CanalVendaId = CanalVenda;
        public void AtualizarTipoInterface(Int16 TipoInterface) => this.TipoInterface = TipoInterface;
        public void AtualizarDescricaoTransportador(byte DescricaoTransportador) => this.DescricaoTransportador = DescricaoTransportador;
        public void AtualizarTimeoutWS(int? TimeoutWS) => this.TimeoutWS = TimeoutWS;
        public void AtualizarEmpresaToken(int EmpresaToken) => this.EmpresaTokenId = EmpresaToken;
        public void AtualizarEmpresa(int Empresa) => this.EmpresaId  = Empresa;
        #endregion
    }
}
