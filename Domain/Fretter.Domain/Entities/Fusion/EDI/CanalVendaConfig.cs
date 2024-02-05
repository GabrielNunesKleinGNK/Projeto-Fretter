using System;

namespace Fretter.Domain.Entities
{
    public class CanalVendaConfig : EntityBase
    {
        #region "Construtores"
        public CanalVendaConfig(int Id, bool? UsaMicroServicoNoTransportador, byte? DescricaoTransportador, int EmpresaId, bool? PrazoAutomatico, bool EmbalagemUnicaMF = false, bool RetornoDesmembradoMF = false)
        {
            this.Id = Id;
            this.UsaMicroServicoNoTransportador = UsaMicroServicoNoTransportador;
            this.DescricaoTransportador = DescricaoTransportador;
            this.EmpresaId = EmpresaId;
            this.PrazoAutomatico = PrazoAutomatico;
            this.EmbalagemUnicaMF = EmbalagemUnicaMF;
            this.RetornoDesmembradoMF = RetornoDesmembradoMF;
        }
        public CanalVendaConfig(int Id, byte? TipoInterface, bool? EmbalagemUnicaMF, int? TimeoutWS, bool? UsaMicroServicoNoTransportador, byte? DescricaoTransportador, bool? PrazoAutomatico, int EmpresaId, int? EmpresaToken, bool RetornoDesmembradoMF)
        {
            this.Id = Id;
            this.TipoInterface = TipoInterface;
            this.EmbalagemUnicaMF = EmbalagemUnicaMF;
            this.TimeoutWS = TimeoutWS;
            this.UsaMicroServicoNoTransportador = UsaMicroServicoNoTransportador;
            this.DescricaoTransportador = DescricaoTransportador;
            this.PrazoAutomatico = PrazoAutomatico;
            this.EmpresaId = EmpresaId;
            this.EmpresaToken = EmpresaToken;
            this.RetornoDesmembradoMF = RetornoDesmembradoMF;
        }
        #endregion

        #region "Propriedades"
        public byte? TipoInterface { get; protected set; }
        public bool? EmbalagemUnicaMF { get; protected set; }
        public int? TimeoutWS { get; protected set; }
        public bool? UsaMicroServicoNoTransportador { get; protected set; }
        public byte? DescricaoTransportador { get; protected set; }
        public bool? PrazoAutomatico { get; protected set; }
        public int EmpresaId { get; protected set; }
        public int? EmpresaToken { get; protected set; }
        public bool RetornoDesmembradoMF { get; protected set; }
        #endregion

        #region "Referencias"
        public CanalVenda CanalVenda { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarTipoInterface(byte? TipoInterface) => this.TipoInterface = TipoInterface;
        public void AtualizarEmbalagemUnicaMF(bool? EmbalagemUnicaMF) => this.EmbalagemUnicaMF = EmbalagemUnicaMF;
        public void AtualizarTimeoutWS(int? TimeoutWS) => this.TimeoutWS = TimeoutWS;
        public void AtualizarUsaMicroServicoNoTransportador(bool? UsaMicroServicoNoTransportador) => this.UsaMicroServicoNoTransportador = UsaMicroServicoNoTransportador;
        public void AtualizarDescricaoTransportador(byte? DescricaoTransportador) => this.DescricaoTransportador = DescricaoTransportador;
        public void AtualizarPrazoAutomatico(bool? PrazoAutomatico) => this.PrazoAutomatico = PrazoAutomatico;
        public void AtualizarEmpresa(int EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarEmpresaToken(int? EmpresaToken) => this.EmpresaToken = EmpresaToken;
        public void AtualizarRetornoDesmembradoMF(bool RetornoDesmembradoMF) => this.RetornoDesmembradoMF = RetornoDesmembradoMF;
        #endregion
    }
}
