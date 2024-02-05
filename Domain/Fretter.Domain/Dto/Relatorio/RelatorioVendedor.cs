using System;
namespace Fretter.Domain.Dto.Relatorio
{
    public class RelatorioVendedor
    {
        public RelatorioVendedor()
        {

        }

        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public int AvaliacaoId { get; set; }
        public string AvaliacaoNome { get; set; }
        public int Quantidade { get; set; }
        public double Ponto { get; set; }
        public double Percentual { get; set; }
    }
}
