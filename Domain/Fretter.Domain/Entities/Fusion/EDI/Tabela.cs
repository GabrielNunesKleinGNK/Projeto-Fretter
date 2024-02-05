using System;

namespace Fretter.Domain.Entities
{
    public class Tabela : EntityBase
    {
        #region "Construtores"
        public Tabela(int Id, int Empresa, int Transportador, byte TipoServico, DateTime? VigenciaInicio, DateTime? VigenciaFim, DateTime Inclusao, string Nome, int? FatorCubagem, bool? RegiaoGenerica, int? PesoMinimo, byte TabelaStatus, int? TabelaCopiaDe, bool UtilizaICMS, bool? GrisPorNF, decimal? AdValoremMinimo, bool? CalculaICMS, string ParametrosJson, int? Timeout, bool EndPoint, int MicroServicoId, decimal? LimitePesoCubico, decimal? MedidaLimite, decimal? MedidaLimiteSoma, decimal? PesoCubadoMaximo, decimal? PesoRealMaximo, decimal? GrisMinimo, decimal? FatorCubagemLimiteMinimo, int? Hub, int? MicroServicoContingencia, bool? OrigemGenerica)
        {
            this.Ativar();
            this.Id = Id;
            this.Empresa = Empresa;
            this.Transportador = Transportador;
            this.TipoServico = TipoServico;
            this.VigenciaInicio = VigenciaInicio;
            this.VigenciaFim = VigenciaFim;
            this.Inclusao = Inclusao;
            this.Nome = Nome;
            this.FatorCubagem = FatorCubagem;
            this.RegiaoGenerica = RegiaoGenerica;
            this.PesoMinimo = PesoMinimo;
            this.TabelaStatus = TabelaStatus;
            this.TabelaCopiaDe = TabelaCopiaDe;
            this.UtilizaICMS = UtilizaICMS;
            this.GrisPorNF = GrisPorNF;
            this.AdValoremMinimo = AdValoremMinimo;
            this.CalculaICMS = CalculaICMS;
            this.ParametrosJson = ParametrosJson;
            this.Timeout = Timeout;
            this.EndPoint = EndPoint;
            this.MicroServicoId = MicroServicoId;
            this.LimitePesoCubico = LimitePesoCubico;
            this.MedidaLimite = MedidaLimite;
            this.MedidaLimiteSoma = MedidaLimiteSoma;
            this.PesoCubadoMaximo = PesoCubadoMaximo;
            this.PesoRealMaximo = PesoRealMaximo;
            this.GrisMinimo = GrisMinimo;
            this.FatorCubagemLimiteMinimo = FatorCubagemLimiteMinimo;
            this.Hub = Hub;
            this.MicroServicoContingencia = MicroServicoContingencia;
            this.OrigemGenerica = OrigemGenerica;
        }
        #endregion

        #region "Propriedades"
        public int Empresa { get; protected set; }
        public int Transportador { get; protected set; }
        public byte TipoServico { get; protected set; }
        public DateTime? VigenciaInicio { get; protected set; }
        public DateTime? VigenciaFim { get; protected set; }
        public DateTime Inclusao { get; protected set; }
        public string Nome { get; protected set; }
        public int? FatorCubagem { get; protected set; }
        public bool? RegiaoGenerica { get; protected set; }
        public int? PesoMinimo { get; protected set; }
        public byte TabelaStatus { get; protected set; }
        public int? TabelaCopiaDe { get; protected set; }
        public bool UtilizaICMS { get; protected set; }
        public bool? GrisPorNF { get; protected set; }
        public decimal? AdValoremMinimo { get; protected set; }
        public bool? CalculaICMS { get; protected set; }
        public string ParametrosJson { get; protected set; }
        public int? Timeout { get; protected set; }
        public bool EndPoint { get; protected set; }
        public int MicroServicoId { get; protected set; }
        public decimal? LimitePesoCubico { get; protected set; }
        public decimal? MedidaLimite { get; protected set; }
        public decimal? MedidaLimiteSoma { get; protected set; }
        public decimal? PesoCubadoMaximo { get; protected set; }
        public decimal? PesoRealMaximo { get; protected set; }
        public decimal? GrisMinimo { get; protected set; }
        public decimal? FatorCubagemLimiteMinimo { get; protected set; }
        public int? Hub { get; protected set; }
        public int? MicroServicoContingencia { get; protected set; }
        public bool? OrigemGenerica { get; protected set; }
        #endregion

        #region "Referencias"
        public EmpresaTransportadorConfig EmpresaTransportadorConfig { get; protected set; }
        public MicroServico MicroServico { get; protected set; }
        #endregion

        #region "Métodos"
        public void AtualizarEmpresa(int Empresa) => this.Empresa = Empresa;
        public void AtualizarTransportador(int Transportador) => this.Transportador = Transportador;
        public void AtualizarTipoServico(byte TipoServico) => this.TipoServico = TipoServico;
        public void AtualizarVigenciaInicio(DateTime? VigenciaInicio) => this.VigenciaInicio = VigenciaInicio;
        public void AtualizarVigenciaFim(DateTime? VigenciaFim) => this.VigenciaFim = VigenciaFim;
        public void AtualizarInclusao(DateTime Inclusao) => this.Inclusao = Inclusao;
        public void AtualizarNome(string Nome) => this.Nome = Nome;
        public void AtualizarFatorCubagem(int? FatorCubagem) => this.FatorCubagem = FatorCubagem;
        public void AtualizarRegiaoGenerica(bool? RegiaoGenerica) => this.RegiaoGenerica = RegiaoGenerica;
        public void AtualizarPesoMinimo(int? PesoMinimo) => this.PesoMinimo = PesoMinimo;
        public void AtualizarTabelaStatus(byte TabelaStatus) => this.TabelaStatus = TabelaStatus;
        public void AtualizarTabelaCopiaDe(int? TabelaCopiaDe) => this.TabelaCopiaDe = TabelaCopiaDe;
        public void AtualizarUtilizaICMS(bool UtilizaICMS) => this.UtilizaICMS = UtilizaICMS;
        public void AtualizarGrisPorNF(bool? GrisPorNF) => this.GrisPorNF = GrisPorNF;
        public void AtualizarAdValoremMinimo(decimal? AdValoremMinimo) => this.AdValoremMinimo = AdValoremMinimo;
        public void AtualizarCalculaICMS(bool? CalculaICMS) => this.CalculaICMS = CalculaICMS;
        public void AtualizarParametrosJson(string ParametrosJson) => this.ParametrosJson = ParametrosJson;
        public void AtualizarTimeout(int? Timeout) => this.Timeout = Timeout;
        public void AtualizarEndPoint(bool EndPoint) => this.EndPoint = EndPoint;
        public void AtualizarMicroServico(int MicroServico) => this.MicroServicoId = MicroServico;
        public void AtualizarLimitePesoCubico(decimal? LimitePesoCubico) => this.LimitePesoCubico = LimitePesoCubico;
        public void AtualizarMedidaLimite(decimal? MedidaLimite) => this.MedidaLimite = MedidaLimite;
        public void AtualizarMedidaLimiteSoma(decimal? MedidaLimiteSoma) => this.MedidaLimiteSoma = MedidaLimiteSoma;
        public void AtualizarPesoCubadoMaximo(decimal? PesoCubadoMaximo) => this.PesoCubadoMaximo = PesoCubadoMaximo;
        public void AtualizarPesoRealMaximo(decimal? PesoRealMaximo) => this.PesoRealMaximo = PesoRealMaximo;
        public void AtualizarGrisMinimo(decimal? GrisMinimo) => this.GrisMinimo = GrisMinimo;
        public void AtualizarFatorCubagemLimiteMinimo(decimal? FatorCubagemLimiteMinimo) => this.FatorCubagemLimiteMinimo = FatorCubagemLimiteMinimo;
        public void AtualizarHub(int? Hub) => this.Hub = Hub;
        public void AtualizarMicroServicoContingencia(int? MicroServicoContingencia) => this.MicroServicoContingencia = MicroServicoContingencia;
        public void AtualizarOrigemGenerica(bool? OrigemGenerica) => this.OrigemGenerica = OrigemGenerica;
        #endregion
    }
}
