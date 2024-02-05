using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fatura
{
    public class FaturaArquivoDTO
    {
        public int FaturaArquivoId { get; set; }
        public int EmpresaId { get; set; }
        public string NomeArquivo { get; set; }
        public string UrlBlobStorage { get; set; }
        public int QtdeRegistros { get; set; }
        public int QtdeCriticas { get; set; }
        public decimal ValorTotal { get; set; }
        public string TransportadorCnpj { get; set; }
        public bool Faturado { get; set; }
        public int? UsuarioCadastro { get; set; }
    }
}
