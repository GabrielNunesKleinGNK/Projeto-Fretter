using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fusion
{
    public class EmpresaCanalVenda
    {
        public int EmpresaId { get; set; }
        public string Cnpj { get; set; }
        public string CanalVendaNome { get; set; }
        public string Modalidades { get; set; }
        public string TokenNome { get; set; }
        public int TipoInterfaceId { get; set; }
        public bool CorreioBalcao { get; set; }
        public string TokenRetorno { get; set; }
        public string MensagemRetorno { get; set; }
        public int CodigoIntegracao { get; set; }
        public int Cep { get; set; }
    }
}
