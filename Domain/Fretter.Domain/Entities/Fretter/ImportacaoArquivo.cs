using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class ImportacaoArquivo : EntityBase
    {
        #region "Construtores"
        protected ImportacaoArquivo() { }
        public ImportacaoArquivo(int Id, string Nome, int? EmpresaId, int ImportacaoArquivoTipoId, int ImportacaoArquivoStatusId, string Identificador, string Diretorio)
        {
            this.Ativar();
            this.Id = Id;
            this.Nome = Nome;
            this.EmpresaId = EmpresaId;
            this.ImportacaoArquivoTipoId = ImportacaoArquivoTipoId;
            this.ImportacaoArquivoStatusId = ImportacaoArquivoStatusId;
            this.Identificador = Identificador;
            this.Diretorio = Diretorio;
        }

        public ImportacaoArquivo(string nome, int? empresaId, EnumImportacaoArquivoTipo tipo, string identificador, string diretorio, int configuracaoId)
        {
            AtualizarNome(nome);
            AtualizarEmpresaId(empresaId);
            AtualizarDiretorio(diretorio);
            AtualizarIdentificador(identificador);
            AtualizarImportacaoArquivoTipoId(tipo);
            AtualizarStatusId(EnumImportacaoArquivoStatus.Pendente);
            AtualizarImportacaoConfiguracaoId(configuracaoId);
            AtualizarDataCriacao();
            Ativar();
        }

        #endregion

        #region "Propriedades"
        public string Nome { get; protected set; }
        public int? EmpresaId { get; protected set; }
        public int? TransportadorId { get; protected set; }
        public int? ImportacaoConfiguracaoId { get; protected set; }
        public int ImportacaoArquivoStatusId { get; protected set; }
        public int ImportacaoArquivoTipoId { get; protected set; }
        public string Identificador { get; protected set; }
        public string Diretorio { get; protected set; }
        public DateTime? DataProcessamento { get; protected set; }
        public string Mensagem { get; protected set; }
        public int? ImportacaoArquivoTipoItemId { get; protected set; }

        #endregion

        #region "Referencias"
        public virtual List<ImportacaoCte> ImportacaoCtes { get; set; }
        public virtual List<ImportacaoArquivoCritica> ImportacaoArquivoCriticas { get; set; }
        public virtual ImportacaoArquivoStatus ImportacaoArquivoStatus { get; set; }
        public virtual ImportacaoArquivoTipo ImportacaoArquivoTipo { get; protected set; }
        public virtual ImportacaoArquivoTipoItem ImportacaoArquivoTipoItem { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarEmpresaId(int? EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarTransportadorId(int? transportadorId) => this.TransportadorId = transportadorId;
        public void AtualizarImportacaoArquivoTipoId(EnumImportacaoArquivoTipo ImportacaoArquivoTipoId) => this.ImportacaoArquivoTipoId = ImportacaoArquivoTipoId.GetHashCode();
        public void AtualizarIdentificador(string Identificador) => this.Identificador = Identificador;
        public void AtualizarDiretorio(string Diretorio) => this.Diretorio = Diretorio;
        public void AtualizarStatusId(EnumImportacaoArquivoStatus status) => this.ImportacaoArquivoStatusId = status.GetHashCode();
        public void AtualizarImportacaoConfiguracaoId(int configuracaoId) => this.ImportacaoConfiguracaoId = configuracaoId;
        public void AtualizarDataProcessamento(DateTime data) => this.DataProcessamento = data;
        public void AtualizarMensagem(string msg) => this.Mensagem = msg;
        public void AtualizarImportacaoArquivoTipoItemId(EnumImportacaoArquivoTipoItem importacaoArquivoTipoItemId) => this.ImportacaoArquivoTipoItemId = importacaoArquivoTipoItemId.GetHashCode();
        #endregion
    }
}
