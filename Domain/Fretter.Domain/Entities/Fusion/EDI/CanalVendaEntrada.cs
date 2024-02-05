using System;

namespace Fretter.Domain.Entities
{
    public class CanalVendaEntrada : EntityBase
    {
        #region "Construtores"
        public CanalVendaEntrada(int Id, string CanalVendaNome, int EmpresaId, int CanalVendaId)
        {
            this.Ativar();
            this.Id = Id;
            this.CanalVendaNome = CanalVendaNome;
            this.EmpresaId = EmpresaId;
            this.CanalVendaId = CanalVendaId;
        }
        #endregion

        #region "Propriedades"
        public string CanalVendaNome { get; protected set; }
        public int EmpresaId { get; protected set; }
        public int CanalVendaId { get; protected set; }
        public byte? OrigemImportacao { get; protected set; }
        #endregion

        #region "Referencias"
        public CanalVenda CanalVenda { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarCanalVenda(string CanalVenda) => this.CanalVendaNome = CanalVenda;
        public void AtualizarEmpresa(int Empresa) => this.EmpresaId = Empresa;
        public void AtualizarCanalVenda(int CanalVenda) => this.CanalVendaId = CanalVenda;
        public void AtualizarOrigemImportacao(byte? OrigemImportacao) => this.OrigemImportacao = OrigemImportacao;
        #endregion
    }
}
