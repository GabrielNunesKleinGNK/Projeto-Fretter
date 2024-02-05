using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class DashboardFiltro
    {
        public DashboardFiltro()
        {
        }

        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int? UsuarioId { get; set; }
        public int? TransportadorId { get; set; }
        public int? CanalId { get; set; }
        public int EmpresaId { get; set; }
    }
}
