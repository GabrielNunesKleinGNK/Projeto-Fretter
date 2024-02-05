using Fretter.Domain.Helpers.Attributes;
using System;

namespace Fretter.Domain.Entities
{
    public class EntregaDevolucaoOcorrencia : EntityBase
    {
        #region "Construtores"
        public EntregaDevolucaoOcorrencia(int Id, int EntregaDevolucao, int? OcorrenciaEmpresaItem, string CodigoIntegracao, string Ocorrencia, DateTime? DataOcorrencia, string Sigla, DateTime? Inclusao)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaDevolucao = EntregaDevolucao;
            this.OcorrenciaEmpresaItem = OcorrenciaEmpresaItem;
            this.CodigoIntegracao = CodigoIntegracao;
            this.Ocorrencia = Ocorrencia;
            this.DataOcorrencia = DataOcorrencia;
            this.Sigla = Sigla;
            this.Inclusao = Inclusao;
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"
        public int EntregaDevolucao { get; protected set; }
        public int? OcorrenciaEmpresaItem { get; protected set; }
        public string CodigoIntegracao { get; protected set; }
        public string Ocorrencia { get; protected set; }
        public DateTime? DataOcorrencia { get; protected set; }
        public string Observacao { get; protected set; }
        public string Sigla { get; protected set; }
        public DateTime? Inclusao { get; protected set; }
        [DataTableColumnIgnore]
        public int? OcorrenciaTipoId { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        public void AtualizarEntregaDevolucao(int EntregaDevolucao) => this.EntregaDevolucao = EntregaDevolucao;
        public void AtualizarOcorrenciaEmpresaItem(int? OcorrenciaEmpresaItem) => this.OcorrenciaEmpresaItem = OcorrenciaEmpresaItem;
        public void AtualizarOcorrencia(string Ocorrencia) => this.Ocorrencia = Ocorrencia;
        public void AtualizarObservacao(string Observacao) => this.Observacao = Observacao;
        public void AtualizarSigla(string Sigla) => this.Sigla = Sigla;
        public void AtualizarDataOcorrencia(DateTime? DataOcorrencia) => this.DataOcorrencia = DataOcorrencia;
        public void AtualizarInclusao(DateTime? Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarOcorrenciaTipoId(int ocorrenciaTipoId) => this.OcorrenciaTipoId = ocorrenciaTipoId;
        #endregion
    }
}
