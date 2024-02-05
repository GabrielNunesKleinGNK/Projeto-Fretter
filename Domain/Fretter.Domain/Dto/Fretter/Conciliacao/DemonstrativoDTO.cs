using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fretter.Domain.Dto.Fatura
{
    public class DemonstrativoDTO
    {
        public DemonstrativoDTO() { }
        public DemonstrativoDTO(DemonstrativoRetorno conciliacao)
        {
            if (conciliacao != null)
            {
                Notafiscal = conciliacao.Notafiscal;
                SerieNotaFiscal = conciliacao.SerieNotaFiscal;
                Pedido = conciliacao.Pedido;
                ChaveNFE = conciliacao.ChaveNFE;
                NomeRegiao = conciliacao.NomeRegiao;
                UF = conciliacao.UF;
                TipoRegiao = conciliacao.TipoRegiao;
                CEP = conciliacao.Cep;
                Prazo = conciliacao.Prazo;
                DataImportacao = conciliacao.DataImportacao;
                UltimaOcorrencia = conciliacao.UltimaOcorrencia;
                DataOcorrencia = conciliacao.DataOcorrencia;
                CTE_Chave = conciliacao.CTE_Chave;
                CTE_Numero = conciliacao.CTE_Numero;
                CTE_Serie = conciliacao.CTE_Serie;
                PesoBruto = conciliacao.PesoBruto?.ToString("0.0000");
                PesoCubado = conciliacao.PesoCubico?.ToString("0.0000");

                AtualizarComposicao(conciliacao);

                AtualizarCteCarga(conciliacao);

                AtualizarCteComposicao(conciliacao);

                AtualizarCamposDiferenca();
            }
        }


        #region Propriedades
        public string Notafiscal { get; set; }
        public string SerieNotaFiscal { get; set; }
        public string Pedido { get; set; }
        public string ChaveNFE { get; set; }
        public string NomeRegiao { get; set; }
        public string UF { get; set; }
        public string TipoRegiao { get; set; }
        public string CEP { get; set; }
        public int Prazo { get; set; }
        public DateTime DataImportacao { get; set; }
        public string UltimaOcorrencia { get; set; }
        public DateTime DataOcorrencia { get; set; }
        
        public string CTE_Chave { get; set; }
        public string CTE_Numero { get; set; }
        public string CTE_Serie { get; set; }
        
        public string PesoBruto { get; set; }
        public string CTE_PesoBruto { get; set; }
        
        
        public string PesoCubado { get; set; }
        public string CTE_PesoCubado { get; set; }

        public string ValorNF { get; set; }
        public string CTE_ValorNF { get; set; }
        
        public string FretePeso { get; set; }
        public string CTE_FretePeso { get; set; }
        
        public string PedagioValor { get; set; }
        public string CTE_PedagioValor { get; set; }
        
        public string GRIS { get; set; }
        public string CTE_GRIS { get; set; }
        
        public string AdValorem { get; set; }
        public string CTE_AdValorem { get; set; }

        public string Balsa { get; set; }
        public string CTE_Balsa { get; set; }
        
        public string RedespachoFluvial { get; set; }
        public string CTE_RedespachoFluvial { get; set; }
        
        public string FreteAdicional { get; set; }
        public string CTE_FreteAdicional { get; set; }

        public string SUFRAMA { get; set; }
        public string CTE_SUFRAMA { get; set; }
        
        public string TRT { get; set; }
        public string CTE_TRT { get; set; }
        
        public string TDE { get; set; }
        public string CTE_TDE { get; set; }
        
        public string TDA { get; set; }
        public string CTE_TDA { get; set; }
        
        public string Valor_CTE { get; set; }
        public string CTE_Valor { get; set; }
        
        public string TaxaRisco { get; set; }
        public string CTE_TaxaRisco { get; set; }
        
        public string ICMS { get; set; }
        public string CTE_ICMS { get; set; }
        
        public string TotalCobranca { get; set; }
        public string CTE_TotalCobranca { get; set; }
        
        public string TotalCobrancaImposto { get; set; }
        public string CTE_TotalCobrancaImposto { get; set; }

        public string Diferenca_PesoBruto { get; set; }
        public string Diferenca_PesoCubado { get; set; }
        public string Diferenca_ValorNF { get; set; }
        public string Diferenca_FretePeso { get; set; }
        public string Diferenca_PedagioValor { get; set; }
        public string Diferenca_GRIS { get; set; }
        public string Diferenca_AdValorem { get; set; }
        public string Diferenca_Balsa { get; set; }
        public string Diferenca_RedespachoFluvial { get; set; }
        public string Diferenca_FreteAdicional { get; set; }
        public string Diferenca_SUFRAMA { get; set; }
        public string Diferenca_TRT { get; set; }
        public string Diferenca_TDE { get; set; }
        public string Diferenca_TDA { get; set; }
        public string Diferenca_CTE_Valor { get; set; }
        public string Diferenca_TaxaRisco { get; set; }
        public string Diferenca_ICMS { get; set; }
        public string Diferenca_TotalCobranca { get; set; }
        public string Diferenca_TotalCobrancaImposto { get; set; }

        #endregion

        #region Métodos

        private static string GetValor(List<ItemConfiguracao> itens, EnumConfiguracaoCteTipo configuracaoCteTipo)
        {
            return itens?.FirstOrDefault(f => f.Chave == configuracaoCteTipo.GetDisplayValue())?.Valor.ToString("0.0000") ?? string.Empty;
        }

        private static string Compare(string item1, string item2)
        {
            var item1HasValue =decimal.TryParse(item1, out decimal item1Value);
            var item2HasValue = decimal.TryParse(item2, out decimal item2Value);

            if (!item1HasValue && !item2HasValue)
                return string.Empty;
            if (item1HasValue && !item2HasValue)
                return item1Value.ToString("0.0000");
            if (!item1HasValue && item2HasValue)
                return item2Value.ToString("0.0000");

            return (item1Value > item2Value ? item1Value - item2Value : item2Value - item1Value).ToString("0.0000");
        }

        private void AtualizarCamposDiferenca()
        {
            Diferenca_PesoBruto = Compare(PesoBruto, CTE_PesoBruto);
            Diferenca_PesoCubado = Compare(PesoCubado, CTE_PesoCubado);
            Diferenca_AdValorem = Compare(AdValorem, CTE_AdValorem);
            Diferenca_ICMS = Compare(ICMS, CTE_ICMS);
            Diferenca_GRIS = Compare(GRIS, CTE_GRIS);
            Diferenca_PedagioValor = Compare(PedagioValor, CTE_PedagioValor);
            Diferenca_TDA = Compare(TDA, CTE_TDA);
            Diferenca_TDE = Compare(TDE, CTE_TDE);
            Diferenca_TRT = Compare(TRT, CTE_TRT);
            Diferenca_TaxaRisco = Compare(TaxaRisco, CTE_TaxaRisco);
            Diferenca_SUFRAMA = Compare(SUFRAMA, CTE_SUFRAMA);
        }

        private void AtualizarComposicao(DemonstrativoRetorno conciliacao)
        {
            if (!string.IsNullOrEmpty(conciliacao.JsonComposicaoValoresCotacao))
            {
                var listComposicaoCte = JsonConvert.DeserializeObject<List<ItemConfiguracao>>(conciliacao.JsonComposicaoValoresCotacao);

                AdValorem = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Advalorem);
                ICMS = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Icms);
                GRIS = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Gris);
                PedagioValor = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Pedagio);
                TDA = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.TaxaTDA);
                TDE = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.TaxaTDE);
                TRT = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.TaxaTRT);
                TaxaRisco = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.TaxaRisco);
                SUFRAMA = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Suframa);
            }
        }

        private void AtualizarCteComposicao(DemonstrativoRetorno conciliacao)
        {
            if (!string.IsNullOrEmpty(conciliacao.CTE_JsonComposicao))
            {
                var listComposicaoCte = JsonConvert.DeserializeObject<List<ItemConfiguracao>>(conciliacao.CTE_JsonComposicao);

                CTE_AdValorem = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Advalorem);
                CTE_ICMS = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Icms);
                CTE_GRIS = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Gris);
                CTE_PedagioValor = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Pedagio);
                CTE_TDA = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.TaxaTDA);
                CTE_TDE = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.TaxaTDE);
                CTE_TRT = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.TaxaTRT);
                CTE_TaxaRisco = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.TaxaRisco);
                CTE_SUFRAMA = GetValor(listComposicaoCte, EnumConfiguracaoCteTipo.Suframa);
            }
        }

        private void AtualizarCteCarga(DemonstrativoRetorno conciliacao)
        {
            if (!string.IsNullOrEmpty(conciliacao.CTE_JsonCarga))
            {
                var listCargaCte = JsonConvert.DeserializeObject<List<ItemConfiguracao>>(conciliacao.CTE_JsonCarga);

                CTE_PesoBruto = GetValor(listCargaCte, EnumConfiguracaoCteTipo.PesoConsiderado);
                CTE_PesoCubado = GetValor(listCargaCte, EnumConfiguracaoCteTipo.Peso);
            }
        }

        internal static string CteColor="#F4B183";
        internal static string DiferencaColor= "#8FAADC";
        internal static string PadraoColor= "#BFBFBF";
        public static Dictionary<int, string> GetCustomColor()
        {
            var customColor = new Dictionary<int, string>();
            var i = 0;
            foreach (PropertyInfo prop in typeof(DemonstrativoDTO).GetType().GetProperties())
            {
                if (prop.Name.StartsWith("CTE"))
                    customColor.Add(i++, CteColor);
                else if (prop.Name.StartsWith("Diferenca"))
                    customColor.Add(i++, DiferencaColor);
                else
                    customColor.Add(i++, PadraoColor);
            }

            return customColor;
        }
        #endregion
    }
}
