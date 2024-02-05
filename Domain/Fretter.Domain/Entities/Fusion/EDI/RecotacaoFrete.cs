using Fretter.Domain.Enum;
using System;

namespace Fretter.Domain.Entities
{
    public class RecotacaoFrete : EntityBase
    {
        #region "Construtores"
        public RecotacaoFrete(int Id, int idEmpresa, int idRecotacaoFreteStatus, int idRecotacaoFreteTipo, string dsNomeArquivo, string dsUrl,
            DateTime? dtAtualizacao, int? qtAdvertencia, int? qtErros)
        {
            Ativar();
            this.Id = Id;
            this.IdEmpresa = idEmpresa;
            this.IdRecotacaoFreteStatus = idRecotacaoFreteStatus;
            this.IdRecotacaoFreteTipo = idRecotacaoFreteTipo;
            this.DsNomeArquivo = dsNomeArquivo;
            this.DsUrl = dsUrl;
            this.DtAtualizacao = dtAtualizacao;
            this.QtAdvertencia = qtAdvertencia;
            this.QtErros = qtErros;
            this.DtInclusao = DateTime.Now;
        }
        #endregion

        #region "Propriedades"
        public int IdEmpresa { get; protected set; }
        public int IdRecotacaoFreteStatus { get; protected set; }
        public int IdRecotacaoFreteTipo { get; protected set; }
        public int? IdCanalVenda { get; protected set; }
        public string DsNomeArquivo { get; protected set; }
        public string DsUrl { get; protected set; }
        public string ObjJsonRetorno { get; protected set; }
        public int? QtAdvertencia { get; protected set; }
        public int? QtErros { get; protected set; }
        public bool PriorizarPrazo { get; protected set; }
        public DateTime DtInclusao { get; protected set; }
        public DateTime? DtAtualizacao { get; protected set; }
        #endregion

        public void AtualizarStatus(EnumRecotacaoFreteStatus idRecotacaoStatus)
        {
            this.DtAtualizacao = DateTime.Now;
            this.IdRecotacaoFreteStatus = (int)idRecotacaoStatus;
        }


        public void AtualizarRetorno(string jsonRetorno) => this.ObjJsonRetorno = jsonRetorno;

        public void AtualizarQtAdvertencia(int qtAdvertencia)=> this.QtAdvertencia = qtAdvertencia;
        public void AtualizarQtErro(int qtErro) => this.QtErros = qtErro;
    }
}