using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class ImportacaoArquivoTipoItem : EntityBase
    {
        #region "Construtores"
        public ImportacaoArquivoTipoItem(int Id, int ImportacaoArquivoTipoId, int ConciliacaoTipoId)
        {
            this.Ativar();
            this.Id = Id;
            this.ImportacaoArquivoTipoId = ImportacaoArquivoTipoId;
            this.ConciliacaoTipoId = ConciliacaoTipoId;
        }
        #endregion

        #region "Propriedades"
        public int ImportacaoArquivoTipoId { get; protected set; }
        public int ConciliacaoTipoId { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual ImportacaoArquivoTipo ImportacaoArquivoTipo { get; set; }
        public virtual ConciliacaoTipo ConciliacaoTipo { get; set; }
        //public List<ContratoTransportadorArquivoTipo> ContratoTransportadorArquivoTipos { get; set; }
        #endregion

        #region "Métodos"        
        #endregion
    }
}
