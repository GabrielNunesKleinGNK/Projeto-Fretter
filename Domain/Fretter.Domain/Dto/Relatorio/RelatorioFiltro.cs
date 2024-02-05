using System;
using System.Collections.Generic;
using Fretter.Domain.Entities;

namespace Fretter.Domain.Dto.Relatorio
{
    public class RelatorioFiltro
    {
        public RelatorioFiltro()
        {
        }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public int SegmentoId { get; set; }
        public int EmpresaId { get; set; }
        public int UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public int ProdutoCategoriaId { get; set; }
        public int AvaliacaoId { get; set; }
        public string UF { get; set; }
        public int UsuarioLogado { get; set; }
    }
}