namespace Fretter.Domain.Entities
{
    public class AgendamentoEntregaProduto : EntityBase
    {
        #region Construtores
        public AgendamentoEntregaProduto() { }
        public AgendamentoEntregaProduto(int id, int? idEntrega, string cdSKU, string cdEAN, string dsNome, decimal vlProduto, decimal vlTotal, decimal vlAltura, decimal vlLargura,
            decimal vlComprimento, decimal vlPeso)
        {
            this.Id = id;
            this.EntregaId = idEntrega;
            this.SKU = cdSKU;
            this.EAN = cdEAN;
            this.Nome = dsNome;
            this.ValorProduto = vlProduto;
            this.ValorTotal = vlTotal;
            this.Altura = vlAltura;
            this.Largura = vlLargura;
            this.Comprimento = vlComprimento;
            this.Peso = vlPeso;
        }
        #endregion

        #region "Propriedades"
        public int? EntregaId { get; protected set; }
        public string SKU { get; protected set; }
        public string EAN { get; protected set; }
        public string Nome { get; protected set; }
        public decimal ValorProduto { get; protected set; }
        public decimal ValorTotal { get; protected set; }
        public decimal Altura { get; protected set; }
        public decimal Largura { get; protected set; }
        public decimal Comprimento{ get; protected set; }
        public decimal Peso { get; protected set; }


        public AgendamentoEntrega Entrega { get; set; }


        public void AtualizarSku(string sku) => this.SKU = sku;
        public void AtualizarEan(string ean) => this.EAN = ean;
        public void AtualizarNome(string nome) => this.Nome = nome;
        public void AtualizarValorProduto(decimal valor) => this.ValorProduto = valor;
        public void AtualizarValorTotal(decimal total) => this.ValorTotal = total;
        public void AtualizarValorAltura(decimal altura) => this.Altura = altura;
        public void AtualizarValorLargura(decimal largura) => this.Largura = largura;
        public void AtualizarValorComprimento(decimal comprimento) => this.Comprimento = comprimento;
        public void AtualizarValorPeso(decimal peso) => this.Peso = peso;
        #endregion
    }
}
