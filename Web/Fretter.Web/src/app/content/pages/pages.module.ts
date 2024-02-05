import { LayoutModule } from '../layout/layout.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutingModule } from './pages-routing.module';
import { PagesComponent } from './pages.component';
import { PartialsModule } from '../partials/partials.module';
import { ActionComponent } from './header/action/action.component';
import { CoreModule } from '../../core/core.module';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxJsonViewerModule } from 'ngx-json-viewer';

import { IConfig, NgxMaskModule } from 'ngx-mask';
export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

import {
	MatTableModule,
	MatFormFieldModule,
	MatInputModule,
	MatRadioModule,
	MatButtonModule,
	MatIconModule,
	MatSortModule,
	MatPaginatorModule,
	MatDialogModule,
	MAT_DIALOG_DEFAULT_OPTIONS,
	MatProgressSpinnerModule,
	MatDatepickerModule,
	MatNativeDateModule,
	MatDividerModule,
	MatTabsModule,
	MatSelectModule,
	MatCheckboxModule,
	MatStepperModule,
	MatProgressBar,
	MatCardModule,
	MatProgressBarModule,
	MatChipsModule,
	MatChip,
	MatChipList,
	MatTooltipModule,
	MatSlideToggleModule,
	MatMenuModule,
} from '@angular/material';

import { HttpClientModule } from '@angular/common/http';
import { ErrorPageComponent } from './snippets/error-page/error-page.component';
import { UsuarioListComponent } from './components/usuario/list/usuario.list.component';
import { UsuarioEditComponent } from './components/usuario/edit/usuario.edit.component';
import { ImportacaoArquivoListComponent } from './components/importacaoArquivo/list/importacaoArquivo.list.component';
import { ImportacaoArquivoEditComponent } from './components/importacaoArquivo/edit/importacaoArquivo.edit.component';
import { ImportacaoArquivoCriticaComponent } from './components/importacaoArquivo/criticas/importacaoArquivo.critica.component';
import { NgSelectModule } from '@ng-select/ng-select';

import { HomeComponent } from './components/home/home.component';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardFiltroComponent } from './components/dashboard/dashboard.filtro/dashboard.filtro.component';
import { DashboardProdutoComponent } from './components/dashboard/dashboard.produto/dashboard.produto.component';
import { DashboardResumoComponent } from './components/dashboard/dashboard.resumo/dashboard.resumo.component';
import { DashboardRegionalComponent } from './components/dashboard/dashboard.regional/dashboard.regional.component';


import { ArquivoImportacaoLogComponent } from './components/arquivoImportacaoLog/arquivoImportacaoLog.component';
import { ArquivoImportacaoLogFiltroComponent } from './components/arquivoImportacaoLog/arquivoImportacaoLog.filtro/arquivoImportacaoLog.filtro.component';
import { ArquivoImportacaoLogListComponent } from './components/arquivoImportacaoLog/arquivoImportacaoLog.list/arquivoImportacaoLog.list.component';


import { LogDashboardComponent } from './components/logDashboard/logDashboard.component';
import { LogDashboardFiltroComponent } from './components/logDashboard/logDashboard.filtro/logDashboard.filtro.component';
import { LogDashboardResumoDiarioComponent } from './components/logDashboard/logDashboard.resumoDiario/logDashboard.resumoDiario.component';
import { LogDashboardResumoMensagemComponent } from './components/logDashboard/logDashboard.resumoMensagem/logDashboard.resumoMensagem.component';
import { LogDashboardResumoProcessoComponent } from './components/logDashboard/logDashboard.resumoProcesso/logDashboard.resumoProcesso.component';
import { LogDashboardListComponent } from './components/logDashboard/logDashboard.list/logDashboard.list.component';
import { JsonViewComponent } from './components/logDashboard/logDashboard.jsonView/jsonView.component';


import { GeradorFaturaComponent } from './components/geradorFatura/geradorFatura.component';
import { GeradorFaturaFiltroComponent } from './components/geradorFatura/filtro/geradorFatura.filtro.component';
import { GeradorFaturaListComponent } from './components/geradorFatura/list/geradorFatura.list.component';
import { GeradorFaturaEditComponent } from './components/geradorFatura/edit/geradorFatura.edit.component';
import { CriticaLeituraDoccobListComponent } from './components/geradorFatura/criticas/criticaLeituraDoccob.list.component';

import { LogCotacaoFreteComponent } from './components/logCotacaoFrete/logCotacaoFrete.component';
import { LogCotacaoFreteFiltroComponent } from './components/logCotacaoFrete/logCotacaoFrete.filtro/logCotacaoFrete.filtro.component';
import { LogCotacaoFreteListComponent } from './components/logCotacaoFrete/logCotacaoFrete.list/logCotacaoFrete.list.component';
import { LogCotacaoFreteJsonViewComponent } from './components/logCotacaoFrete/logCotacaoFrete.jsonView/logCotacaoFrete.jsonView.component';

import { DashboardresumoDiarioEntregasComponent } from './components/dashboard/dashboard.resumoDiarioEntregas/dashboard.resumoDiarioEntregas.component';
import { DashboardTransportadoresQuantidadeComponent } from './components/dashboard/dashboard.transportadorQuantidade/dashboard.transportadorQuantidade.component';
import { DashboardTransportadoresValorComponent } from './components/dashboard/dashboard.transportadorValor/dashboard.transportadorValor.component';
import { DashboardTransportadoresListComponent } from './components/dashboard/dashboard.transportadorList/dashboard.transportadorList.component';

import { CurrencyMaskModule } from 'ng2-currency-mask';
import { ConfiguracaoCteTransportadorListComponent } from './components/configuracaoCteTransportador/list/configuracaoCteTransportador.list.component';
import { ConfiguracaoCteTransportadorEditComponent } from './components/configuracaoCteTransportador/edit/configuracaoCteTransportador.edit.component';

import { FaturaListComponent } from './components/fatura/list/fatura.list.component';
import { FaturaEditComponent } from './components/fatura/edit/fatura.edit.component';
import { FaturaHistoricoComponent } from './components/fatura/fatura.historico/fatura.historico.component';
import { UsuarioTipoEditComponent } from './components/usuarioTipo/edit/usuarioTipo.edit.component';
import { UsuarioTipoListComponent } from './components/usuarioTipo/list/usuarioTipo.list.component';
import { ContratoTransportadorEditComponent } from './components/contratoTransportador/edit/contratoTransportador.edit.component';
import { ContratoTransportadorListComponent } from './components/contratoTransportador/list/contratoTransportador.list.component';
import { ArquivoCobrancaComponent } from './components/fatura/edit/arquivoCobranca/arquivoCobranca.component';
import { ImportacaoConfiguracaoListComponent } from './components/configuracoes/importacaoConfiguracao/list/importacaoConfiguracao.list.component';
import { ImportacaoConfiguracaoEditComponent } from './components/configuracoes/importacaoConfiguracao/edit/importacaoConfiguracao.edit.component';
import { EmpresaImportacaoListComponent } from './components/empresaImportacao/list/empresaImportacao.list.component';
import { EmpresaImportacaDetalheComponent } from './components/empresaImportacao/list/detalhe/empresaImportacao.detalhe.component';
import { EmpresaIntegracaoListComponent } from './empresa/configuracoes/empresaIntegracao/list/empresaIntegracao.list.component';
import { EmpresaIntegracaoEditComponent } from './empresa/configuracoes/empresaIntegracao/edit/empresaIntegracao.edit.component';
import { IntegracaoListComponent } from './empresa/configuracoes/integracao/list/integracao.list.component';
import { IntegracaoEditComponent } from './empresa/configuracoes/integracao/edit/integracao.edit.component';
import { TesteIntegracaoComponent } from './empresa/configuracoes/integracao/list/testeIntegracao/testeIntegracao.component';

import { EntregaDevolucaoListComponent } from './components/entregaDevolucao/list/entregaDevolucao.list.component';
import { EntregaDevolucaoEditComponent } from './components/entregaDevolucao/edit/entregaDevolucao.edit.component';
import { EntregaDevolucaoHistoricoComponent } from './components/entregaDevolucao/entregaDevolucao.historico/entregaDevolucao.historico.component';
import { EntregaDevolucaoOcorrenciaComponent } from './components/entregaDevolucao/entregaDevolucao.ocorrencia/entregaDevolucao.ocorrencia.component';
import { RegraEstoqueListComponent } from './empresa/configuracoes/regraEstoque/list/regraEstoque.list.component';
import { RegraEstoqueEditComponent } from './empresa/configuracoes/regraEstoque/edit/regraEstoque.edit.component';
import { RelatorioDetalhadoListComponent } from './conciliacao/relatorios/detalhado/list/relatorioDetalhado.list.component';
import { RelatorioDetalhadoDetalhesComponent } from './conciliacao/relatorios/detalhado/detalhes/relatorioDetalhado.detalhes.component';
import { RelatorioDetalhadoValoresListComponent } from './conciliacao/relatorios/detalhado/detalhes/valores/valores.list.component';
import { RelatorioDetalhadoPesosListComponent } from './conciliacao/relatorios/detalhado/detalhes/pesos/pesos.list.component';
import { RelatorioDetalhadoRecotacoesListComponent } from './conciliacao/relatorios/detalhado/detalhes/recotacoes/recotacoes.list.component';
import { EntregaDevolucaoHistoricoJsonComponent } from './components/entregaDevolucao/entregaDevolucao.historicoJson/entregaDevolucao.historicoJson.component';
import { EntregaDevolucaoHistoricoXMLComponent } from './components/entregaDevolucao/entregaDevolucao.historicoXML/entregaDevolucao.historicoXML.component';
import { ContratoTransportadorHistoricoListComponent } from './components/contratoTransportador/historico/list/contratoTransportadorHistorico.list.component';
import { ContratoTransportadorHistoricoEditComponent } from './components/contratoTransportador/historico/edit/contratoTransportadorHistorico.edit.component';
import { EmpresaTransporteConfiguracaoListComponent } from './components/empresaTransporte/list/empresaTransporteConfiguracao.list.component';
import { EmpresaTransporteConfiguracaoEditComponent } from './components/empresaTransporte/edit/empresaTransporteConfiguracao.edit.component';
import { EmpresaTransporteConfiguracaoItemListComponent } from './components/empresaTransporte/item/empresaTransporteConfiguracaoItem.list.component';
import { FaturaItemListComponent } from './components/fatura/faturaItem/faturaItem.list.component';
import { AtualizacaoTabelasCorreiosListComponent } from './components/atualizacaoTabelasCorreios/list/atualizacaoTabelasCorreios.list.component';
import { ContratoTransportadorRegraListComponent } from './components/contratoTransportador/edit/regra/list/regra.list.component';
import { ContratoTransportadorRegraEditComponent } from './components/contratoTransportador/edit/regra/edit/regra.edit.component';
import { ContratoTransportadorArquivoTipoListComponent } from './components/contratoTransportador/edit/arquivo/list/contratoTransportadorArquivoTipo.list.component';
import { ContratoTransportadorArquivoTipoEditComponent } from './components/contratoTransportador/edit/arquivo/edit/contratoTransportadorArquivoTipo.edit.component';

import { AgendamentoExpedicaoEditComponent } from './agendamento/expedicao/edit/agendamentoExpedicao.edit.component';
import { AgendamentoExpedicaoListComponent } from './agendamento/expedicao/list/agendamentoExpedicao.list.component';
import { AgendamentoEntregaListComponent } from './agendamento/entrega/list/agendamentoEntrega.list.component';
import { AgendamentoEntregaWizardComponent } from './agendamento/entrega/wizard/agendamentoEntrega.wizard.component';
import { AgendamentoEntregaWizardProdutosComponent } from './agendamento/entrega/wizard/steps/agendamentoEntrega.wizard.produtos.component';
import { AgendamentoEntregaWizardDadosAgendamentoComponent } from './agendamento/entrega/wizard/steps/agendamentoEntrega.wizard.dadosAgendamento.component';
import { AgendamentoEntregaWizardDisponibilidadeComponent } from './agendamento/entrega/wizard/steps/agendamentoEntrega.wizard.disponibilidade.component';
import { AgendamentoEntregaWizardResumoComponent } from './agendamento/entrega/wizard/steps/agendamentoEntrega.wizard.resumo.component';
import { AgendamentoRegraListComponent } from './agendamento/regra/list/agendamentoRegra.list.component';
import { AgendamentoRegraEditComponent } from './agendamento/regra/edit/agendamentoRegra.edit.component';

import { ClausulasComponent } from './components/shared/clausulas/clausulas.component';
import { EmpresaIntegracaoItemDetalheListComponent } from './empresa/configuracoes/empresaIntegracao/detalhe/list/itemDetalhe.list.component';
import { EmpresaIntegracaoItemDetalheEditComponent } from './empresa/configuracoes/empresaIntegracao/detalhe/edit/itemDetalhe.edit.component';
import { FaturaConciliacaoIntegracaoComponent } from './components/fatura/faturaConciliacaoIntegracao/faturaConciliacaoIntegracao.component';
import { PesquisaProdutoComponent } from './components/shared/pesquisaProduto/pesquisaProduto.component';
import { EntregaOcorrenciaEditComponent } from './ocorrencia/entregaOcorrencia/edit/entregaOcorrencia.edit.component';
import { EntregaOcorrenciaListComponent } from './ocorrencia/entregaOcorrencia/list/entregaOcorrencia.list.component';
import { OcorrenciaEmpresaListComponent } from './ocorrencia/entregaOcorrencia/list/listadepara/ocorrenciaEmpresa.list.component';
import { DownloadArquivoOcorrenciaEditComponent } from './ocorrencia/entregaOcorrencia/list/download/downloadOcorrenciaArquivo.edit.component';
import { OcorrenciaEntregaListComponent } from './ocorrencia/entregaOcorrencia/list/ocorrenciasEntrega/ocorrenciaEntrega.list.component';
import { OcorrenciaArquivoListComponent } from './ocorrencia/entregaOcorrencia/list/ocorrenciaArquivo/ocorrenciaArquivo.list.component';


// export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
//     align: "right",
//     allowNegative: true,
//     decimal: ",",
//     precision: 2,
//     prefix: "R$ ",
//     suffix: "",
//     thousands: "."
// };


@NgModule({
	declarations: [
		PagesComponent,
		ActionComponent,
		ErrorPageComponent,
		HomeComponent,
		UsuarioListComponent,
		UsuarioEditComponent,
		UsuarioTipoEditComponent,
		UsuarioTipoListComponent,
		ImportacaoArquivoListComponent,
		ImportacaoArquivoEditComponent,
		ImportacaoArquivoCriticaComponent,
		ConfiguracaoCteTransportadorEditComponent,
		ConfiguracaoCteTransportadorListComponent,
		ContratoTransportadorEditComponent,
		ContratoTransportadorListComponent,
		DashboardComponent,
		DashboardFiltroComponent,
		DashboardProdutoComponent,
		DashboardResumoComponent,
		DashboardRegionalComponent,
		DashboardresumoDiarioEntregasComponent,
		DashboardTransportadoresQuantidadeComponent,
		DashboardTransportadoresValorComponent,
		DashboardTransportadoresListComponent,
		LogDashboardComponent,
		LogDashboardFiltroComponent,
		LogDashboardResumoDiarioComponent,
		LogDashboardResumoMensagemComponent,
		LogDashboardResumoProcessoComponent,
		LogDashboardListComponent,
		JsonViewComponent,
		LogCotacaoFreteComponent,
		LogCotacaoFreteListComponent,
		LogCotacaoFreteFiltroComponent,
		LogCotacaoFreteJsonViewComponent,
		FaturaListComponent,
		FaturaEditComponent,
		FaturaHistoricoComponent,
		ArquivoCobrancaComponent,
		ImportacaoConfiguracaoListComponent,
		ImportacaoConfiguracaoEditComponent,
		EmpresaImportacaoListComponent,
		EmpresaImportacaDetalheComponent,
		EntregaDevolucaoListComponent,
		EntregaDevolucaoEditComponent,
		EntregaDevolucaoHistoricoComponent,
		EntregaDevolucaoOcorrenciaComponent,
		EntregaDevolucaoHistoricoJsonComponent,
		EntregaDevolucaoHistoricoXMLComponent,
		EntregaOcorrenciaListComponent,
		EntregaOcorrenciaEditComponent,
		OcorrenciaEntregaListComponent,
		OcorrenciaEmpresaListComponent,
		DownloadArquivoOcorrenciaEditComponent,
		OcorrenciaArquivoListComponent,
		RegraEstoqueListComponent,
		RegraEstoqueEditComponent,
		RelatorioDetalhadoListComponent,
		RelatorioDetalhadoDetalhesComponent,
		RelatorioDetalhadoValoresListComponent,
		RelatorioDetalhadoPesosListComponent,
		RelatorioDetalhadoRecotacoesListComponent,
		IntegracaoListComponent,
		IntegracaoEditComponent,
		TesteIntegracaoComponent,
		EmpresaIntegracaoListComponent,
		EmpresaIntegracaoEditComponent,
		EmpresaIntegracaoItemDetalheListComponent,
		EmpresaIntegracaoItemDetalheEditComponent,
		ContratoTransportadorHistoricoListComponent,
		ContratoTransportadorHistoricoEditComponent,
		EmpresaTransporteConfiguracaoListComponent,
		EmpresaTransporteConfiguracaoEditComponent,
		EmpresaTransporteConfiguracaoItemListComponent,
		ArquivoImportacaoLogComponent,
		ArquivoImportacaoLogFiltroComponent,
		ArquivoImportacaoLogListComponent,
		GeradorFaturaComponent,
		GeradorFaturaFiltroComponent,
		GeradorFaturaListComponent,
		GeradorFaturaEditComponent,
		CriticaLeituraDoccobListComponent,
		FaturaItemListComponent,
		AtualizacaoTabelasCorreiosListComponent,
		ContratoTransportadorRegraEditComponent,
		ContratoTransportadorRegraListComponent,
		ContratoTransportadorArquivoTipoEditComponent,
		ContratoTransportadorArquivoTipoListComponent,
		AgendamentoExpedicaoEditComponent,
		AgendamentoExpedicaoListComponent,
		AgendamentoEntregaListComponent,
		AgendamentoEntregaWizardComponent,
		AgendamentoEntregaWizardProdutosComponent,
		AgendamentoEntregaWizardDadosAgendamentoComponent,
		AgendamentoEntregaWizardDisponibilidadeComponent,
		AgendamentoEntregaWizardResumoComponent,
		AgendamentoRegraListComponent,
		AgendamentoRegraEditComponent,
		ClausulasComponent,
		FaturaConciliacaoIntegracaoComponent,
		PesquisaProdutoComponent
	],
	entryComponents: [
		EmpresaImportacaDetalheComponent,
		RegraEstoqueEditComponent,
		ContratoTransportadorHistoricoListComponent,
		JsonViewComponent,
		ContratoTransportadorHistoricoEditComponent,
		LogCotacaoFreteJsonViewComponent,
		EmpresaTransporteConfiguracaoEditComponent,
		EmpresaTransporteConfiguracaoItemListComponent,
		GeradorFaturaEditComponent,
		FaturaItemListComponent,
		AtualizacaoTabelasCorreiosListComponent,
		ContratoTransportadorEditComponent,
		ContratoTransportadorRegraEditComponent,
		AgendamentoEntregaListComponent,
		AgendamentoEntregaWizardComponent,
		AgendamentoEntregaWizardProdutosComponent,
		ContratoTransportadorRegraListComponent,
		ContratoTransportadorArquivoTipoEditComponent,
		ContratoTransportadorArquivoTipoListComponent,
		AgendamentoEntregaWizardDadosAgendamentoComponent,
		AgendamentoEntregaWizardDisponibilidadeComponent,
		AgendamentoEntregaWizardResumoComponent
	],
	imports: [
		MatDialogModule,
		CommonModule,
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		PagesRoutingModule,
		CoreModule,
		LayoutModule,
		PartialsModule,
		AngularEditorModule,
		MatTableModule,
		MatFormFieldModule,
		MatInputModule,
		MatDatepickerModule,
		MatNativeDateModule,
		MatRadioModule,
		MatButtonModule,
		MatDividerModule,
		MatIconModule,
		MatSortModule,
		MatPaginatorModule,
		MatProgressSpinnerModule,
		MatTabsModule,
		MatSelectModule,
		NgSelectModule,
		MatCheckboxModule,
		MatStepperModule,
		MatCardModule,
		MatProgressBarModule,
		NgxMaskModule.forRoot(),
		CurrencyMaskModule,
		MatChipsModule,
		MatTooltipModule,
		NgxJsonViewerModule,
		MatSlideToggleModule,
		MatMenuModule
	],
	exports: [
		MatDatepickerModule,
		MatProgressBarModule
	],
	// providers: [
	// 	{ provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig }
	// ]
})
export class PagesModule {
}
