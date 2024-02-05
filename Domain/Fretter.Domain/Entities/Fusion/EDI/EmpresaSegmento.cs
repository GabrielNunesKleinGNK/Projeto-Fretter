using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaSegmento : EntityBase
    {
        #region "Construtores"
        public EmpresaSegmento(int Id, DateTime Inclusao, string Segmento, int? OrigemImportacaoId, int EmpresaId, bool Padrao)
        {
            this.Ativar();
            this.Id = Id;
            this.Inclusao = Inclusao;
            this.Segmento = Segmento;
            this.OrigemImportacaoId = OrigemImportacaoId;
            this.EmpresaId = EmpresaId;
            this.Padrao = Padrao;
            //this.Canals = new List<Canal>();
        }
        #endregion

        #region "Propriedades"		
        public DateTime Inclusao { get; protected set; }
        public string Segmento { get; protected set; }
        public int? OrigemImportacaoId { get; protected set; }
        public int EmpresaId { get; protected set; }
        public bool Padrao { get; protected set; }
        #endregion

        #region "Referencias"
        public Canal Canal { get; private set; }
        public Empresa Empresa { get; private set; }
        #endregion

        #region "Métodos"
        public void AtualizarPadrao(bool padrao) => this.Padrao = padrao;
        public void AtualizarSegmento(string segmento) => this.Segmento = segmento;
        public void AtualizarInclusao(DateTime dataInclusao) => this.Inclusao = dataInclusao;
        public void AtualizarEmpresa(int empresaId) => this.EmpresaId = empresaId;
        public void AtualizarOrigemImportacao(int? origemImportacao) => this.OrigemImportacaoId = origemImportacao;
        public void AtualizarCanal(Canal canal) => this.Canal = canal;
        #endregion
    }
}
