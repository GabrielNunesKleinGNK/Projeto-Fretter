using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaTransporteConfiguracaoItem : EntityBase
    {
        #region "Construtores"
        public EmpresaTransporteConfiguracaoItem(int Id, int EmpresaTransporteConfiguracaoId, string CodigoServico, string CodigoServicoCategoria, string CodigoIntegracao, string Nome,
            DateTime? DataCadastroServico, DateTime? VigenciaInicial, DateTime? VigenciaFinal, DateTime? DataAtualizacao)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaTransporteConfiguracaoId = EmpresaTransporteConfiguracaoId;
            this.CodigoServico = CodigoServico;
            this.CodigoServicoCategoria = CodigoServicoCategoria;
            this.CodigoIntegracao = CodigoIntegracao;
            this.Nome = Nome;
            this.DataCadastroServico = DataCadastroServico;
            this.VigenciaInicial = VigenciaInicial;
            this.VigenciaFinal = VigenciaFinal;
            this.DataAtualizacao = DataAtualizacao;
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"
        public int EmpresaTransporteConfiguracaoId { get; protected set; }
        public string CodigoServico { get; protected set; }
        public string CodigoServicoCategoria { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public string Nome { get; protected set; }
        public DateTime? DataCadastroServico { get; protected set; }
        public DateTime? VigenciaInicial { get; protected set; }
        public DateTime? VigenciaFinal { get; protected set; }
        public DateTime? DataAtualizacao { get; protected set; }

        #endregion

        #region "Referencias"
        //public virtual EmpresaTransporteConfiguracao EmpresaTransporteConfiguracao { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresaTransporteConfiguracaoId(int EmpresaTransporteConfiguracaoId) => this.EmpresaTransporteConfiguracaoId = EmpresaTransporteConfiguracaoId;
        public void AtualizarCodigoServico(string CodigoServico) => this.CodigoServico = CodigoServico;
        public void AtualizarCodigoServicoCategoria(string CodigoServicoCategoria) => this.CodigoServicoCategoria = CodigoServicoCategoria;
        public void AtualizarCodigoIntegracao(string CodigoIntegracao) => this.CodigoIntegracao = CodigoIntegracao;
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarDataCadastroServico(DateTime? DataCadastroServico) => this.DataCadastroServico = DataCadastroServico;
        public void AtualizarVigenciaInicial(DateTime? VigenciaInicial) => this.VigenciaInicial = VigenciaInicial;
        public void AtualizarVigenciaFinal(DateTime? VigenciaFinal) => this.VigenciaFinal = VigenciaFinal;
        public void AtualizarDataAtualizacao(DateTime? DataAtualizacao) => this.DataAtualizacao = DataAtualizacao;
        #endregion
    }
}
