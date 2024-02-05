using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fretter.Conciliacao
{
    public class RelatorioDetalhadoFiltroDTO
    {
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public int EmpresaId { get; set; }
        public int FaturaId { get; set; }
        public int TransportadorId { get; set; }
        public int StatusId { get; set; }
        public string CodigoEntrega { get; set; }
        public string CodigoPedido { get; set; }
        public string CodigoDanfe { get; set; }

        //Filtro Procedure Paginada
        public int PageSelected { get; set; }
        public int PageSize { get; set; }
        public string OrderByDirection { get; set; }
        public int ListSize { get; set; }
    }
}
