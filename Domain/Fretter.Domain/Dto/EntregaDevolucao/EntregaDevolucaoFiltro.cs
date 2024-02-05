using System;
namespace Fretter.Domain.Dto.EntregaDevolucao
{
    public class EntregaDevolucaoFiltro
    {
        public EntregaDevolucaoFiltro()
        {
        }

        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public string Danfe { get; set; }
        public string GerencialId { get; set; }
        public int? EntregaDevolucaoStatusId { get; set; }
    }
}
