
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Services;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Application;
using Fretter.Application;
using Fretter.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Fretter.Repository;
using Fretter.Repository.Repositories;
using Fretter.Application.ServiceApplication;
using Fretter.Domain.Interfaces.Services;
using Fretter.Domain.Interfaces.Applications;
using Fretter.Domain.Interfaces.Repositories;
using Fretter.Repository.Util;
using Fretter.Domain.Interfaces.Service.Api;
using Fretter.Domain.Services.Api;
using Fretter.Domain.Config;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Repository.Repositories.Fusion;
using Fretter.Domain.Interfaces.Service.Fusion;
using Fretter.Domain.Services.Fusion;
using Fretter.Domain.Helpers;
using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Serilog.Exceptions;
using Fretter.Domain.Interfaces.Service.Webhook;
using Fretter.Domain.Services.Webhook;
using Fretter.Repository.Repositories.Webhook;
using Fretter.Domain.Interfaces.Repository.Webhook;
using Fretter.Domain.Interfaces.Service.Util;
using Fretter.Domain.Services.Util;

namespace Fretter.IoC
{
    public static class FretterCoreIoc
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<FretterContext>(options => options.UseSqlServer(configuration.GetConnectionString("FretterConnection")));
            services.AddScoped<IUnitOfWork<FretterContext>, FretterContext>();

            services.Configure<BlobStorageConfig>(configuration.GetSection("BlobStorageConfig"));
            services.Configure<TimeoutConfig>(configuration.GetSection("TimeoutConfig"));
            services.Configure<RecotacaoFreteConfig>(configuration.GetSection("RecotacaoFreteConfig"));
            services.Configure<FretterConfig>(configuration.GetSection("FretterConfig"));
            services.Configure<MessageBusConfig>(configuration.GetSection("MessageBusConfig"));
            services.Configure<MessageBusShipNConfig>(configuration.GetSection("MessageBusShipNConfig"));
            services.Configure<PedidoIntegracaoConfig>(configuration.GetSection("PedidoIntegracaoConfig"));
            services.Configure<ImportacaoEntregaConfig>(configuration.GetSection("ImportacaoEntregaConfig"));
            services.Configure<ElasticSearchConfig>(configuration.GetSection("ElasticSearchConfig"));
            services.Configure<MiraklConfig>(configuration.GetSection("MiraklConfig"));
            services.Configure<ShipNConfig>(configuration.GetSection("ShipNConfig"));
            services.Configure<FusionApiSkuConfig>(configuration.GetSection("FusionApiSkuConfig"));

            services.AddDbContextPool<CommandContext>(options => options.UseSqlServer(configuration.GetConnectionString("FretterConnection")));
            services.AddScoped<IUnitOfWork<CommandContext>, CommandContext>();


            services.AddScoped(typeof(ITrackingService<>), typeof(TrackingService<>));
            services.AddScoped(typeof(ITrackingRepository<>), typeof(TrackingRepository<>));

            services.AddScoped(typeof(ITrackingIntegracaoService<>), typeof(TrackingIntegracaoService<>));
            services.AddScoped(typeof(ITrackingIntegracaoRepository<>), typeof(TrackingIntegracaoRepository<>));

            services.AddScoped(typeof(IBlobStorageService), typeof(BlobStorageService));
            services.AddScoped(typeof(IMessageBusService<>), typeof(MessageBusService<>));
            services.AddScoped(typeof(ICorreioService), typeof(CorreioService));

            services.AddScoped(typeof(Domain.Interfaces.Helper.ILogHelper), typeof(LogHelper));

            // Base
            services.AddScoped(typeof(IApplicationBase<,>), typeof(ApplicationBase<,>));
            services.AddScoped(typeof(IServiceBase<,>), typeof(ServiceBase<,>));
            services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

            services.AddScoped(typeof(IElasticSearchRepository), typeof(ElasticSearchRepository));

            services.AddScoped(typeof(IUsuarioService<>), typeof(UsuarioService<>));
            services.AddScoped(typeof(IUsuarioRepository<>), typeof(UsuarioRepository<>));
            services.AddScoped(typeof(IUsuarioApplication<>), typeof(UsuarioApplication<>));

            services.AddScoped(typeof(IConciliacaoTransportadorService<>), typeof(ConciliacaoTransportadorService<>));
            services.AddScoped(typeof(IConciliacaoTransportadorRepository<>), typeof(ConciliacaoTransportadorRepository<>));
            services.AddScoped(typeof(IConciliacaoTransportadorApplication<>), typeof(ConciliacaoTransportadorApplication<>));

            services.AddScoped(typeof(ISistemaMenuService<>), typeof(SistemaMenuService<>));
            services.AddScoped(typeof(ISistemaMenuRepository<>), typeof(SistemaMenuRepository<>));
            services.AddScoped(typeof(ISistemaMenuApplication<>), typeof(SistemaMenuApplication<>));
            services.AddScoped(typeof(ISistemaMenuPermissaoRepository<>), typeof(SistemaMenuPermissaoRepository<>));


            services.AddScoped(typeof(ISisMenuService<>), typeof(SisMenuService<>));
            services.AddScoped(typeof(ISisMenuApplication<>), typeof(SisMenuApplication<>));


            services.AddScoped(typeof(IConciliacaoService<>), typeof(ConciliacaoService<>));
            services.AddScoped(typeof(IConciliacaoRepository<>), typeof(ConciliacaoRepository<>));
            services.AddScoped(typeof(IConciliacaoApplication<>), typeof(ConciliacaoApplication<>));

            services.AddScoped(typeof(IConciliacaoHistoricoService<>), typeof(ConciliacaoHistoricoService<>));
            services.AddScoped(typeof(IConciliacaoHistoricoRepository<>), typeof(ConciliacaoHistoricoRepository<>));
            services.AddScoped(typeof(IConciliacaoHistoricoApplication<>), typeof(ConciliacaoHistoricoApplication<>));

            services.AddScoped(typeof(IConciliacaoMediacaoService<>), typeof(ConciliacaoMediacaoService<>));
            services.AddScoped(typeof(IConciliacaoMediacaoRepository<>), typeof(ConciliacaoMediacaoRepository<>));
            services.AddScoped(typeof(IConciliacaoMediacaoApplication<>), typeof(ConciliacaoMediacaoApplication<>));

            services.AddScoped(typeof(IConciliacaoTipoService<>), typeof(ConciliacaoTipoService<>));
            services.AddScoped(typeof(IConciliacaoTipoRepository<>), typeof(ConciliacaoTipoRepository<>));
            services.AddScoped(typeof(IConciliacaoTipoApplication<>), typeof(ConciliacaoTipoApplication<>));

            services.AddScoped(typeof(IConciliacaoStatusService<>), typeof(ConciliacaoStatusService<>));
            services.AddScoped(typeof(IConciliacaoStatusRepository<>), typeof(ConciliacaoStatusRepository<>));
            services.AddScoped(typeof(IConciliacaoStatusApplication<>), typeof(ConciliacaoStatusApplication<>));

            services.AddScoped(typeof(IContratoTransportadorService<>), typeof(ContratoTransportadorService<>));
            services.AddScoped(typeof(IContratoTransportadorRepository<>), typeof(ContratoTransportadorRepository<>));
            services.AddScoped(typeof(IContratoTransportadorApplication<>), typeof(ContratoTransportadorApplication<>));

            services.AddScoped(typeof(IContratoTransportadorHistoricoService<>), typeof(ContratoTransportadorHistoricoService<>));
            services.AddScoped(typeof(IContratoTransportadorHistoricoRepository<>), typeof(ContratoTransportadorHistoricoRepository<>));
            services.AddScoped(typeof(IContratoTransportadorHistoricoApplication<>), typeof(ContratoTransportadorHistoricoApplication<>));

            services.AddScoped(typeof(IImportacaoArquivoService<>), typeof(ImportacaoArquivoService<>));
            services.AddScoped(typeof(IImportacaoArquivoRepository<>), typeof(ImportacaoArquivoRepository<>));
            services.AddScoped(typeof(IImportacaoArquivoApplication<>), typeof(ImportacaoArquivoApplication<>));

            services.AddScoped(typeof(IImportacaoArquivoCategoriaService<>), typeof(ImportacaoArquivoCategoriaService<>));
            services.AddScoped(typeof(IImportacaoArquivoCategoriaRepository<>), typeof(ImportacaoArquivoCategoriaRepository<>));
            services.AddScoped(typeof(IImportacaoArquivoCategoriaApplication<>), typeof(ImportacaoArquivoCategoriaApplication<>));

            services.AddScoped(typeof(IImportacaoArquivoStatusService<>), typeof(ImportacaoArquivoStatusService<>));
            services.AddScoped(typeof(IImportacaoArquivoStatusRepository<>), typeof(ImportacaoArquivoStatusRepository<>));
            services.AddScoped(typeof(IImportacaoArquivoStatusApplication<>), typeof(ImportacaoArquivoStatusApplication<>));

            services.AddScoped(typeof(IImportacaoArquivoTipoService<>), typeof(ImportacaoArquivoTipoService<>));
            services.AddScoped(typeof(IImportacaoArquivoTipoRepository<>), typeof(ImportacaoArquivoTipoRepository<>));
            services.AddScoped(typeof(IImportacaoArquivoTipoApplication<>), typeof(ImportacaoArquivoTipoApplication<>));

            services.AddScoped(typeof(IImportacaoArquivoTipoItemService<>), typeof(ImportacaoArquivoTipoItemService<>));
            services.AddScoped(typeof(IImportacaoArquivoTipoItemRepository<>), typeof(ImportacaoArquivoTipoItemRepository<>));
            services.AddScoped(typeof(IImportacaoArquivoTipoItemApplication<>), typeof(ImportacaoArquivoTipoItemApplication<>));

            services.AddScoped(typeof(IImportacaoCteService<>), typeof(ImportacaoCteService<>));
            services.AddScoped(typeof(IImportacaoCteRepository<>), typeof(ImportacaoCteRepository<>));
            services.AddScoped(typeof(IImportacaoCteApplication<>), typeof(ImportacaoCteApplication<>));

            services.AddScoped(typeof(IImportacaoCteCargaService<>), typeof(ImportacaoCteCargaService<>));
            services.AddScoped(typeof(IImportacaoCteCargaRepository<>), typeof(ImportacaoCteCargaRepository<>));
            services.AddScoped(typeof(IImportacaoCteCargaApplication<>), typeof(ImportacaoCteCargaApplication<>));

            services.AddScoped(typeof(IImportacaoCteNotaFiscalService<>), typeof(ImportacaoCteNotaFiscalService<>));
            services.AddScoped(typeof(IImportacaoCteNotaFiscalRepository<>), typeof(ImportacaoCteNotaFiscalRepository<>));
            services.AddScoped(typeof(IImportacaoCteNotaFiscalApplication<>), typeof(ImportacaoCteNotaFiscalApplication<>));

            services.AddScoped(typeof(IConfiguracaoCteTransportadorService<>), typeof(ConfiguracaoCteTransportadorService<>));
            services.AddScoped(typeof(IConfiguracaoCteTransportadorRepository<>), typeof(ConfiguracaoCteTransportadorRepository<>));
            services.AddScoped(typeof(IConfiguracaoCteTransportadorApplication<>), typeof(ConfiguracaoCteTransportadorApplication<>));

            services.AddScoped(typeof(IDashboardService<>), typeof(DashboardService<>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IImportacaoConfiguracaoService<>), typeof(ImportacaoConfiguracaoService<>));
            services.AddScoped(typeof(IImportacaoConfiguracaoRepository<>), typeof(ImportacaoConfiguracaoRepository<>));
            services.AddScoped(typeof(IImportacaoConfiguracaoApplication<>), typeof(ImportacaoConfiguracaoApplication<>));

            services.AddScoped(typeof(IImportacaoConfiguracaoTipoService<>), typeof(ImportacaoConfiguracaoTipoService<>));
            services.AddScoped(typeof(IImportacaoConfiguracaoTipoRepository<>), typeof(ImportacaoConfiguracaoTipoRepository<>));
            services.AddScoped(typeof(IImportacaoConfiguracaoTipoApplication<>), typeof(ImportacaoConfiguracaoTipoApplication<>));

            ////////////////////  Fusion (.dbo)  ///////////////////
            services.AddScoped(typeof(ITransportadorService<>), typeof(TransportadorService<>));
            services.AddScoped(typeof(ITransportadorRepository<>), typeof(TransportadorRepository<>));

            services.AddScoped(typeof(ITransportadorCnpjRepository<>), typeof(TransportadorCnpjRepository<>));

            services.AddScoped(typeof(IEntregaConfiguracaoApplication<>), typeof(EntregaConfiguracaoApplication<>));
            services.AddScoped(typeof(IEntregaConfiguracaoService<>), typeof(EntregaConfiguracaoService<>));
            services.AddScoped(typeof(IEntregaConfiguracaoRepository<>), typeof(EntregaConfiguracaoRepository<>));

            services.AddScoped(typeof(IEntregaStageReprocessamentoRepository<>), typeof(EntregaStageReprocessamentoRepository<>));

            services.AddScoped(typeof(ICanalService<>), typeof(CanalService<>));
            services.AddScoped(typeof(ICanalApplication<>), typeof(CanalApplication<>));

            services.AddScoped(typeof(IFaturaService<>), typeof(FaturaService<>));
            services.AddScoped(typeof(IFaturaApplication<>), typeof(FaturaApplication<>));
            services.AddScoped(typeof(IFaturaRepository<>), typeof(FaturaRepository<>));

            services.AddScoped(typeof(IFaturaStatusService<>), typeof(FaturaStatusService<>));
            services.AddScoped(typeof(IFaturaStatusRepository<>), typeof(FaturaStatusRepository<>));
            services.AddScoped(typeof(IFaturaStatusApplication<>), typeof(FaturaStatusApplication<>));

            services.AddScoped(typeof(IFaturaStatusAcaoRepository<>), typeof(FaturaStatusAcaoRepository<>));
            services.AddScoped(typeof(IFaturaStatusAcaoService<>), typeof(FaturaStatusAcaoService<>));
            services.AddScoped(typeof(IFaturaStatusAcaoApplication<>), typeof(FaturaStatusAcaoApplication<>));

            services.AddScoped(typeof(IFaturaHistoricoService<>), typeof(FaturaHistoricoService<>));
            services.AddScoped(typeof(IFaturaHistoricoRepository<>), typeof(FaturaHistoricoRepository<>));
            services.AddScoped(typeof(IFaturaHistoricoApplication<>), typeof(FaturaHistoricoApplication<>));

            services.AddScoped(typeof(IFaturaConciliacaoService<>), typeof(FaturaConciliacaoService<>));
            services.AddScoped(typeof(IFaturaConciliacaoRepository<>), typeof(FaturaConciliacaoRepository<>));
            services.AddScoped(typeof(IFaturaConciliacaoApplication<>), typeof(FaturaConciliacaoApplication<>));

            services.AddScoped(typeof(IFaturaArquivoRepository<>), typeof(FaturaArquivoRepository<>));

            services.AddScoped(typeof(IArquivoCobrancaService<>), typeof(ArquivoCobrancaService<>));
            services.AddScoped(typeof(IArquivoCobrancaRepository<>), typeof(ArquivoCobrancaRepository<>));
            services.AddScoped(typeof(IArquivoCobrancaApplication<>), typeof(ArquivoCobrancaApplication<>));

            services.AddScoped(typeof(IIndicadorConciliacaoService<>), typeof(IndicadorConciliacaoService<>));
            services.AddScoped(typeof(IIndicadorConciliacaoRepository<>), typeof(IndicadorConciliacaoRepository<>));
            services.AddScoped(typeof(IIndicadorConciliacaoApplication<>), typeof(IndicadorConciliacaoApplication<>));

            services.AddScoped(typeof(IEmpresaService<>), typeof(EmpresaService<>));
            services.AddScoped(typeof(IEmpresaRepository<>), typeof(EmpresaRepository<>));
            services.AddScoped(typeof(IEmpresaApplication<>), typeof(EmpresaApplication<>));

            services.AddScoped(typeof(IArquivoImportacaoService<>), typeof(ArquivoImportacaoService<>));
            services.AddScoped(typeof(IArquivoImportacaoRepository<>), typeof(ArquivoImportacaoRepository<>));
            services.AddScoped(typeof(IArquivoImportacaoApplication<>), typeof(ArquivoImportacaoApplication<>));

            services.AddScoped(typeof(ICanalVendaService<>), typeof(CanalVendaService<>));
            services.AddScoped(typeof(ICanalVendaRepository<>), typeof(CanalVendaRepository<>));
            services.AddScoped(typeof(ICanalVendaApplication<>), typeof(CanalVendaApplication<>));

            services.AddScoped(typeof(IEmpresaImportacaoService<>), typeof(EmpresaImportacaoService<>));
            services.AddScoped(typeof(IEmpresaImportacaoRepository<>), typeof(EmpresaImportacaoRepository<>));
            services.AddScoped(typeof(IEmpresaImportacaoApplication<>), typeof(EmpresaImportacaoApplication<>));

            services.AddScoped(typeof(IEmpresaIntegracaoService<>), typeof(EmpresaIntegracaoService<>));
            services.AddScoped(typeof(IEmpresaIntegracaoRepository<>), typeof(EmpresaIntegracaoRepository<>));
            services.AddScoped(typeof(IEmpresaIntegracaoApplication<>), typeof(EmpresaIntegracaoApplication<>));

            services.AddScoped(typeof(IEmpresaIntegracaoItemRepository<>), typeof(EmpresaIntegracaoItemRepository<>));

            services.AddScoped(typeof(IEmpresaIntegracaoItemDetalheService<>), typeof(EmpresaIntegracaoItemDetalheService<>));
            services.AddScoped(typeof(IEmpresaIntegracaoItemDetalheRepository<>), typeof(EmpresaIntegracaoItemDetalheRepository<>));
            services.AddScoped(typeof(IEmpresaIntegracaoItemDetalheApplication<>), typeof(EmpresaIntegracaoItemDetalheApplication<>));

            services.AddScoped(typeof(IEntregaOrigemImportacaoApplication<>), typeof(EntregaOrigemIntegracaoApplication<>));
            services.AddScoped(typeof(IEntregaOrigemImportacaoRepository<>), typeof(EntregaOrigemImportacaoRepository<>));
            services.AddScoped(typeof(IEntregaOrigemImportacaoService<>), typeof(EntregaOrigemImportacaoService<>));

            services.AddScoped(typeof(IIntegracaoService<>), typeof(IntegracaoService<>));
            services.AddScoped(typeof(IIntegracaoRepository<>), typeof(IntegracaoRepository<>));
            services.AddScoped(typeof(IIntegracaoApplication<>), typeof(IntegracaoApplication<>));

            services.AddScoped(typeof(IIntegracaoTipoService<>), typeof(IntegracaoTipoService<>));
            services.AddScoped(typeof(IIntegracaoTipoRepository<>), typeof(IntegracaoTipoRepository<>));
            services.AddScoped(typeof(IIntegracaoTipoApplication<>), typeof(IntegracaoTipoApplication<>));

            services.AddScoped(typeof(IEntregaIncidenteService<>), typeof(EntregaIncidenteService<>));
            services.AddScoped(typeof(IEntregaIncidenteApplication<>), typeof(EntregaIncidenteApplication<>));

            services.AddScoped(typeof(IEntregaDevolucaoService<>), typeof(EntregaDevolucaoService<>));
            services.AddScoped(typeof(IEntregaDevolucaoRepository<>), typeof(EntregaDevolucaoRepository<>));
            services.AddScoped(typeof(IEntregaDevolucaoApplication<>), typeof(EntregaDevolucaoApplication<>));

            services.AddScoped(typeof(IAgendamentoEntregaApplication<>), typeof(AgendamentoEntregaApplication<>));
            services.AddScoped(typeof(IAgendamentoEntregaService<>), typeof(AgendamentoEntregaService<>));
            services.AddScoped(typeof(IAgendamentoEntregaRepository<>), typeof(AgendamentoEntregaRepository<>));

            services.AddScoped(typeof(IEntregaDevolucaoOcorrenciaService<>), typeof(EntregaDevolucaoOcorrenciaService<>));
            services.AddScoped(typeof(IEntregaDevolucaoOcorrenciaRepository<>), typeof(EntregaDevolucaoOcorrenciaRepository<>));
            services.AddScoped(typeof(IEntregaDevolucaoOcorrenciaApplication<>), typeof(EntregaDevolucaoOcorrenciaApplication<>));

            services.AddScoped(typeof(IOcorrenciaTransportadorTipoRepository<>), typeof(OcorrenciaTransportadorTipoRepository<>));

            services.AddScoped(typeof(IEntregaDevolucaoStatusService<>), typeof(EntregaDevolucaoStatusService<>));
            services.AddScoped(typeof(IEntregaDevolucaoStatusRepository<>), typeof(EntregaDevolucaoStatusRepository<>));
            services.AddScoped(typeof(IEntregaDevolucaoStatusApplication<>), typeof(EntregaDevolucaoStatusApplication<>));

            services.AddScoped(typeof(IEntregaDevolucaoStatusAcaoService<>), typeof(EntregaDevolucaoStatusAcaoService<>));
            services.AddScoped(typeof(IEntregaDevolucaoStatusAcaoRepository<>), typeof(EntregaDevolucaoStatusAcaoRepository<>));
            services.AddScoped(typeof(IEntregaDevolucaoStatusAcaoApplication<>), typeof(EntregaDevolucaoStatusAcaoApplication<>));

            services.AddScoped(typeof(IEntregaDevolucaoAcaoService<>), typeof(EntregaDevolucaoAcaoService<>));
            services.AddScoped(typeof(IEntregaDevolucaoAcaoRepository<>), typeof(EntregaDevolucaoAcaoRepository<>));
            services.AddScoped(typeof(IEntregaDevolucaoAcaoApplication<>), typeof(EntregaDevolucaoAcaoApplication<>));

            services.AddScoped(typeof(IEntregaDevolucaoHistoricoService<>), typeof(EntregaDevolucaoHistoricoService<>));
            services.AddScoped(typeof(IEntregaDevolucaoHistoricoRepository<>), typeof(EntregaDevolucaoHistoricoRepository<>));
            services.AddScoped(typeof(IEntregaDevolucaoHistoricoApplication<>), typeof(EntregaDevolucaoHistoricoApplication<>));

            services.AddScoped(typeof(IEntregaTransporteServicoService<>), typeof(EntregaTransporteServicoService<>));
            services.AddScoped(typeof(IEntregaTransporteServicoRepository<>), typeof(EntregaTransporteServicoRepository<>));
            services.AddScoped(typeof(IEntregaTransporteServicoApplication<>), typeof(EntregaTransporteServicoApplication<>));

            services.AddScoped(typeof(IEntregaTransporteTipoService<>), typeof(EntregaTransporteTipoService<>));
            services.AddScoped(typeof(IEntregaTransporteTipoRepository<>), typeof(EntregaTransporteTipoRepository<>));
            services.AddScoped(typeof(IEntregaTransporteTipoApplication<>), typeof(EntregaTransporteTipoApplication<>));

            services.AddScoped(typeof(IProdutoApplication<>), typeof(ProdutoApplication<>));
            services.AddScoped(typeof(IProdutoRepository<>), typeof(ProdutoRepository<>));
            services.AddScoped(typeof(IProdutoService<>), typeof(ProdutoService<>));

            services.AddScoped(typeof(IEmpresaConfigRepository<>), typeof(EmpresaConfigRepository<>));
            services.AddScoped(typeof(IEmpresaSegmentoRepository<>), typeof(EmpresaSegmentoRepository<>));
            services.AddScoped(typeof(ICanalRepository<>), typeof(CanalRepository<>));
            services.AddScoped(typeof(ICnpjDetalheRepository<>), typeof(CnpjDetalheRepository<>));
            services.AddScoped(typeof(ICanalConfigRepository<>), typeof(CanalConfigRepository<>));
            services.AddScoped(typeof(IEmpresaTransportadorConfigRepository<>), typeof(EmpresaTransportadorConfigRepository<>));
            services.AddScoped(typeof(IEmpresaTransporteTipoItemRepository<>), typeof(EmpresaTransporteTipoItemRepository<>));
            services.AddScoped(typeof(IEmpresaTransporteTipoItemService<>), typeof(EmpresaTransporteTipoItemService<>));
            services.AddScoped(typeof(IEmpresaTransporteTipoItemApplication<>), typeof(EmpresaTransporteTipoItemApplication<>));
            services.AddScoped(typeof(IEmpresaTransporteTipoRepository<>), typeof(EmpresaTransporteTipoRepository<>));
            services.AddScoped(typeof(IEmpresaTransporteTipoService<>), typeof(EmpresaTransporteTipoService<>));
            services.AddScoped(typeof(IEmpresaTransporteTipoApplication<>), typeof(EmpresaTransporteTipoApplication<>));
            services.AddScoped(typeof(IEmpresaTransporteConfiguracaoRepository<>), typeof(EmpresaTransporteConfiguracaoRepository<>));
            services.AddScoped(typeof(IEmpresaTransporteConfiguracaoService<>), typeof(EmpresaTransporteConfiguracaoService<>));
            services.AddScoped(typeof(IEmpresaTransporteConfiguracaoApplication<>), typeof(EmpresaTransporteConfiguracaoApplication<>));
            services.AddScoped(typeof(IEmpresaTransporteConfiguracaoItemRepository<>), typeof(EmpresaTransporteConfiguracaoItemRepository<>));
            services.AddScoped(typeof(IEmpresaTransporteConfiguracaoItemService<>), typeof(EmpresaTransporteConfiguracaoItemService<>));
            services.AddScoped(typeof(IEmpresaTransporteConfiguracaoItemApplication<>), typeof(EmpresaTransporteConfiguracaoItemApplication<>));
            services.AddScoped(typeof(IMicroServicoRepository<>), typeof(MicroServicoRepository<>));
            services.AddScoped(typeof(ICanalVendaEntradaRepository<>), typeof(CanalVendaEntradaRepository<>));
            services.AddScoped(typeof(ITabelaTipoRepository<>), typeof(TabelaTipoRepository<>));
            services.AddScoped(typeof(ITabelaTipoCanalVendaRepository<>), typeof(TabelaTipoCanalVendaRepository<>));
            services.AddScoped(typeof(IEmpresaTokenRepository<>), typeof(EmpresaTokenRepository<>));
            services.AddScoped(typeof(ICanalVendaInterfaceRepository<>), typeof(CanalVendaInterfaceRepository<>));
            services.AddScoped(typeof(IMicroServicoRepository<>), typeof(MicroServicoRepository<>));
            services.AddScoped(typeof(ITabelaCorreioCanalRepository<>), typeof(TabelaCorreioCanalRepository<>));

            services.AddScoped(typeof(IEntregaStageService<>), typeof(EntregaStageService<>));
            services.AddScoped(typeof(IEntregaStageApplication<>), typeof(EntregaStageApplication<>));
            services.AddScoped(typeof(IEntregaStageRepository<>), typeof(EntregaStageRepository<>));

            services.AddScoped(typeof(IEntregaStageCallBackService<>), typeof(EntregaStageCallBackService<>));
            services.AddScoped(typeof(IEntregaStageCallBackApplication<>), typeof(EntregaStageCallBackApplication<>));
            services.AddScoped(typeof(IEntregaStageCallBackRepository<>), typeof(EntregaStageCallBackRepository<>));

            services.AddScoped(typeof(IEntregaStageEnvioLogRepository<>), typeof(EntregaStageEnvioLogRepository<>));
            services.AddScoped(typeof(IEntregaStageEnvioLogService<>), typeof(EntregaStageEnvioLogService<>));

            services.AddScoped(typeof(IEntregaRepository<>), typeof(EntregaRepository<>));
            services.AddScoped(typeof(IEntregaStageErroRepository<>), typeof(EntregaStageErroRepository<>));

            services.AddScoped(typeof(IRegraEstoqueService<>), typeof(RegraEstoqueService<>));
            services.AddScoped(typeof(IRegraEstoqueApplication<>), typeof(RegraEstoqueApplication<>));
            services.AddScoped(typeof(IRegraEstoqueRepository<>), typeof(RegraEstoqueRepository<>));

            services.AddScoped(typeof(IGrupoService<>), typeof(GrupoService<>));
            services.AddScoped(typeof(IGrupoApplication<>), typeof(GrupoApplication<>));
            services.AddScoped(typeof(IGrupoRepository<>), typeof(GrupoRepository<>));

            services.AddScoped(typeof(IAnymarketService<>), typeof(AnymarketService<>));
            services.AddScoped(typeof(IAnymarketApplication<>), typeof(AnymarketApplication<>));

            services.AddScoped(typeof(IVtexService<>), typeof(VtexService<>));
            services.AddScoped(typeof(IVtexApplication<>), typeof(VtexApplication<>));

            services.AddScoped(typeof(IEntregaDevolucaoCallBackApplication<>), typeof(EntregaDevolucaoCallBackApplication<>));
            services.AddScoped(typeof(IEntregaDevolucaoCallBackService<>), typeof(EntregaDevolucaoCallBackService<>));
            services.AddScoped(typeof(IEntregaDevolucaoLogRepository<>), typeof(EntregaDevolucaoLogRepository<>));

            services.AddScoped(typeof(IEntregaService<>), typeof(EntregaService<>));
            services.AddScoped(typeof(IEntregaRepository<>), typeof(EntregaRepository<>));

            services.AddScoped(typeof(ITipoRepository<>), typeof(TipoRepository<>));

            services.AddScoped(typeof(IMenuFreteTabelaArquivoApplication<>), typeof(MenuFreteTabelaArquivoApplication<>));
            services.AddScoped(typeof(IMenuFreteTabelaArquivoService<>), typeof(MenuFreteTabelaArquivoService<>));
            services.AddScoped(typeof(IMenuFreteTabelaArquivoRepository<>), typeof(MenuFreteTabelaArquivoRepository<>));

            services.AddScoped(typeof(ILogElasticSearchService), typeof(LogElasticSearchService));

            services.AddScoped(typeof(IPedidoPendenteBSellerService<>), typeof(PedidoPendenteBSellerService<>));
            services.AddScoped(typeof(IPedidoPendenteBSellerApplication<>), typeof(PedidoPendenteBSellerApplication<>));
            services.AddScoped(typeof(IPedidoPendenteBSellerRepository<>), typeof(PedidoPendenteBSellerRepository<>));

            services.AddScoped(typeof(IPedidoPendenteTransportadorService<>), typeof(PedidoPendenteTransportadorService<>));
            services.AddScoped(typeof(IPedidoPendenteTransportadorApplication<>), typeof(PedidoPendenteTransportadorApplication<>));
            services.AddScoped(typeof(IPedidoPendenteTransportadorRepository<>), typeof(PedidoPendenteTransportadorRepository<>));
            services.AddScoped(typeof(IPedidoPendenteTransportadorRepository<>), typeof(PedidoPendenteTransportadorRepository<>));

            services.AddScoped(typeof(IRecotacaoFreteService<>), typeof(RecotacaoFreteService<>));
            services.AddScoped(typeof(IRecotacaoFreteApplication<>), typeof(RecotacaoFreteApplication<>));
            services.AddScoped(typeof(IRecotacaoFreteRepository<>), typeof(RecotacaoFreteRepository<>));

            services.AddScoped(typeof(IContratoTransportadorRegraRepository<>), typeof(ContratoTransportadorRegraRepository<>));
            services.AddScoped(typeof(IContratoTransportadorArquivoTipoApplication<>), typeof(ContratoTransportadorArquivoTipoApplication<>));
            services.AddScoped(typeof(IContratoTransportadorArquivoTipoRepository<>), typeof(ContratoTransportadorArquivoTipoRepository<>));
            services.AddScoped(typeof(IContratoTransportadorArquivoTipoService<>), typeof(ContratoTransportadorArquivoTipoService<>));

            services.AddScoped(typeof(IAtualizacaoTabelasCorreiosApplication<>), typeof(AtualizacaoTabelasCorreiosApplication<>));
            services.AddScoped(typeof(IAtualizacaoTabelasCorreiosRepository<>), typeof(AtualizacaoTabelasCorreiosRepository<>));
            services.AddScoped(typeof(IAtualizacaoTabelasCorreiosService<>), typeof(AtualizacaoTabelasCorreiosService<>));

            services.AddScoped(typeof(IAgendamentoExpedicaoApplication<>), typeof(AgendamentoExpedicaoApplication<>));
            services.AddScoped(typeof(IAgendamentoExpedicaoRepository<>), typeof(AgendamentoExpedicaoRepository<>));
            services.AddScoped(typeof(IAgendamentoExpedicaoService<>), typeof(AgendamentoExpedicaoService<>));

            services.AddScoped(typeof(IAgendamentoRegraApplication<>), typeof(AgendamentoRegraApplication<>));
            services.AddScoped(typeof(IAgendamentoRegraRepository<>), typeof(AgendamentoRegraRepository<>));
            services.AddScoped(typeof(IAgendamentoRegraService<>), typeof(AgendamentoRegraService<>));

            services.AddScoped(typeof(IEntregaOcorrenciaApplication<>), typeof(EntregaOcorrenciaApplication<>));
            services.AddScoped(typeof(IEntregaOcorrenciaRepository<>), typeof(EntregaOcorrenciaRepository<>));
            services.AddScoped(typeof(IEntregaOcorrenciaService<>), typeof(EntregaOcorrenciaService<>));

            services.AddScoped(typeof(IOcorrenciaArquivoApplication<>), typeof(OcorrenciaArquivoApplication<>));
            services.AddScoped(typeof(IOcorrenciaArquivoRepository<>), typeof(OcorrenciaArquivoRepository<>));
            services.AddScoped(typeof(IOcorrenciaArquivoService<>), typeof(OcorrenciaArquivoService<>));

            services.AddScoped(typeof(IEntregaDevolucaoOcorrenciaElasticService<>), typeof(EntregaDevolucaoOcorrenciaElasticService<>));

            services.AddScoped(typeof(IStageConfigRepository<>), typeof(StageConfgRepository<>));

            services.AddScoped(typeof(IAzureApiService), typeof(AzureApiService));

            services.RegisterLogging(configuration);
        }
        public static IServiceCollection RegisterLogging(this IServiceCollection services, IConfiguration configuration)
        {
            var seriLogConfig = configuration.GetSection("SeriLogConfig").Get<SeriLogConfig>();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        //.MinimumLevel.Information()
                        //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                        .WriteTo.Console(outputTemplate: "Level:{Level:u4} Tenant:{TenantId} Time:{Timestamp:yyyy-MM-dd HH:mm:ss} Message:{Message:lj}{NewLine}{Exception}")
                        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(seriLogConfig.ConnectionUri))
                        {
                            AutoRegisterTemplate = true,
                            DetectElasticsearchVersion = true,
                            RegisterTemplateFailure = RegisterTemplateRecovery.IndexAnyway,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                            FailureCallback = e => Console.WriteLine($"Unable to submit event {e.RenderMessage()} to ElasticSearch. Full message : " + e.Properties),
                            NumberOfShards = 1,
                            NumberOfReplicas = 1,
                            IndexFormat = seriLogConfig.IndexPrefix + "-{0:yyyy.MM}",
                            ModifyConnectionSettings = x => x.BasicAuthentication(seriLogConfig.Usuario, seriLogConfig.Senha)
                        })
                        .Enrich.WithProperty("Environment", environment)
                        .Enrich.WithProperty("ApplicationName", seriLogConfig.ApplicationName)
                        .Enrich.WithProperty("Ambiente", seriLogConfig.Ambiente)
                        .Enrich.FromLogContext()
                        .Enrich.WithExceptionDetails()
                        .Enrich.WithMachineName()
                        .Enrich.WithEnvironmentName()
                        .Enrich.WithCorrelationId()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();

            //Indico no registro como singleton e apontando que o ILogger (Serilog Lib) é o responsável pelos Logs
            services.AddSingleton<ILogger>(logger);
            services.AddLogging(builder => builder.AddSerilog(logger));

            return services;
        }
    }
}