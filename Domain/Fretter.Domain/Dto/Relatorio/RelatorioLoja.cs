using System;
namespace Fretter.Domain.Dto.Relatorio
{
    public class RelatorioCategoria
    {
        public RelatorioCategoria()
        {
        }

        public string GestorNome { get; set; }
        public string UsuarioNome { get; set; }
        public string NomeLoja { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public double Pontuacao { get; set; }
        public string Categoria { get; set; }
        public double CategoriaPontuacao { get; set; }
        public string Produto { get; set; }
        public double ProdutoPontuacao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
