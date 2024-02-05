using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fusion
{
    public class EmpresaImportacao
    {
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string CEP { get; set; }
        public string CEPPostagem { get; set; }
        public string UF { get; set; }
        public string Email { get; set; }
        public bool SedexBalcao { get; set; }
        public bool ApiFrete { get; set; }
        public bool Sucesso { get; set; }
        public string Token { get; set; }
        public int ImportacaoDetalheId { get; set; }
    }

    public class EmpresaImportacaoFiltro
    {
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
    }
}
