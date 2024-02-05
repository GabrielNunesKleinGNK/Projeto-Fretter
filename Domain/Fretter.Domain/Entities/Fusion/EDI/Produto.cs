using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Fretter.Domain.Entities.Fusion.EDI
{
    public class Produto : EntityBase
    {
        #region "Construtores"

        public Produto()
        {
        }

        public Produto(int id, int empresaId, string codigo, string nome, decimal preco, decimal pesoLiq, decimal largura, decimal altura, decimal comprimento)
        {
            Id = id;
            EmpresaId = empresaId;
            Codigo = codigo;
            Nome = nome;
            Preco = preco;
            PesoLiq = pesoLiq;
            Largura = largura;
            Altura = altura;
            Comprimento = comprimento;
        }

        #endregion

        #region "Propriedades"
        public int EmpresaId { get; protected set; }
        public string Codigo { get; protected set; }
        public string CodigoPai { get; protected set; }
        public string Nome { get; protected set; }
        public string Complementar { get; protected set; }
        public int Unidade { get; protected set; }
        public decimal Preco { get; protected set; }
        public decimal PrecoCusto { get; protected set; }
        public decimal PesoLiq { get; protected set; }
        public decimal PesoBruto { get; protected set; }
        public string Gtin { get; protected set; }
        public string NomeFornecedor { get; protected set; }
        public string Marca { get; protected set; }
        public string ClassificacaoFiscal { get; protected set; }
        public string Cest { get; protected set; }
        public int? ItensPorCaixa { get; protected set; }
        public decimal Largura { get; protected set; }
        public decimal Altura { get; protected set; }
        public decimal Comprimento { get; protected set; }
        public int? Crossdocking { get; protected set; }

        #endregion

        #region "Referencias"
        #endregion

        #region "Métodos"
        #endregion
    }
}
