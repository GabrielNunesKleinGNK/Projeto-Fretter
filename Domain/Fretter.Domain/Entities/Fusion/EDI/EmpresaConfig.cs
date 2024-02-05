using System;

namespace Fretter.Domain.Entities
{
    public class EmpresaConfig : EntityBase
    {
        #region "Construtores"
        public EmpresaConfig(int Id, bool NaoUsaEndpointExterno, DateTime Inclusao, bool SplitCanal, bool ControlaSaldoProdutos, int? QtdTabelas)
        {
            this.Ativar();
            this.Id = Id;
            this.NaoUsaEndpointExterno = NaoUsaEndpointExterno;
            this.Inclusao = Inclusao;
            this.Ativo = Ativo;
            this.SplitCanal = SplitCanal;
            this.ControlaSaldoProdutos = ControlaSaldoProdutos;
            this.QtdTabelas = QtdTabelas;
        }
        #endregion

        #region "Propriedades"
        public bool NaoUsaEndpointExterno { get; protected set; }
        public DateTime Inclusao { get; protected set; }
        public bool SplitCanal { get; protected set; }
        public bool ControlaSaldoProdutos { get; protected set; }
        public int? QtdTabelas { get; protected set; }
        #endregion

        #region "Referencias"
        public Empresa Empresa { get; private set; }
        #endregion

        #region "Métodos"
        public void AtualizarNaoUsaEndpointExterno(bool NaoUsaEndpointExterno) => this.NaoUsaEndpointExterno = NaoUsaEndpointExterno;
        public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarSplitCanal(bool SplitCanal) => this.SplitCanal = SplitCanal;
        public void AtualizarControlaSaldoProdutos(bool ControlaSaldoProdutos) => this.ControlaSaldoProdutos = ControlaSaldoProdutos;
        public void AtualizarQtdTabelas(int? QtdTabelas) => this.QtdTabelas = QtdTabelas;
        #endregion
    }
}
