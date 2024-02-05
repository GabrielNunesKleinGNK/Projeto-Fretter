using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class EmpresaTransportadorConfig : EntityBase
    {
        #region "Construtores"
        public EmpresaTransportadorConfig(int Id, int EmpresaId, int TransportadorId, int PrazoComercial, Int16 TipoServico, int OcorrenciaTransportador, Int16? OrigemImportacaoId, int ArquivoOcorrenciaConfig, int? PrazoComercialRelativo, int PrazoEntrega, Int16 EmpresaTransportadorConfigTipoPrazoCliente, DateTime? Inclusao)
        {
            this.Id = Id;
            this.EmpresaId = EmpresaId;
            this.TransportadorId = TransportadorId;
            this.PrazoComercial = PrazoComercial;
            this.TipoServico = TipoServico;
            this.OcorrenciaTransportador = OcorrenciaTransportador;
            this.OrigemImportacaoId = OrigemImportacaoId;
            this.ArquivoOcorrenciaConfig = ArquivoOcorrenciaConfig;
            this.FatorCubagem = 0;
            this.PrazoComercialRelativo = PrazoComercialRelativo;
            this.PrazoEntrega = PrazoEntrega;
            this.EmpresaTransportadorConfigTipoPrazoCliente = EmpresaTransportadorConfigTipoPrazoCliente;
            this.Inclusao = Inclusao;
            this.MicroServicos = new List<MicroServico>();
        }
        #endregion

        #region "Propriedades"
        public int EmpresaId { get; protected set; }
        public int TransportadorId { get; protected set; }
        public int PrazoComercial { get; protected set; }
        public Int16 TipoServico { get; protected set; }
        public int OcorrenciaTransportador { get; protected set; }
        public int ArquivoOcorrenciaConfig { get; protected set; }
        public int? FatorCubagem { get; protected set; }
        public int? PrazoComercialRelativo { get; protected set; }
        public int PrazoEntrega { get; protected set; }
        public Int16 EmpresaTransportadorConfigTipoPrazoCliente { get; protected set; }
        public Int16? OrigemImportacaoId { get; protected set; }
        public DateTime? Inclusao { get; protected set; }
        public bool? MostraOcoComplementar { get; protected set; }
        #endregion

        #region "Referencias"
        public Empresa Empresa { get; protected set; }
        public ICollection<MicroServico> MicroServicos { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresa(int Empresa) => this.EmpresaId = Empresa;
        public void AtualizarTransportador(int Transportador) => this.TransportadorId = Transportador;
        public void AtualizarPrazoComercial(int PrazoComercial) => this.PrazoComercial = PrazoComercial;
        public void AtualizarTipoServico(Int16 TipoServico) => this.TipoServico = TipoServico;
        public void AtualizarOcorrenciaTransportador(int OcorrenciaTransportador) => this.OcorrenciaTransportador = OcorrenciaTransportador;
        public void AtualizarArquivoOcorrenciaConfig(int ArquivoOcorrenciaConfig) => this.ArquivoOcorrenciaConfig = ArquivoOcorrenciaConfig;
        public void AtualizarFatorCubagem(int? FatorCubagem) => this.FatorCubagem = FatorCubagem;
        public void AtualizarPrazoComercialRelativo(int? PrazoComercialRelativo) => this.PrazoComercialRelativo = PrazoComercialRelativo;
        public void AtualizarPrazoEntrega(int PrazoEntrega) => this.PrazoEntrega = PrazoEntrega;
        public void AtualizarEmpresaTransportadorConfigTipoPrazoCliente(Int16 EmpresaTransportadorConfigTipoPrazoCliente) => this.EmpresaTransportadorConfigTipoPrazoCliente = EmpresaTransportadorConfigTipoPrazoCliente;
        public void AtualizarOrigemImportacao(Int16? OrigemImportacao) => this.OrigemImportacaoId = OrigemImportacao;
        public void AtualizarInclusao(DateTime? Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarMostraOcoComplementar(bool? MostraOcoComplementar) => this.MostraOcoComplementar = MostraOcoComplementar;
        #endregion
    }
}
