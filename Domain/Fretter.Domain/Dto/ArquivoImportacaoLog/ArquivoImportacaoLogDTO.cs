using Fretter.Domain.Entities;
using Fretter.Domain.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.ArquivoImportacaoLog
{
    public class ArquivoImportacaoLogDTO
    {
        public ArquivoImportacaoLogDTO(ArquivoImportacao arquivoImportacao)
        {
            DtImportacao = arquivoImportacao.DtImportacao;
            DsNome = arquivoImportacao.DsNome;
            ObjectJson = arquivoImportacao.DsArquivo.RetornaContent(arquivoImportacao.FlComprimido);
            DtImportacaoDATE = arquivoImportacao.DtImportacaoDATE;
            IsBatch = DsNome.Contains("lote");
            NumeroPedido = GetNumeroPedido(arquivoImportacao.DsNome, ObjectJson);
        }

        public DateTime DtImportacao { get; set; }
        public string DsNome { get; set; }
        public string NumeroPedido { get; set; }
        public string ObjectJson { get; set; }
        public bool IsBatch { get; set; }
        public DateTime? DtImportacaoDATE { get; set; }


        private string GetNumeroPedido(string dsNome, string obj)
        {
            var jObj = JsonConvert.DeserializeObject<JObject>(ObjectJson);
            if (dsNome.Contains("n_lote"))
            {
                var listContent = (JArray)jObj["content"];
                var numeroPedido = "";
                for (var i=0; i< listContent.Count;i++)
                {
                    numeroPedido = numeroPedido + jObj["content"][i]["pedido"] + ";";
                }
                return numeroPedido;

            }
            return jObj["numeroPedido"].ToString();
        }
    }
}
