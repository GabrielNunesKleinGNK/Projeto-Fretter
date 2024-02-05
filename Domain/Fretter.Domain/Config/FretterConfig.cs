using System;
namespace Fretter.Domain.Config
{
    public class FretterConfig
    {
        public FretterConfig()
        {
        }

        public int OrigemImportacaoId { get; set; }
        public string CanalVendaNome { get; set; }
        public int TipoInterfaceId { get; set; }
        public string TokenNome { get; set; }
        public int CorreioTabelaId { get; set; }
        public int CorreioTransportadorId { get; set; }
        public int EmpresaQuantidadeTabela { get; set; }
        public int EmpresaImportacaoReferenciaId { get; set; } //Fixo Kabum???
    }
}

