using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.LogisticaReversa
{
    public class DevolucaoCorreioCancela
    {
        public long Id_EntregaStage { get; set; }
        public long Id_Entrega { get; set; }
        public int Id_EntregaDevolucao { get; set; }
        public int Id_EntregaDevolucaoStatus { get; set; }
        public string Cd_CodigoContrato { get; set; }
        public string Cd_CodigoIntegracao { get; set; }
        public string Cd_CodigoCartao { get; set; }
        public string Cd_CodigoServico { get; set; }
        public string Ds_ServicoUsuario { get; set; }
        public string Ds_ServicoSenha { get; set; }
        public string Ds_ServicoURL { get; set; }
        public string Ds_SolicitacaoTipo { get; set; }
        public string Cd_CodigoColeta { get; set; }
        public string Cd_CodigoRastreio { get; set; }
    }
}
