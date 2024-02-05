using System;

namespace Fretter.Domain.Entities
{
    public class IndicadorConciliacao : EntityBase
    {
        #region "Construtores"
        public IndicadorConciliacao(int Id)
        {
            this.Ativar();
            this.Id = Id;
        }
        #endregion

        #region "Propriedades"
        public int IndicadorConciliacaoId { get; protected set; }
        public DateTime? Data { get; protected set; }
        public int? EmpresaId { get; protected set; }
        public int? TransportadorId { get; protected set; }
        public int? TransportadorCnpjId { get; protected set; }
        public int? QtdEntrega { get; protected set; }
        public int? QtdCte { get; protected set; }
        public int? QtdSucesso { get; protected set; }
        public int? QtdErro { get; protected set; }
        public int? QtdDivergencia { get; protected set; }
        public int? QtdDivergenciaPeso { get; protected set; }
        public int? QtdDivergenciaTarifa { get; protected set; }
        public double? ValorCustoFreteEstimado { get; protected set; }
        public double? ValorCustoFreteReal { get; protected set; }
        public double? ValorTarifaPesoEstimado { get; protected set; }
        public double? ValorTarifaPesoReal { get; protected set; }
        public DateTime? DataProcessamento { get; protected set; }        
        #endregion

        #region "Referencias"
        //public Usuario UsuarioCadastro{get; protected set;}
        ////public Usuario UsuarioAlteracao{get; protected set;}
        #endregion

        #region "Métodos"
        //public void AtualizarChave(string Chave) => this.Chave = Chave;
        //public void AtualizarValor(string Valor) => this.Valor = Valor;
        #endregion
    }
}
