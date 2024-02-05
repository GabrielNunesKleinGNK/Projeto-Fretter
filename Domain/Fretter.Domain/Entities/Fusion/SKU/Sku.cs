using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Fretter.Domain.Entities.Fusion.SKU
{
    public class Sku : EntityBase
    {
        #region "Construtores"

        public Sku()
        {
        }
        
        public Sku(int id, string codigo, string codigoPai, string nome, string complementar, string unidade, string[] grupos, decimal preco, decimal precoCusto, decimal pesoLiq, decimal pesoBruto,
                   string gtin, string nomeFornecedor, string marca, string classificacaoFiscal, string cest, int? itensPorCaixa, bool todosCds, bool todosCanaisVenda)
        {
            this.Ativar();
            this.Id = id;
            this.Codigo = codigo;
            this.CodigoPai = codigoPai;
            this.Nome = nome;
            this.Complementar = complementar;
            this.Unidade = unidade;
            this.Grupos = grupos;
            this.Preco = preco == default ? precoCusto : preco;
            this.PrecoCusto = precoCusto == default ? preco : precoCusto;
            this.PesoLiq = pesoLiq;
            this.PesoBruto = pesoBruto;
            this.Gtin = gtin;
            this.NomeFornecedor = nomeFornecedor;
            this.Marca = marca;
            this.ClassificacaoFiscal = classificacaoFiscal;
            this.Cest = cest;
            this.ItensPorCaixa = itensPorCaixa;
            this.TodosCds = todosCds;
            this.TodosCanaisVenda = todosCanaisVenda;
        }
        #endregion

        #region "Propriedades"
        [Required]
        public string Codigo { get; protected set; }
        public string CodigoPai { get; protected set; }
        [Required]
        public string Nome { get; protected set; }
        [Required]
        public string Complementar { get; protected set; }
        [Required]
        public string Unidade { get; protected set; }
        public string[] Grupos { get; protected set; }
        public decimal Preco { get; protected set; }
        public decimal PrecoCusto { get; protected set; }
        public decimal PesoLiq { get; protected set; }
        [Required]
        public decimal PesoBruto { get; protected set; }
        public string Gtin { get; protected set; }
        public string NomeFornecedor { get; protected set; }
        public string Marca { get; protected set; }
        public string ClassificacaoFiscal { get; protected set; }
        public string Cest { get; protected set; }
        public int? ItensPorCaixa { get; protected set; }
        public bool TodosCds { get; protected set; }
        public bool TodosCanaisVenda { get; protected set; }
        #endregion

        #region "Referencias"
        [Required]
        public SkuMedidas Medidas { get; set; }
        [Required]
        public SkuEstoqueCd[] EstoqueCd { get; set; }
        [Required]
        public SkuCanalVenda[] CanaisVenda { get; set; }
        #endregion

        #region "Métodos"
        #endregion
    }
}
