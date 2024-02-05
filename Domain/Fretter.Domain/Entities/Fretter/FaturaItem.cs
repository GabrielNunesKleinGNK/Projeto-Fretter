using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class FaturaItem : EntityBase
    {
        #region "Construtores"
        public FaturaItem(int Id, decimal Valor, string Descricao, int FaturaId)
        {
            this.Ativar();
            this.Id = Id;
            this.Valor = Valor;
            this.Descricao = Descricao;
            this.FaturaId = FaturaId;
        }

        #endregion

        #region "Propriedades"
        public decimal Valor { get; protected set; }
        public string Descricao { get; protected set; }
        public int FaturaId { get; protected set; }
        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        public void AtualizarValor(decimal Valor) => this.Valor = Valor;
        public void AtualizarDescricao(string Descricao) => this.Descricao = Descricao;
        public void AtualizarFaturaId(int FaturaId) => this.FaturaId = FaturaId;
        #endregion
    }
}
