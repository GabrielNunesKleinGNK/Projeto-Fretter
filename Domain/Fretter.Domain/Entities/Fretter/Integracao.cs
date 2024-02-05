using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class Integracao : EntityBase
    {
        #region "Construtores"
        public Integracao(int Id, int IntegracaoTipoId, int EmpresaIntegracaoId, string URL, string Verbo, bool? Lote, string LayoutHeader, string Layout,
                          DateTime? DataProcessamento, bool? ProcessamentoSucesso, int Registros, int Paralelo, bool? Producao, bool? EnvioBody,
                          bool? EnviaOcorrenciaHistorico, int? EnvioConfigId, bool? IntegracaoGatilho, bool Ativo)
        {
            this.Ativar();
            this.Id = Id;
            this.IntegracaoTipoId = IntegracaoTipoId;
            this.EmpresaIntegracaoId = EmpresaIntegracaoId;
            this.URL = URL;
            this.Verbo = Verbo;
            this.Lote = Lote;
            this.LayoutHeader = LayoutHeader;
            this.Layout = Layout;
            this.DataProcessamento = DataProcessamento;
            this.ProcessamentoSucesso = ProcessamentoSucesso;
            this.Registros = Registros;
            this.Paralelo = Paralelo;
            this.Producao = Producao;
            this.EnvioBody = EnvioBody;
            this.EnviaOcorrenciaHistorico = EnviaOcorrenciaHistorico;
            this.EnvioConfigId = EnvioConfigId;
            this.IntegracaoGatilho = IntegracaoGatilho;
            this.Ativo = Ativo;
        }
        #endregion

        #region "Propriedades"  

        public int IntegracaoTipoId { get; protected set; }
        public int EmpresaIntegracaoId { get; protected set; }
        public string URL { get; protected set; }
        public string Verbo { get; protected set; }
        public bool? Lote { get; protected set; }
        public string LayoutHeader { get; protected set; }
        public string Layout { get; protected set; }
        public DateTime? DataProcessamento { get; protected set; }
        public bool? ProcessamentoSucesso { get; protected set; }
        public int Registros { get; protected set; }
        public int Paralelo { get; protected set; }
        public bool? Producao { get; protected set; }
        public bool? EnvioBody { get; protected set; }
        public bool? EnviaOcorrenciaHistorico { get; protected set; }
        public int? EnvioConfigId { get; protected set; }
        public bool? IntegracaoGatilho { get; protected set; }

        #endregion

        #region "Referencias"

        public IntegracaoTipo IntegracaoTipo { get; set; }

        //public EmpresaIntegracao EmpresaIntegracao { get; set; }

        #endregion

        #region "Métodos"
        public void AtualizarIntegracaoTipoId(int IntegracaoTipoId) => this.IntegracaoTipoId = IntegracaoTipoId;
        public void AtualizarEmpresaIntegracaoId(int EmpresaIntegracaoId) => this.EmpresaIntegracaoId = EmpresaIntegracaoId;
        public void AtualizarURL(string URL) => this.URL = URL;
        public void AtualizarVerbo(string Verbo) => this.Verbo = Verbo;
        public void AtualizarLote(bool? Lote) => this.Lote = Lote;
        public void AtualizarLayoutHeader(string LayoutHeader) => this.LayoutHeader = LayoutHeader;
        public void AtualizarLayout(string Layout) => this.Layout = Layout;
        public void AtualizarDataProcessamento(DateTime? DataProcessamento) => this.DataProcessamento = DataProcessamento;
        public void AtualizarRegistros(int Registros) => this.Registros = Registros;
        public void AtualizarParalelo(int Paralelo) => this.Paralelo = Paralelo;
        public void AtualizarProducao(bool? Producao) => this.Producao = Producao;
        public void AtualizarEnvioBody(bool? EnvioBody) => this.EnvioBody = EnvioBody;
        public void AtualizarEnviaOcorrenciaHistorico(bool? EnviaOcorrenciaHistorico) => this.EnviaOcorrenciaHistorico = EnviaOcorrenciaHistorico;
        public void AtualizarEnvioConfigId(int? EnvioConfigId) => this.EnvioConfigId = EnvioConfigId;
        public void AtualizarIntegracaoGatilho(bool? IntegracaoGatilho) => this.IntegracaoGatilho = IntegracaoGatilho;
        #endregion
    }
}