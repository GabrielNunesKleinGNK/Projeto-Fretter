using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class RegraEstoque : EntityBase
    {
        #region "Construtores"
        public RegraEstoque(int id, int empresaId, int canalIdOrigem, int canalIdDestino, int? grupoId, string skus)
        {
            this.Ativar();
            this.Id = id;
            this.EmpresaId = empresaId;
            this.CanalIdOrigem = canalIdOrigem;
            this.CanalIdDestino = canalIdDestino;
            this.GrupoId = grupoId;
            this.Skus = skus;
        }
        #endregion

        #region "Propriedades"
        public int EmpresaId { get; protected set; }
        public int CanalIdOrigem { get; protected set; }
        public int CanalIdDestino { get; protected set; }
        public int? GrupoId { get; protected set; }
        public string Skus { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual Empresa Empresa { get; set; }
        public virtual Canal CanalOrigem { get; set; }
        public virtual Canal CanalDestino { get; set; }
        public virtual Grupo Grupo { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresaId(int empresaId) => this.EmpresaId = empresaId;
        #endregion
    }
}
