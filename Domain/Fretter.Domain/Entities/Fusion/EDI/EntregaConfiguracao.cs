using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EntregaConfiguracao : EntityBase
    {
        #region "Construtores"
        public EntregaConfiguracao(int Id, int EntregaConfiguracaoTipo, int EmpresaId, string Caminho, string Verbo, string Layout, string LayoutHeader, string ApiKey, string URLStageCallBack, string URLEtiquetaCallBack, bool? Lote, int? Registro, int? Paralelo, bool? ProcessamentoSucesso, DateTime? DataProcessamento, DateTime? DataProximaExecucao, int? IntervaloExecucao, int IntervaloExecucaoTipo)
        {
            this.Ativar();
            this.Id = Id;
            this.EntregaConfiguracaoTipo = EntregaConfiguracaoTipo;
            this.EmpresaId = EmpresaId;
            this.Caminho = Caminho;
            this.Verbo = Verbo;
            this.Layout = Layout;
            this.LayoutHeader = LayoutHeader;
            this.ApiKey = ApiKey;
            this.URLStageCallBack = URLStageCallBack;
            this.URLEtiquetaCallBack = URLEtiquetaCallBack;
            this.Lote = Lote;
            this.Registro = Registro;
            this.Paralelo = Paralelo;
            this.ProcessamentoSucesso = ProcessamentoSucesso;
            this.DataProcessamento = DataProcessamento;
            this.DataProximaExecucao = DataProximaExecucao;
            this.IntervaloExecucao = IntervaloExecucao;
            this.Ativo = Ativo;
            this.IntervaloExecucaoTipo = IntervaloExecucaoTipo;
        }
        #endregion

        #region "Propriedades"
        public int EntregaConfiguracaoTipo { get; protected set; }
        public int EmpresaId { get; protected set; }
        public string Caminho { get; protected set; }
        public string Verbo { get; protected set; }
        public string Layout { get; protected set; }
        public string LayoutHeader { get; protected set; }
        public string ApiKey { get; protected set; }
        public string URLStageCallBack { get; protected set; }
        public string URLEtiquetaCallBack { get; protected set; }
        public bool? Lote { get; protected set; }
        public int? Registro { get; protected set; }
        public int? Paralelo { get; protected set; }
        public bool? ProcessamentoSucesso { get; protected set; }
        public DateTime? DataProcessamento { get; protected set; }
        public int? IntervaloExecucao { get; protected set; }
        public DateTime? DataProximaExecucao { get; protected set; }
        public int IntervaloExecucaoTipo { get; protected set; }
        #endregion

        #region "Referencias"
        public virtual List<EntregaConfiguracaoHistorico> Historicos { get; set; }
        public virtual List<EntregaConfiguracaoItem> Itens { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarDataProximaExecucao(DateTime? data) => this.DataProximaExecucao = data;
        public void AtualizarEntregaConfiguracaoTipo(int EntregaConfiguracaoTipo) => this.EntregaConfiguracaoTipo = EntregaConfiguracaoTipo;
        public void AtualizarEmpresa(int Empresa) => this.EmpresaId = Empresa;
        public void AtualizarCaminho(string Caminho) => this.Caminho = Caminho;
        public void AtualizarVerbo(string Verbo) => this.Verbo = Verbo;
        public void AtualizarLayout(string Layout) => this.Layout = Layout;
        public void AtualizarLayoutHeader(string LayoutHeader) => this.LayoutHeader = LayoutHeader;
        public void AtualizarApiKey(string ApiKey) => this.ApiKey = ApiKey;
        public void AtualizarURLStageCallBack(string URLStageCallBack) => this.URLStageCallBack = URLStageCallBack;
        public void AtualizarURLEtiquetaCallBack(string URLEtiquetaCallBack) => this.URLEtiquetaCallBack = URLEtiquetaCallBack;
        public void AtualizarLote(bool? Lote) => this.Lote = Lote;
        public void AtualizarRegistro(int? Registro) => this.Registro = Registro;
        public void AtualizarParalelo(int? Paralelo) => this.Paralelo = Paralelo;
        public void AtualizarProcessamentoSucesso(bool? ProcessamentoSucesso) => this.ProcessamentoSucesso = ProcessamentoSucesso;
        public void AtualizarProcessamento(DateTime? Processamento) => this.DataProcessamento = Processamento;
        public void AtualizarIntervaloExecucaoTipo(int IntervaloExecucaoTipo) => this.IntervaloExecucaoTipo = IntervaloExecucaoTipo;

        internal void AdicionarHistorico(EntregaConfiguracaoHistorico historico)
        {
            if (this.Historicos == null)
                this.Historicos = new List<EntregaConfiguracaoHistorico>();
            this.Historicos.Add(historico);
        }
        #endregion
    }
}
