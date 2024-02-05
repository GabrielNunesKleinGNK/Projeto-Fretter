using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class Transportador
    {
        public Transportador()
        {
        }

        public int Id{ get; set; }
        public string TransportadorNome{ get; set; }
        public int EmpresaId{ get; set; }
    }
}
