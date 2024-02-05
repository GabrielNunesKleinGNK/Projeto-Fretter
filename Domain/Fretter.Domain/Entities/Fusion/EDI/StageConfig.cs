using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities
{
    public class StageConfig : EntityBase
    {
        public StageConfig() { }

        public int Cd_Id { get; set; }
        public DateTime Dt_Inclusao { get; set; }
        public int Id_MicroServico { get; set; }
        public int? Id_Servico { get; set; }
        public string Nr_Contrato { get; set; }
        public string Nr_Diretoria { get; set; }
        public int Cd_Administrativo { get; set; }
        public string Cd_Cnpj { get; set; }
        public string Cd_Servico { get; set; }
        public string Ds_Login { get; set; }
        public string Ds_Senha { get; set; }
        public string Ds_CartaoPostagem { get; set; }
        public string Ds_LogoCliente { get; set; }
        public string Cd_ServicoAdicional { get; set; }
        public string Ds_SiglaEtiqueta { get; set; }
        public string Cd_ServicoEtiqueta { get; set; }
        public int Id_StageConfigEtiquetaTipo { get; set; }
        public string Ds_Endereco { get; set; }
        public int? Id_ExpedicaoEnvioCorreiosTipo { get; set; }
        public bool? Flg_GerarSro { get; set; }
        public int? Nr_QtdeEtiquetaEstoque { get; set; }
        public bool? Flg_UsarCnpjDanfe { get; set; }
        public string Ds_UrlCotacaoFrete { get; set; }
        public string Ds_CaminhoCotacaoFrete { get; set; }
        public string Ds_TokenCotacaoFrete { get; set; }
        public string Ds_CanalVendaCotacaoFrete { get; set; }
    }
}
