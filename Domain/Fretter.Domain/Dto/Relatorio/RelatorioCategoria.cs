using System;
namespace Fretter.Domain.Dto.Relatorio
{
    public class RelatorioLoja
    {
        public RelatorioLoja()
        {
        }

        public int AvaliacaoUsuarioId { get; set; }
        public int AvaliacaoId { get; set; }
        public string AvaliacaoNome { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public string NomeLoja { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public double Pontuacao { get; set; }
    }
}
