using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class Canal
    {
        public Canal()
        {
        }

        public int Id { get; set; }
        public string CanalNome { get; set; }
        public int EmpresaId { get; set; }
    }
}
