using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fretter.Domain.Dto.EmpresaIntegracao
{
    public class DeParaEmpresaIntegracao
    {
        public string Nome { get; set; }
        public string Campo { get; set; }
    }


    
    public class TesteIntegracaoRetorno
    {
        public TesteIntegracaoRetorno()
        {
            Body = "Não foi feita a chamada. Verifique se os parâmetros foram alterados na URL, Header e Layout.";
            StatusCode = 0;
        }

        public string Body { get; set; }
        public int StatusCode { get; set; }
        public double Tempo { get; set; }
    }
}

