using Fretter.Domain.Enum;

namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class TabelaArquivoProcessamento
    {
        public int Id { get; set; }
        public int IdTabela { get; set; }
        public int IdTransportador { get; set; }
        public int IdEmpresa { get; set; }
        public EnumTabelaArquivoStatus IdTabelaArquivoStatus { get; set; }
        public string DsNomeArquivo { get; set; }
        public string DsUrl { get; set; }
        public string DsUrlParametroCep { get; set; }
        public string DsModelo { get;  set; }
    }
}
