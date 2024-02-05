import { BrowserModule, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule, Injector, LOCALE_ID } from '@angular/core'
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { NgSelectModule } from '@ng-select/ng-select';;
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { MatDialogModule, MAT_DATE_LOCALE, MatDividerModule, MAT_DATE_FORMATS, DateAdapter, MatChipsModule, MatStepperModule } from '@angular/material';

import { AuthenticationModule } from './core/auth/authentication.module';
import { NgxPermissionsModule } from 'ngx-permissions';

import { LayoutModule } from './content/layout/layout.module';
import { PartialsModule } from './content/partials/partials.module';
import { CoreModule } from './core/core.module';
import { AclService } from './core/services/acl.service';
import { LayoutConfigService } from './core/services/layout-config.service';
import { MenuConfigService } from './core/services/menu-config.service';
import { PageConfigService } from './core/services/page-config.service';
import { UserService } from './core/services/user.service';
import { UtilsService } from './core/services/utils.service';
import { ClassInitService } from './core/services/class-init.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import {
	GestureConfig,
	MatProgressSpinnerModule,
	MatInputModule,
	MatSelectModule,
	MatSortModule,
	MatTableModule
} from '@angular/material';
import { OverlayModule } from '@angular/cdk/overlay';

import { CdkTableModule } from '@angular/cdk/table';


import { MessengerService } from './core/services/messenger.service';
import { ClipboardService } from './core/services/clipboard.sevice';

import { PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { LayoutConfigStorageService } from './core/services/layout-config-storage.service';
import { LogsService } from './core/services/logs.service';
import { QuickSearchService } from './core/services/quick-search.service';
import { SubheaderService } from './core/services/layout/subheader.service';
import { HeaderService } from './core/services/layout/header.service';
import { MenuHorizontalService } from './core/services/layout/menu-horizontal.service';
import { MenuAsideService } from './core/services/layout/menu-aside.service';
import { LayoutRefService } from './core/services/layout/layout-ref.service';
import { SplashScreenService } from './core/services/splash-screen.service';
import { DataTableService } from './core/services/datatable.service';
import { ClienteService } from './core/services/cliente.service';
import { UsuarioService } from './core/services/usuario.service';
import { ImportacaoArquivoService } from './core/services/importacaoArquivo.service';
import { TransportadorService } from './core/services/transportador.service';
import { CanalService } from './core/services/canal.service';
import { ConfiguracaoCteTransportadorService } from './core/services/configuracaoCteTransportador.service';


import 'hammerjs';
import { ErrorDialogService } from './content/partials/layout/error-dialog/errordialog.service';
import { ErrorDialogComponent } from './content/partials/layout/error-dialog/errordialog.component';
import { HttpConfigInterceptor } from './core/interceptors/httpconfig.interceptor';
import { MOMENT_DATE_FORMATS, MomentDateAdapter } from './core/helpers/momentDateAdapter';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { AlertService } from './core/services/alert.service';
import { LoaderService } from './core/services/loader.service';
import { LoaderInterceptor } from './core/interceptors/loader.interceptor';
import { UsuarioTipoService } from './core/services/usuarioTipo.service';
import { RelatorioService } from './core/services/relatorio.service';
import { DashboardService } from './core/services/dashboard.service';
import { LogDashboardService } from './core/services/logDashboard.service';
import { LogCotacaoFreteService } from './core/services/logCotacaoFrete.service';
import { SistemaMenuService } from './core/services/sistemaMenu.service';
import { NotificacaoService } from './core/services/notificacao.service';
import { EmpresaService } from './core/services/empresa.service';
import { ProvedorService } from './core/services/provedor.service';
import { FaturaService } from './core/services/fatura.service';
import { GeradorFaturaService } from './core/services/geradorFatura.service';
import { FaturaStatusService } from './core/services/faturaStatus.service';
import { FaturaHistoricoService } from './core/services/faturaHistorico.service';
import { ConfiguracaoCteTipoService } from './core/services/configuracaoCteTipo.service';
import { ContratoTransportadorService } from './core/services/contratoTransportador.service';
import { ArquivoCobrancaService } from './core/services/arquivoCobranca.service';
import { ImportacaoConfiguracaoService } from './core/services/importacaoConfiguracao.service';
import { ImportacaoConfiguracaoTipoService } from './core/services/importacaoConfiguracaoTipo.service';
import { EntregaDevolucaoService } from './core/services/entregaDevolucao.service';
import { EntregaDevolucaoStatusService } from './core/services/entregaDevolucaoStatus.service';
import { EntregaDevolucaoStatusAcaoService } from './core/services/entregaDevolucaoStatusAcao.service';
import { EntregaDevolucaoAcaoService } from './core/services/entregaDevolucaoAcao.service';
import { EntregaDevolucaoHistoricoService } from './core/services/entregaDevolucaoHistorico.service';
import { EntregaDevolucaoOcorrenciaService } from './core/services/entregaDevolucaoOcorrencia.service';
import { RegraEstoqueService } from './core/services/regraEstoque.service';
import { GrupoService } from './core/services/grupo.service';
import { ConciliacaoService } from './core/services/conciliacao.service';
import { IntegracaoService } from './core/services/integracao.service';
import { EmpresaIntegracaoService } from './core/services/empresaIntegracao.service';
import { IntegracaoTipoService } from './core/services/integracaoTipo.service';
import { CanalVendaService } from './core/services/canalVenda.service';
import { EntregaOrigemImportacaoService } from './core/services/entregaOrigemImportacao.service';
import { TransportadorCnpjService } from './core/services/transportadorCnpj.service';
import { ToleranciaTipoService } from './core/services/toleranciaTipo.service';
import { ContratoTransportadorHistoricoService } from './core/services/contratoTransportadorHistorico.service';
import { ArquivoImportacaoLogService } from './core/services/arquivoImportacaoLog.service';
import { EmpresaTransporteTipoItemService } from './core/services/empresaTransporteTipoItem.service';
import { EmpresaTransporteTipoService } from './core/services/empresaTransporteTipo.service';
import { EmpresaTransporteConfiguracaoService } from './core/services/empresaTransporteConfiguracao.service';
import { EmpresaTransporteConfiguracaoItemService } from './core/services/empresaTransporteConfiguracaoItem.service';
import { APP_INITIALIZER } from '@angular/core';
import { AppConfigService } from './core/services/app.config.service';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from './core/models/app.config';
import { FaturaStatusAcaoService } from './core/services/faturaStatusAcao.service';
import { FaturaItemService } from './core/services/faturaItem.service';
import { AtualizacaoTabelasCorreiosService } from './core/services/atualizacaoTabelasCorreios.service';
import { AgendamentoExpedicaoService } from './core/services/agendamentoExpedicao.service'; 
import { AgendamentoEntregaService } from './core/services/agendamentoEntrega.service'; 
import { ProdutoService } from './core/services/produto.service';
import { ImportacaoArquivoTipoService } from './core/services/importacaoArquivoTipo.service';
import { ImportacaoArquivoTipoItemService } from './core/services/importacaoArquivoTipoItem.service';
import { ContratoTransportadorArquivoTipoService } from './core/services/contratoTransportadorArquivoTipo.service';
import { MenuFretePeriodoService } from './core/services/menuFretePeriodo.service';
import { AgendamentoRegraService } from './core/services/agendamentoRegra.service'; 
import { EmpresaIntegracaoItemDetalheService } from './core/services/empresaIntegracaoItemDetalhe.service';
import { ImportacaoArquivoStatusService } from './core/services/importacaoArquivoStatus.service';
import { ConciliacaoTipoService } from './core/services/conciliacaoTipo.service';
import { FaturaConciliacaoService } from './core/services/faturaConciliacao.service';
import { EntregaOcorrenciaService } from './core/services/entregaOcorrencia.service';
import { EntregaService } from './core/services/entrega.service';
import { OcorrenciaArquivoService } from './core/services/ocorrenciaArquivo.service';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
	suppressScrollX: true
};

export function initializerFn(jsonAppConfigService: AppConfigService) {
	return () => {
		return jsonAppConfigService.load();
	};
}


registerLocaleData(localePt, 'pt-BR');

@NgModule({
	declarations: [AppComponent, ErrorDialogComponent],
	imports: [
		BrowserAnimationsModule,
		NgSelectModule,
		BrowserModule,
		AppRoutingModule,
		HttpClientModule,
		MatDialogModule,
		LayoutModule,
		PartialsModule,
		CoreModule,
		OverlayModule,
		AuthenticationModule,
		NgxPermissionsModule.forRoot(),
		NgbModule.forRoot(),
		TranslateModule.forRoot(),
		MatProgressSpinnerModule,
		CdkTableModule,
		BrowserModule,
		MatInputModule,
		MatSelectModule,
		MatTableModule,
		MatSortModule,
		MatProgressSpinnerModule,
		MatDividerModule,
		MatChipsModule,
		MatStepperModule,
		MatSlideToggleModule,
		NgxLoadingModule.forRoot({
			animationType: ngxLoadingAnimationTypes.wanderingCubes,
			backdropBackgroundColour: 'rgba(0,0,0,0.1)',
			backdropBorderRadius: '4px',
			primaryColour: '#ffffff',
			secondaryColour: '#ffffff',
			tertiaryColour: '#ffffff',
			successColour: '#00FF00'
		})
	],
	providers: [
		AclService,
		LoaderService,
		LayoutConfigService,
		LayoutConfigStorageService,
		LayoutRefService,
		MenuConfigService,
		PageConfigService,
		UserService,
		ClienteService,
		UsuarioService,
		ImportacaoArquivoService,
		ImportacaoArquivoStatusService,
		ImportacaoArquivoTipoService,
		ImportacaoArquivoTipoItemService,
		UsuarioTipoService,
		UtilsService,
		ClassInitService,
		MessengerService,
		ClipboardService,
		LogsService,
		QuickSearchService,
		DataTableService,
		SplashScreenService,
		ErrorDialogService,
		AlertService,
		LoaderService,
		RelatorioService,
		DashboardService,
		LogDashboardService,
		LogCotacaoFreteService,
		ClienteService,
		SistemaMenuService,
		NotificacaoService,
		EmpresaService,
		ClienteService,
		ProvedorService,
		TransportadorService,
		ConfiguracaoCteTransportadorService,
		ContratoTransportadorService,
		ContratoTransportadorArquivoTipoService,
		ConfiguracaoCteTipoService,
		ConciliacaoTipoService,
		CanalService,
		FaturaService,
		FaturaItemService,
		GeradorFaturaService,
		FaturaStatusService,
		FaturaStatusAcaoService,
		FaturaHistoricoService,
		ArquivoCobrancaService,
		ImportacaoConfiguracaoService,
		ImportacaoConfiguracaoTipoService,
		EntregaDevolucaoService,
		EntregaDevolucaoStatusService,
		EntregaDevolucaoStatusAcaoService,
		EntregaDevolucaoHistoricoService,
		EntregaDevolucaoOcorrenciaService,
		EntregaDevolucaoAcaoService,
		EntregaOcorrenciaService,
		OcorrenciaArquivoService,
		EntregaService,
		RegraEstoqueService,
		GrupoService,
		ConciliacaoService,
		IntegracaoService,
		EmpresaIntegracaoService,
		EmpresaIntegracaoItemDetalheService,
		IntegracaoTipoService,
		CanalVendaService,
		EntregaOrigemImportacaoService,
		TransportadorCnpjService,
		ToleranciaTipoService,
		ContratoTransportadorHistoricoService,
		EmpresaTransporteTipoItemService,
		EmpresaTransporteTipoService,
		EmpresaTransporteConfiguracaoService,
		EmpresaTransporteConfiguracaoItemService,
		ArquivoImportacaoLogService,
		AtualizacaoTabelasCorreiosService,
		AppConfigService,
		AgendamentoExpedicaoService,
		AgendamentoEntregaService,
		ProdutoService,
		MenuFretePeriodoService,
		AgendamentoRegraService,
		FaturaConciliacaoService,
		{
			provide: AppConfig,
			deps: [HttpClient],
			useExisting: AppConfigService
		},
		{
			provide: APP_INITIALIZER,
			deps: [AppConfigService],
			multi: true,
			useFactory: initializerFn
		},
		{ provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
		{ provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
		{ provide: MAT_DATE_FORMATS, useValue: MOMENT_DATE_FORMATS },
		{ provide: DateAdapter, useClass: MomentDateAdapter },

		{ provide: LOCALE_ID, useValue: 'pt-BR' },
		{ provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true },

		{
			provide: PERFECT_SCROLLBAR_CONFIG,
			useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
		},

		// template services
		SubheaderService,
		HeaderService,
		MenuHorizontalService,
		MenuAsideService,
		{
			provide: HAMMER_GESTURE_CONFIG,
			useClass: GestureConfig
		}
	],
	entryComponents: [ErrorDialogComponent],
	bootstrap: [AppComponent]
})
export class AppModule {
	static injector: Injector;
	constructor(injector: Injector) {
		AppModule.injector = injector;
	}
}
