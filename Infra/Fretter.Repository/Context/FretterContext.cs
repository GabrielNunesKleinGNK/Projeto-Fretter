using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Maps;
using Fretter.Repository.Maps.Fusion;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Repository.Contexts
{
    public class FretterContext : DbContext, IUnitOfWork<FretterContext>
    {
        public FretterContext(DbContextOptions<FretterContext> options) : base(options) { }
        public int Commit() => this.SaveChanges();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioTipoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new SistemaMenuMap());
            modelBuilder.ApplyConfiguration(new SistemaMenuPermissaoMap());

            modelBuilder.ApplyConfiguration(new ImportacaoArquivoMap());
            modelBuilder.ApplyConfiguration(new ImportacaoArquivoStatusMap());
            modelBuilder.ApplyConfiguration(new ImportacaoArquivoTipoMap());
            modelBuilder.ApplyConfiguration(new ImportacaoArquivoTipoItemMap());
            modelBuilder.ApplyConfiguration(new ImportacaoArquivoCriticaMap());
            modelBuilder.ApplyConfiguration(new ImportacaoConfiguracaoMap());
            modelBuilder.ApplyConfiguration(new ImportacaoConfiguracaoTipoMap());
            modelBuilder.ApplyConfiguration(new ImportacaoCteMap());
            modelBuilder.ApplyConfiguration(new ImportacaoCteCargaMap());
            modelBuilder.ApplyConfiguration(new ImportacaoCteNotaFiscalMap());
            modelBuilder.ApplyConfiguration(new ImportacaoCteComposicaoMap());
            modelBuilder.ApplyConfiguration(new ImportacaoCteImpostoMap());
            modelBuilder.ApplyConfiguration(new ConfiguracaoCteTransportadorMap());
            modelBuilder.ApplyConfiguration(new FaturaMap());
            modelBuilder.ApplyConfiguration(new FaturaItemMap());
            modelBuilder.ApplyConfiguration(new FaturaCicloMap());
            modelBuilder.ApplyConfiguration(new FaturaArquivoMap());
            modelBuilder.ApplyConfiguration(new FaturaArquivoCriticaMap());
            modelBuilder.ApplyConfiguration(new FaturaStatusMap());
            modelBuilder.ApplyConfiguration(new FaturaHistoricoMap());
            modelBuilder.ApplyConfiguration(new FaturaPeriodoMap());
            modelBuilder.ApplyConfiguration(new FaturaAcaoMap());
            modelBuilder.ApplyConfiguration(new FaturaStatusAcaoMap());
            modelBuilder.ApplyConfiguration(new ConfiguracaoCteTipoMap());
            modelBuilder.ApplyConfiguration(new ContratoTransportadorMap());
            modelBuilder.ApplyConfiguration(new ContratoTransportadorArquivoTipoMap());
            modelBuilder.ApplyConfiguration(new IndicadorConciliacaoMap());
            modelBuilder.ApplyConfiguration(new ArquivoCobrancaMap());
            modelBuilder.ApplyConfiguration(new ArquivoCobrancaDocumentoMap());
            modelBuilder.ApplyConfiguration(new ArquivoCobrancaDocumentoItemMap());
            modelBuilder.ApplyConfiguration(new EmpresaImportacaoArquivoMap());
            modelBuilder.ApplyConfiguration(new EmpresaImportacaoDetalheMap());
            modelBuilder.ApplyConfiguration(new ConciliacaoStatusMap());
            modelBuilder.ApplyConfiguration(new ConciliacaoMap());
            modelBuilder.ApplyConfiguration(new ConciliacaoTipoMap());
            modelBuilder.ApplyConfiguration(new ConciliacaoReenvioMap());
            modelBuilder.ApplyConfiguration(new ConciliacaoReenvioHistoricoMap());
            modelBuilder.ApplyConfiguration(new ToleranciaTipoMap());
            modelBuilder.ApplyConfiguration(new ContratoTransportadorHistoricoMap());
            modelBuilder.ApplyConfiguration(new ContratoTransportadorRegraMap());


            modelBuilder.ApplyConfiguration(new ArquivoImportacaoMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new CanalConfigMap());
            modelBuilder.ApplyConfiguration(new CanalMap());
            modelBuilder.ApplyConfiguration(new CanalVendaConfigMap());
            modelBuilder.ApplyConfiguration(new CanalVendaEntradaMap());
            modelBuilder.ApplyConfiguration(new CanalVendaInterfaceMap());
            modelBuilder.ApplyConfiguration(new CanalVendaMap());
            modelBuilder.ApplyConfiguration(new CnpjDetalheMap());
            modelBuilder.ApplyConfiguration(new EmpresaConfigMap());
            modelBuilder.ApplyConfiguration(new EmpresaSegmentoMap());
            modelBuilder.ApplyConfiguration(new EmpresaTokenMap());
            modelBuilder.ApplyConfiguration(new EmpresaTokenTipoMap());
            modelBuilder.ApplyConfiguration(new EmpresaTransportadorConfigMap());
            modelBuilder.ApplyConfiguration(new EmpresaTransporteConfiguracaoMap());
            modelBuilder.ApplyConfiguration(new EmpresaTransporteConfiguracaoItemMap());
            modelBuilder.ApplyConfiguration(new EmpresaTransporteTipoItemMap());
            modelBuilder.ApplyConfiguration(new EmpresaTransporteTipoMap());
            modelBuilder.ApplyConfiguration(new MicroServicoMap());
            modelBuilder.ApplyConfiguration(new TabelaCorreioCanalMap());
            modelBuilder.ApplyConfiguration(new TabelaCorreioCanalTabelaTipoMap());
            modelBuilder.ApplyConfiguration(new TabelaMap());
            modelBuilder.ApplyConfiguration(new TabelaTipoCanalVendaMap());
            modelBuilder.ApplyConfiguration(new TabelaTipoMap());


            modelBuilder.ApplyConfiguration(new EntregaConfiguracaoHistoricoMap());
            modelBuilder.ApplyConfiguration(new EntregaConfiguracaoMap());
            modelBuilder.ApplyConfiguration(new EntregaConfiguracaoItemMap());
            modelBuilder.ApplyConfiguration(new EntregaConfiguracaoTipoMap());
            modelBuilder.ApplyConfiguration(new EntregaStageDestinatarioMap());
            modelBuilder.ApplyConfiguration(new EntregaStageEntradaMap());
            modelBuilder.ApplyConfiguration(new EntregaStageErroMap());
            modelBuilder.ApplyConfiguration(new EntregaStageItemMap());
            modelBuilder.ApplyConfiguration(new EntregaStageLogMap());
            modelBuilder.ApplyConfiguration(new EntregaStageMap());
            modelBuilder.ApplyConfiguration(new EntregaStageRemetenteMap());
            modelBuilder.ApplyConfiguration(new EntregaStageSkuMap());
            modelBuilder.ApplyConfiguration(new EntregaStageReprocessamentoMap());
            modelBuilder.ApplyConfiguration(new EntregaStageEnvioLogMap());
            modelBuilder.ApplyConfiguration(new EntregaTransporteTipoMap());
            modelBuilder.ApplyConfiguration(new EntregaTransporteServicoMap());
            modelBuilder.ApplyConfiguration(new EntregaDevolucaoMap());
            modelBuilder.ApplyConfiguration(new EntregaDevolucaoStatusMap());
            modelBuilder.ApplyConfiguration(new EntregaDevolucaoOcorrenciaMap());
            modelBuilder.ApplyConfiguration(new EntregaDevolucaoAcaoMap());
            modelBuilder.ApplyConfiguration(new EntregaDevolucaoStatusAcaosMap());
            modelBuilder.ApplyConfiguration(new EntregaDevolucaoHistoricoMap());
            modelBuilder.ApplyConfiguration(new EntregaDevolucaoLogMap());
            modelBuilder.ApplyConfiguration(new OcorrenciaTipoMap());
            modelBuilder.ApplyConfiguration(new OcorrenciaTransportadorTipoMap());
            modelBuilder.ApplyConfiguration(new AgendamentoExpedicaoMap());
            modelBuilder.ApplyConfiguration(new AgendamentoEntregaMap());
            modelBuilder.ApplyConfiguration(new AgendamentoEntregaDestinatarioMap());
            modelBuilder.ApplyConfiguration(new AgendamentoEntregaProdutoMap());
            modelBuilder.ApplyConfiguration(new MenuFretePeriodoMap());
            modelBuilder.ApplyConfiguration(new MenuFreteRegiaoCepCapacidadeMap());
            modelBuilder.ApplyConfiguration(new AgendamentoRegraMap());
            modelBuilder.ApplyConfiguration(new RegraTipoMap());
            modelBuilder.ApplyConfiguration(new RegraTipoOperadorMap());
            modelBuilder.ApplyConfiguration(new RegraTipoItemMap());
            modelBuilder.ApplyConfiguration(new RegraGrupoItemMap());
            modelBuilder.ApplyConfiguration(new RegraItemMap());

            // Fusion Maps //
            modelBuilder.ApplyConfiguration(new Maps.Fusion.TransportadorCnpjMap());
            modelBuilder.ApplyConfiguration(new Maps.Fusion.TransportadorMap());
            modelBuilder.ApplyConfiguration(new EntregaMap());
            modelBuilder.ApplyConfiguration(new EmpresaIntegracaoMap());
            modelBuilder.ApplyConfiguration(new EmpresaIntegracaoItemMap());
            modelBuilder.ApplyConfiguration(new EmpresaIntegracaoItemDetalheMap());
            modelBuilder.ApplyConfiguration(new IntegracaoMap());
            modelBuilder.ApplyConfiguration(new IntegracaoTipoMap());
            modelBuilder.ApplyConfiguration(new EntregaOrigemImportacaoMap());
            modelBuilder.ApplyConfiguration(new EntregaOcorrenciaMap());
            modelBuilder.ApplyConfiguration(new OcorrenciaArquivoMap());
            modelBuilder.ApplyConfiguration(new AspNetUsersMap());
            modelBuilder.ApplyConfiguration(new RecotacaoFreteMap());
            modelBuilder.ApplyConfiguration(new TabelasCorreiosArquivoMap());
            modelBuilder.ApplyConfiguration(new TabelaArquivoStatusMap());
            modelBuilder.ApplyConfiguration(new TipoMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());

            //SKU//
            modelBuilder.ApplyConfiguration(new Maps.Fusion.SKU.GrupoMap());
            modelBuilder.ApplyConfiguration(new Maps.Fusion.SKU.RegraEstoqueMap());
            ////////////////
        }
    }
}