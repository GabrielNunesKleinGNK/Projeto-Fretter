using System;

namespace Fretter.Domain.Entities
{
    public class CanalConfig : EntityBase
    {
        #region "Construtores"
        public CanalConfig(int Id, bool? ControlaSaldo, Empresa Empresa)
        {
            this.Id = Id;
            this.ControlaSaldo = ControlaSaldo;
            this.Empresa = Empresa;
        }
        public CanalConfig(int Id, bool? ControlaSaldo, int EmpresaId)
        {
            this.Id = Id;
            this.ControlaSaldo = ControlaSaldo;
            this.EmpresaId = EmpresaId;
        }
        #endregion

        #region "Propriedades"
        public bool? ControlaSaldo { get; protected set; }
        public int EmpresaId { get; protected set; }
        #endregion

        #region "Referencias"		
        public Empresa Empresa { get; private set; }
        public Canal Canal { get; private set; }
        #endregion

        #region "Métodos"
        public void AtualizarControlaSaldo(bool? ControlaSaldo) => this.ControlaSaldo = ControlaSaldo;
        public void AtualizarEmpresa(int Empresa) => this.EmpresaId = Empresa;
        #endregion
    }
}
