using Fretter.Domain.Enum;
using System;
using static Fretter.Domain.Enum.Webhook.Enums;

namespace Fretter.Domain.Entities
{
    public class ArquivoImportacao : EntityBase
    {
        #region "Construtores"
        public ArquivoImportacao() { }

        public ArquivoImportacao(string dsNome, string dsExtensao, OrigemImportacao origemImportacao) 
        {
            this.DsNome = dsNome;
            this.DsExtensao = dsExtensao;
            this.OrigemId = (byte)origemImportacao;
        }
        public ArquivoImportacao(int Id, DateTime dtImportacao, string dsNome, DateTime? dtTratamento, bool flgTratamento,
            byte[] dsArquivo, int origemId, int? idArquivoImportacaoPai, DateTime? dtImportacaoDATE, bool flComprimido, string dsExtensao)
        {
            this.Ativar();
            this.Id = Id;
            this.DtImportacao = dtImportacao;
            this.DsNome = dsNome;
            this.DtTratamento = dtTratamento;
            this.FlgTratamento = flgTratamento;
            this.DsArquivo = dsArquivo;
            this.OrigemId = origemId;
            this.IdArquivoImportacaoPai = idArquivoImportacaoPai;
            this.DtImportacaoDATE = dtImportacaoDATE;
            this.FlComprimido = flComprimido;
            this.DsExtensao = dsExtensao;
        }
        #endregion

        #region "Propriedades"
        
        public DateTime DtImportacao { get; protected set; }
        public string DsNome { get; protected set; }
        public DateTime? DtTratamento { get; protected set; }
        public bool FlgTratamento { get; protected set; }
        public byte[] DsArquivo { get; protected set; }
        public int OrigemId { get; protected set; }
        public int? IdArquivoImportacaoPai { get; protected set; }
        public DateTime? DtImportacaoDATE { get; protected set; }
        public bool FlComprimido { get; protected set; }
        public string DsExtensao { get; protected set; }
        
        #endregion

    }
}
