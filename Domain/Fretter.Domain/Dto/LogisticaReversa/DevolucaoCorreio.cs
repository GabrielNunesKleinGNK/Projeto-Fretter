using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.LogisticaReversa
{
    public class DevolucaoCorreio
    {
        public long Id_EntregaStage { get; set; }
        public long Id_Entrega { get; set; }
        public int Id_Empresa { get; set; }
        public int Id_EntregaDevolucao { get; set; }
        public int Id_EntregaDevolucaoStatus { get; set; }
        public string Cd_Pedido { get; set; }
        public string Cd_CodigoContrato { get; set; }
        public string Cd_CodigoIntegracao { get; set; }
        public string Cd_CodigoCartao { get; set; }
        public string Cd_CodigoServico { get; set; }
        public string Ds_ServicoUsuario { get; set; }
        public string Ds_ServicoSenha { get; set; }
        public string Ds_ServicoURL { get; set; }
        public string Ds_RemetenteNome { get; set; }
        public string Ds_RemetenteIdentificacao { get; set; }
        public string Ds_RemetenteLogradouro { get; set; }
        public string Ds_RemetenteNumero { get; set; }
        public string Ds_RemetenteComplemento { get; set; }
        public string Ds_RemetenteBairro { get; set; }
        public string Ds_RemetenteCidade { get; set; }
        public string Ds_RemetenteUF { get; set; }
        public string Ds_RemetenteCEP { get; set; }
        public string Ds_RemetenteEmail { get; set; }
        public string Ds_RemetenteDDD { get; set; }
        public string Ds_RemetenteTelefone { get; set; }
        public string Ds_DestinatarioNome { get; set; }
        public string Ds_DestinatarioLogradouro { get; set; }
        public string Ds_DestinatarioNumero { get; set; }
        public string Ds_DestinatarioComplemento { get; set; }
        public string Ds_DestinatarioBairro { get; set; }
        public string Ds_DestinatarioCidade { get; set; }
        public string Ds_DestinatarioUF { get; set; }
        public string Ds_DestinatarioCEP { get; set; }
        public string Ds_DestinatarioEmail { get; set; }
        public string Ds_DestinatarioDDD { get; set; }
        public string Ds_DestinatarioTelefone { get; set; }
        public string Ds_SolicitacaoTipo { get; set; }
        public bool Ds_SolicitacaoAR { get; set; }
        public string Ds_SolicitacaoAG { get; set; }
        public string Ds_SolicitacaoNumero { get; set; }
        public string Ds_SolicitacaoIdCliente { get; set; }
        public string Ds_SolicitacaoItemQuantidade { get; set; }
        public string Ds_SolicitacaoItemDescricao { get; set; }
        public string Ds_SolicitacaoItemEntregaId { get; set; }
        public string Ds_SolicitacaoItemEntrega { get; set; }
        public string Ds_SolicitacaoItemEntregaNumero { get; set; }
        public bool Ds_SolicitacaoProdutoPossui { get; set; }
        public string Ds_SolicitacaoProdutoCodigo { get; set; }
        public string Ds_SolicitacaoProdutoTipo { get; set; }
        public string Ds_SolicitacaoProdutoQtd { get; set; }
    }
}
