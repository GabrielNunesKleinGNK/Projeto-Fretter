using AutoMapper;
using Fretter.Api.Models;
using Fretter.Api.Models.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Entities.Fusion.EDI;
using Fretter.Domain.Entities.Fusion.SKU;
using System.Linq;

namespace Fretter.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioTipo, UsuarioTipoViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<SistemaMenu, SistemaMenuViewModel>();

            CreateMap<ImportacaoArquivo, ImportacaoArquivoViewModel>();
            CreateMap<ImportacaoArquivoStatus, ImportacaoArquivoStatusViewModel>();
            CreateMap<ImportacaoArquivoTipo, ImportacaoArquivoTipoViewModel>();
            CreateMap<ImportacaoArquivoTipoItem, ImportacaoArquivoTipoItemViewModel>();
            CreateMap<ImportacaoArquivoTipoItemViewModel, ImportacaoArquivoTipoItem>();
            CreateMap<ImportacaoArquivoCriticaViewModel, ImportacaoArquivoCritica>().ReverseMap();

            CreateMap<ConfiguracaoCteTransportador, ConfiguracaoCteTransportadorViewModel>();
            CreateMap<ConfiguracaoCteTipo, ConfiguracaoCteTipoViewModel>();
            CreateMap<ContratoTransportador, ContratoTransportadorViewModel>();
            CreateMap<ContratoTransportadorRegra, ContratoTransportadorRegraViewModel>();
            CreateMap<ContratoTransportadorRegraViewModel, ContratoTransportadorRegra>();
            CreateMap<ContratoTransportadorArquivoTipo, ContratoTransportadorArquivoTipoViewModel>();
            CreateMap<ContratoTransportadorArquivoTipoViewModel, ContratoTransportadorArquivoTipo>();
            CreateMap<ContratoTransportadorHistorico, ContratoTransportadorHistoricoViewModel>();

            CreateMap<Fatura, FaturaViewModel>()
                .ForMember(destino => destino.QuantidadeDivergencia, options => options.MapFrom(origem => origem.QuantidadeEntrega - origem.QuantidadeSucesso))
                .ForMember(destino => destino.ValorTotalDoccob, options => options.MapFrom(origem => origem.ArquivoCobrancas.Sum(x => x.ValorTotal)))
                .ForMember(destino => destino.QtdTotalItensDoccob, options => options.MapFrom(origem => origem.ArquivoCobrancas.Sum(x => x.QtdItens)))
                .ForMember(destino => destino.QtdTotalDoccob, options => options.MapFrom(origem => origem.ArquivoCobrancas.Sum(x => x.QtdTotal)));

            CreateMap<FaturaItem, FaturaItemViewModel>();
            CreateMap<FaturaStatus, FaturaStatusViewModel>();
            CreateMap<FaturaAcao, FaturaAcaoViewModel>();
            CreateMap<FaturaStatusAcao, FaturaStatusAcaoViewModel>();
            CreateMap<FaturaCiclo, FaturaCicloViewModel>()
                .ForMember(destino => destino.Descricao, options => options.MapFrom(origem => $"Fechamento: {origem.DiaFechamento}, Vencimento: {origem.DiaVencimento}"));
            CreateMap<FaturaHistorico, FaturaHistoricoViewModel>()
                .ForMember(destino => destino.QuantidadeDivergencia, options => options.MapFrom(origem => origem.QuantidadeEntrega - origem.QuantidadeSucesso));
            CreateMap<FaturaPeriodo, FaturaPeriodoViewModel>();
            CreateMap<ConciliacaoTipo, ConciliacaoTipoViewModel>();

            CreateMap<ArquivoCobranca, ArquivoCobrancaViewModel>();
            CreateMap<ArquivoCobrancaDocumento, ArquivoCobrancaDocumentoViewModel>();
            CreateMap<ArquivoCobrancaDocumentoItem, ArquivoCobrancaDocumentoItemViewModel>();
            CreateMap<ImportacaoConfiguracao, ImportacaoConfiguracaoViewModel>();
            CreateMap<ImportacaoConfiguracaoTipo, ImportacaoConfiguracaoTipoViewModel>();
            CreateMap<EmpresaImportacaoArquivo, EmpresaImportacaoArquivoViewModel>();
            CreateMap<EmpresaImportacaoDetalhe, EmpresaImportacaoDetalheViewModel>();
            CreateMap<ToleranciaTipo, ToleranciaTipoViewModel>();

            CreateMap<EntregaDevolucao, EntregaDevolucaoViewModel>();
            CreateMap<EntregaDevolucaoStatus, EntregaDevolucaoStatusViewModel>();
            CreateMap<EntregaDevolucaoStatusAcao, EntregaDevolucaoStatusAcaoViewModel>();
            CreateMap<EntregaDevolucaoAcao, EntregaDevolucaoAcaoViewModel>();
            CreateMap<EntregaDevolucaoHistorico, EntregaDevolucaoHistoricoViewModel>();
            CreateMap<EntregaDevolucaoOcorrencia, EntregaDevolucaoOcorrenciaViewModel>();
            CreateMap<Entrega, EntregaViewModel>();
            CreateMap<EntregaOcorrencia, EntregaOcorrenciaViewModel>();
            CreateMap<EntregaOcorrenciaViewModel, EntregaOcorrencia>();

            CreateMap<OcorrenciaArquivo, OcorrenciaArquivoViewModel>();
            CreateMap<OcorrenciaArquivoViewModel, OcorrenciaArquivo>();

            CreateMap<RegraEstoque, RegraEstoqueViewModel>();
            CreateMap<Grupo, GrupoViewModel>();
            CreateMap<Canal, CanalViewModel>();
            CreateMap<CanalViewModel, Canal>();
            CreateMap<CanalVenda, CanalVendaViewModel>();

            // Fusion //
            CreateMap<TransportadorCnpj, TransportadorCnpjViewModel>()
                .ForMember(destino => destino.Descricao, options => options.MapFrom(origem => $"{origem.Apelido} - {origem.CNPJ}"));
            CreateMap<Transportador, TransportadorViewModel>();
            CreateMap<Empresa, EmpresaViewModel>();
            CreateMap<EmpresaIntegracao, EmpresaIntegracaoViewModel>();
            CreateMap<EmpresaIntegracaoItem, EmpresaIntegracaoItemViewModel>();
            CreateMap<EmpresaIntegracaoItemDetalhe, EmpresaIntegracaoItemDetalheViewModel>();
            CreateMap<Integracao, IntegracaoViewModel>();
            CreateMap<IntegracaoTipo, IntegracaoTipoViewModel>();
            CreateMap<EntregaOrigemImportacao, EntregaOrigemImportacaoViewModel>();
            CreateMap<AspNetUsers, AspNetUsersViewModel>();
            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<EmpresaTransporteTipo, EmpresaTransporteTipoViewModel>();
            CreateMap<EmpresaTransporteTipoItem, EmpresaTransporteTipoItemViewModel>();
            CreateMap<EmpresaTransporteConfiguracao, EmpresaTransporteConfiguracaoViewModel>();
            CreateMap<EmpresaTransporteConfiguracaoItem, EmpresaTransporteConfiguracaoItemViewModel>();

            CreateMap<TabelasCorreiosArquivo, TabelasCorreiosViewModel>();
            CreateMap<TabelaArquivoStatus, TabelaArquivoStatusViewModel>();

            CreateMap<AgendamentoExpedicao, AgendamentoExpedicaoViewModel>();
            CreateMap<AgendamentoEntrega, AgendamentoEntregaViewModel>();
            CreateMap<AgendamentoEntregaDestinatario, AgendamentoEntregaDestinatarioViewModel>();
            CreateMap<AgendamentoEntregaProduto, AgendamentoEntregaProdutoViewModel>();
            CreateMap<MenuFretePeriodo, MenuFretePeriodoViewModel>();
            CreateMap<MenuFreteRegiaoCepCapacidade, MenuFreteRegiaoCepCapacidadeViewModel>();
            CreateMap<AgendamentoRegra, AgendamentoRegraViewModel>();
            CreateMap<RegraTipoOperador, RegraTipoOperadorViewModel>();
            CreateMap<RegraTipoItem, RegraTipoItemViewModel>();
            CreateMap<RegraTipo, RegraTipoViewModel>()
                .ForMember(destino => destino.DescricaoRegraTipo, options => options.MapFrom(origem => origem.Nome));
            CreateMap<RegraItem, RegraItemViewModel>();
            CreateMap<RegraGrupoItem, RegraGrupoItemViewModel>()
                .ForMember(destino => destino.Grupo, options => options.MapFrom(origem => origem.Nome));
            ///////////

            CreateMap<ConciliacaoReenvio, ConciliacaoReenvioViewModel>().ReverseMap();
        }
    }
}