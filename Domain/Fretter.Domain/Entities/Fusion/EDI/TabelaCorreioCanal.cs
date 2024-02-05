using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class TabelaCorreioCanal : EntityBase
    {
        #region "Construtores"
        public TabelaCorreioCanal(int Id, int TabelaCorreio, int CanalId, DateTime? VigenciaInicio, DateTime? VigenciaFim, DateTime Inclusao, int MicroServicoId,int? Cep)
        {            
            this.Id = Id;
            this.TabelaCorreio = TabelaCorreio;
            this.CanalId = CanalId;
            this.VigenciaInicio = VigenciaInicio;
            this.VigenciaFim = VigenciaFim;
            this.Inclusao = Inclusao;
            this.MicroServicoId = MicroServicoId;
            this.Cep = Cep;
            this.TabelaCorreioCanalTabelaTipos = new List<TabelaCorreioCanalTabelaTipo>();            
        }
        #endregion

        #region "Propriedades"
        public int TabelaCorreio { get; protected set; }
        public int CanalId { get; protected set; }
        public DateTime? VigenciaInicio { get; protected set; }
        public DateTime? VigenciaFim { get; protected set; }
        public DateTime Inclusao { get; protected set; }
        public int MicroServicoId { get; protected set; }
        public int? Cep { get; protected set; }
        #endregion

        #region "Referencias"
        public Canal Canal { get; protected set; }
        public MicroServico MicroServico { get; protected set; }
        public ICollection<TabelaCorreioCanalTabelaTipo> TabelaCorreioCanalTabelaTipos { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarTabelaCorreio(int TabelaCorreio) => this.TabelaCorreio = TabelaCorreio;
        public void AtualizarCanal(int Canal) => this.CanalId = Canal;
        public void AtualizarVigenciaInicio(DateTime? VigenciaInicio) => this.VigenciaInicio = VigenciaInicio;
        public void AtualizarVigenciaFim(DateTime? VigenciaFim) => this.VigenciaFim = VigenciaFim;
        public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarMicroServico(int MicroServico) => this.MicroServicoId = MicroServico;
        public void AtualizarCep(int? Cep) => this.Cep = Cep;
        #endregion
    }
}
