using Fretter.Domain.Helpers.Attributes;
using Newtonsoft.Json;
using System;

namespace Fretter.Domain.Dto.RecotacaoFrete
{
    public class MenuFreteCotacao
    {
        public MenuFreteCotacao()
        {
            Dt_Inclusao = DateTime.Now;
        }
        public MenuFreteCotacao(int idRecotacaoFrete, int linha, string token)
        {
            Id_RecotacaoFrete = idRecotacaoFrete;
            Linha = linha;
            Ds_Token = token;
            Dt_Inclusao = DateTime.Now;
        }
        public int Id_RecotacaoFrete { get; set; }
        public string Ds_Cliente { get; set; }
        public string Ds_Token { get; set; }
        public int? Cd_EntregaId { get; set; }
        public int? Cd_EntregaUnificadaId { get; set; }
        public string Cd_CodigoPedido { get; set; }
        public int? Cd_SellerId { get;  set; }
        public string Ds_Canal { get; set; }
        public string Ds_Modalidade { get; set; }
        public string Ds_ModalidadeRetorno { get; set; }
        public string Ds_Interface { get; set; }
        public string Ds_URL { get; set; }
        public string Ds_Formato { get; set; }
        [JsonIgnore]
        [DataTableColumnIgnore]
        private string _ds_CepOrigem { get; set; }
        public string Ds_CepOrigem { get { return (Convert.ToInt32(_ds_CepOrigem)).ToString().PadLeft(8, '0'); } set { _ds_CepOrigem = value; } }
        [JsonIgnore]
        [DataTableColumnIgnore]
        private string _ds_CepDestino { get; set; }
        public string Ds_CepDestino { get { return (Convert.ToInt32(_ds_CepDestino)).ToString().PadLeft(8, '0'); } set { _ds_CepDestino = value; } }
        public string Ds_SKU { get; set; }
        public string Ds_SRO { get; set; }
        public int Nr_Quantidade { get; set; }
        public string Ds_Mensagem { get; set; }
        public string Ds_Transportador { get; set; }
        public string Ds_Grupo { get; set; }
        public string Ds_Perfil { get; set; }
        public string Ds_Cep { get; set; }
        public string Ds_UF { get; set; }
        public string Ds_Regiao { get; set; }
        public string Ds_Protocolo { get; set; }

        public decimal Nr_Altura { get; set; }
        public decimal Nr_Largura { get; set; }
        public decimal Nr_Comprimento { get; set; }
        public decimal Nr_Peso { get; set; }
        public decimal Nr_Valor { get; set; }
        public decimal Nr_Tempo { get; set; }
        public decimal Nr_Prazo { get; set; }
        public decimal Nr_PrazoExpedicao { get; set; }
        public decimal Nr_PrazoTransportador { get; set; }

        public decimal Nr_ValorCusto { get; set; }
        public decimal Nr_ValorReceita { get; set; }
        
        public decimal Nr_Margem { get; set; }
        
        public bool Flg_Agrupado { get; set; }

        public DateTime? Dt_DataPedido { get; set; }
        public DateTime Dt_Inclusao { get; set; }


        [JsonIgnore]
        [DataTableColumnIgnore]
        public int? Id_MicroServico { get; set; }
        [JsonIgnore]
        [DataTableColumnIgnore]
        public string Ds_MicroServico { get; set; }

        [JsonIgnore]
        [DataTableColumnIgnore]
        public string Ds_PriorizaPrazo { get; set; }

        [JsonIgnore]
        [DataTableColumnIgnore]
        public int Linha { get; set; }

    }
}