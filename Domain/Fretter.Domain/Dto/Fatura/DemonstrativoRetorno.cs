using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fretter.Domain.Dto.Fretter.Conciliacao
{
    public class DemonstrativoRetorno
    {
        public int FaturaId { get; set; }
        public string Notafiscal { get; set; }
        public string SerieNotaFiscal { get; set; }
        public string Pedido { get; set; }
        public string ChaveNFE { get; set; }
        public string NomeRegiao { get; set; }
        public string UF { get; set; }
        public string TipoRegiao { get; set; }
        public string Cep { get; set; }
        public int Prazo { get; set; }
        public DateTime DataImportacao { get; set; }
        public string UltimaOcorrencia { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public decimal? PesoBruto { get; set; }
        public decimal? PesoCubico { get; set; }
        public decimal? ValorDeclarado { get; set; }
        public decimal? FreteCusto { get; set; }
        public string JsonComposicaoValoresCotacao { get; set; }
        public int ImportacaoCteId { get; set; }
        public string CTE_Numero { get; set; }
        public string CTE_Serie { get; set; }
        public string CTE_Chave { get; set; }
        public string CTE_JsonComposicao { get; set; }
        public string CTE_JsonCarga { get; set; }

    }
}
