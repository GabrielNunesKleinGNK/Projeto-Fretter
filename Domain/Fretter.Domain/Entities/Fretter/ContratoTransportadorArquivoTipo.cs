using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class ContratoTransportadorArquivoTipo : EntityBase
    {
        #region "Construtores"
        protected ContratoTransportadorArquivoTipo() { }
        public ContratoTransportadorArquivoTipo(int Id, int? EmpresaId, int TransportadorId, int ImportacaoArquivoTipoItemId, string Alias)
        {
            this.Ativar();
            this.Id = Id;
            this.EmpresaId = EmpresaId;
            this.TransportadorId = TransportadorId;
            this.ImportacaoArquivoTipoItemId = ImportacaoArquivoTipoItemId;
            this.Alias = Alias;
        }

        #endregion

        #region "Propriedades"
        public int? EmpresaId { get; protected set; }
        public int TransportadorId { get; protected set; }
        public int ImportacaoArquivoTipoItemId { get; protected set; }
        public string Alias { get; protected set; }
        #endregion

        #region "Referencias"
        public ImportacaoArquivoTipoItem ImportacaoArquivoTipoItem { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresaId(int EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarTransportadorId(int TransportadorId) => this.TransportadorId = TransportadorId;
        public void AtualizarImportacaoArquivoTipoItemId(int ImportacaoArquivoTipoItemId) => this.ImportacaoArquivoTipoItemId = ImportacaoArquivoTipoItemId;
        public void AtualizarAlias(string Alias) => this.Alias = Alias;
        #endregion
    }
}
