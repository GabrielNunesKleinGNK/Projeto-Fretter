using System;
namespace Fretter.Domain.Dto.Relatorio
{
    public class RelatorioEmpresa
    {
        public RelatorioEmpresa()
        {
        }

        public int AvalicacaoId { get; set; }
        public string ProdutoCategoria { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public decimal Pontuacao { get; set; }
    }
}
