using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class ImportacaoArquivoCritica : EntityBase
    {
        #region Construtores
        public ImportacaoArquivoCritica(int id, int importacaoArquivoId, string descricao)
        {
            Id = id;
            ImportacaoArquivoId = importacaoArquivoId;
            Descricao = descricao;
        }

        public ImportacaoArquivoCritica(int importacaoArquivoId, string descricao, int? linha, string campo)
        {
            ImportacaoArquivoId = importacaoArquivoId;
            Descricao = descricao;
            Linha = linha;
            Campo = campo;
        }
        #endregion

        #region Propriedades
        public int ImportacaoArquivoId { get; set; }
        public string Descricao { get; set; }
        public int? Linha { get; set; }
        public string Campo { get; set; }
        #endregion

        public virtual ImportacaoArquivo ImportacaoArquivo { get; set; }

        #region Metodos

        #endregion
    }
}