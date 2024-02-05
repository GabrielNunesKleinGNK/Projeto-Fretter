using Fretter.Domain.Enum;
using System;

namespace Fretter.Domain.Entities
{
    public class TabelaArquivo : EntityBase
    {
        #region "Construtores"
        public TabelaArquivo(int Id, int idTabela, EnumTabelaArquivoStatus idTabelaArquivoStatus, string dsNomeArquivo, string dsUrl, string dsModelo, string dsUrlResultado,
            DateTime? dtAtualizacao, int? qtAdvertencia, int? qtErro)
        {
            this.Ativar();
            this.Id = Id;
            this.IdTabela = idTabela;
            this.IdTabelaArquivoStatus = idTabelaArquivoStatus;
            this.DsNomeArquivo = DsNomeArquivo;
            this.DsUrl = dsUrl;
            this.DsModelo = dsModelo;
            this.DtCriacao = DateTime.Now;
            this.DsUrlResultado = dsUrlResultado;
            this.DtAtualizacao = dtAtualizacao;
            this.QtAdvertencia = qtAdvertencia;
            this.QtErro = qtErro;
        }
        #endregion

        #region "Propriedades"
        public int IdTabela { get; protected set; }
        public EnumTabelaArquivoStatus IdTabelaArquivoStatus { get; protected set; }
        public string DsNomeArquivo { get; protected set; }
        public string DsUrl { get; protected set; }
        public string DsModelo { get; protected set; }
        public DateTime DtCriacao { get; protected set; }
        public string DsUrlResultado { get; protected set; }
        public DateTime? DtAtualizacao { get; protected set; }
        public int? QtAdvertencia { get; protected set; }
        public int? QtErro { get; protected set; }
        #endregion

    }
}
