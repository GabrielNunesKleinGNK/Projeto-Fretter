import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PagesComponent } from './pages.component';
import { ActionComponent } from './header/action/action.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { ErrorPageComponent } from './snippets/error-page/error-page.component';
import { HomeComponent } from './components/home/home.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UsuarioListComponent } from './components/usuario/list/usuario.list.component';
import { UsuarioEditComponent } from './components/usuario/edit/usuario.edit.component';
import { ImportacaoArquivoListComponent } from './components/importacaoArquivo/list/importacaoArquivo.list.component';
import { ImportacaoArquivoEditComponent } from './components/importacaoArquivo/edit/importacaoArquivo.edit.component';
import { ImportacaoArquivoCriticaComponent } from './components/importacaoArquivo/criticas/importacaoArquivo.critica.component';
import { FaturaListComponent } from './components/fatura/list/fatura.list.component';
import { FaturaEditComponent } from './components/fatura/edit/fatura.edit.component';

import { ConfiguracaoCteTransportadorListComponent } from './components/configuracaoCteTransportador/list/configuracaoCteTransportador.list.component';
import { ConfiguracaoCteTransportadorEditComponent } from './components/configuracaoCteTransportador/edit/configuracaoCteTransportador.edit.component';
import { ContratoTransportadorListComponent } from './components/contratoTransportador/list/contratoTransportador.list.component';
import { ContratoTransportadorEditComponent } from './components/contratoTransportador/edit/contratoTransportador.edit.component';

import { ImportacaoConfiguracaoListComponent } from './components/configuracoes/importacaoConfiguracao/list/importacaoConfiguracao.list.component';
import { ImportacaoConfiguracaoEditComponent } from './components/configuracoes/importacaoConfiguracao/edit/importacaoConfiguracao.edit.component';
import { EmpresaImportacaoListComponent } from './components/empresaImportacao/list/empresaImportacao.list.component';

import { EntregaDevolucaoListComponent } from './components/entregaDevolucao/list/entregaDevolucao.list.component';
import { EntregaDevolucaoEditComponent } from './components/entregaDevolucao/edit/entregaDevolucao.edit.component';
import { EntregaDevolucaoOcorrenciaComponent } from './components/entregaDevolucao/entregaDevolucao.ocorrencia/entregaDevolucao.ocorrencia.component';
import { RegraEstoqueListComponent } from './empresa/configuracoes/regraEstoque/list/regraEstoque.list.component';
import { EmpresaIntegracaoEditComponent } from './empresa/configuracoes/empresaIntegracao/edit/empresaIntegracao.edit.component';
import { EmpresaIntegracaoListComponent } from './empresa/configuracoes/empresaIntegracao/list/empresaIntegracao.list.component';
import { IntegracaoEditComponent } from './empresa/configuracoes/integracao/edit/integracao.edit.component';
import { IntegracaoListComponent } from './empresa/configuracoes/integracao/list/integracao.list.component';
import { TesteIntegracaoComponent } from './empresa/configuracoes/integracao/list/testeIntegracao/testeIntegracao.component';
import { RelatorioDetalhadoListComponent } from './conciliacao/relatorios/detalhado/list/relatorioDetalhado.list.component';
import { AuthGuard } from '../../auth.guard';
import { EntregaDevolucaoHistoricoJsonComponent } from './components/entregaDevolucao/entregaDevolucao.historicoJson/entregaDevolucao.historicoJson.component';
import { EntregaDevolucaoHistoricoXMLComponent } from './components/entregaDevolucao/entregaDevolucao.historicoXML/entregaDevolucao.historicoXML.component';
import { RelatorioDetalhadoDetalhesComponent } from './conciliacao/relatorios/detalhado/detalhes/relatorioDetalhado.detalhes.component';
import { LogDashboardComponent } from './components/logDashboard/logDashboard.component';
import { ArquivoImportacaoLogComponent } from './components/arquivoImportacaoLog/arquivoImportacaoLog.component';
import { LogCotacaoFreteComponent } from './components/logCotacaoFrete/logCotacaoFrete.component';
import { EmpresaTransporteConfiguracaoEditComponent } from './components/empresaTransporte/edit/empresaTransporteConfiguracao.edit.component';
import { EmpresaTransporteConfiguracaoListComponent } from './components/empresaTransporte/list/empresaTransporteConfiguracao.list.component';
import { GeradorFaturaComponent } from './components/geradorFatura/geradorFatura.component';
import { AtualizacaoTabelasCorreiosListComponent } from './components/atualizacaoTabelasCorreios/list/atualizacaoTabelasCorreios.list.component';
import { AgendamentoExpedicaoListComponent } from './agendamento/expedicao/list/agendamentoExpedicao.list.component';
import { AgendamentoExpedicaoEditComponent } from './agendamento/expedicao/edit/agendamentoExpedicao.edit.component';
import { AgendamentoEntregaListComponent } from './agendamento/entrega/list/agendamentoEntrega.list.component';
import { AgendamentoRegraListComponent } from './agendamento/regra/list/agendamentoRegra.list.component';
import { AgendamentoRegraEditComponent } from './agendamento/regra/edit/agendamentoRegra.edit.component';
import { EmpresaIntegracaoItemDetalheListComponent } from './empresa/configuracoes/empresaIntegracao/detalhe/list/itemDetalhe.list.component';
import { EmpresaIntegracaoItemDetalheEditComponent } from './empresa/configuracoes/empresaIntegracao/detalhe/edit/itemDetalhe.edit.component';
import { FaturaConciliacaoIntegracaoComponent } from './components/fatura/faturaConciliacaoIntegracao/faturaConciliacaoIntegracao.component';
import { EntregaOcorrenciaEditComponent } from './ocorrencia/entregaOcorrencia/edit/entregaOcorrencia.edit.component';
import { PesquisaProdutoComponent } from './components/shared/pesquisaProduto/pesquisaProduto.component';
import { EntregaOcorrenciaListComponent } from './ocorrencia/entregaOcorrencia/list/entregaOcorrencia.list.component';
import { OcorrenciaEmpresaListComponent } from './ocorrencia/entregaOcorrencia/list/listadepara/ocorrenciaEmpresa.list.component';
import { DownloadArquivoOcorrenciaEditComponent } from './ocorrencia/entregaOcorrencia/list/download/downloadOcorrenciaArquivo.edit.component';
import { OcorrenciaEntregaListComponent } from './ocorrencia/entregaOcorrencia/list/ocorrenciasEntrega/ocorrenciaEntrega.list.component';
import { OcorrenciaArquivoListComponent } from './ocorrencia/entregaOcorrencia/list/ocorrenciaArquivo/ocorrenciaArquivo.list.component';

const routes: Routes = [
	{
		path: '',
		component: PagesComponent,
		// Remove comment to enable login
		canActivate: [NgxPermissionsGuard],
		data: {
			permissions: {
				only: ['ADMIN', 'USER'],
				except: ['GUEST'],
				redirectTo: '/login'
			}
		},
		children: [
			{
				path: '',
				component: HomeComponent
			},
			{
				path: 'builder',
				loadChildren: './builder/builder.module#BuilderModule'
			},
			{
				path: 'header/actions',
				component: ActionComponent,
			},
			{
				path: 'conciliacao',
				children: [
					{
						path: 'relatorio',
						children: [
							{
								path: 'detalhado',
								canActivate: [AuthGuard],
								component: RelatorioDetalhadoListComponent
							},
							{
								path: 'detalhadodetalhes',
								canActivate: [AuthGuard],
								component: RelatorioDetalhadoDetalhesComponent
							}
						]
					}
				]
			},
			{
				path: 'dashboard',
				canActivate: [AuthGuard],
				component: DashboardComponent
			},
			{
				path: 'usuario',
				canActivate: [AuthGuard],
				component: UsuarioListComponent
			},
			{
				path: 'usuario/:id',
				canActivate: [AuthGuard],
				component: UsuarioEditComponent
			},
			{
				path: 'logDashboard',
				canActivate: [AuthGuard],
				component: LogDashboardComponent
			},
			{
				path: 'arquivoImportacaoLog',
				//canActivate: [AuthGuard],
				component: ArquivoImportacaoLogComponent
			},
			{
				path: 'logCotacaoFrete',
				canActivate: [AuthGuard],
				component: LogCotacaoFreteComponent
			},
			{
				path: 'faturamento/:id/detalhe',
				canActivate: [AuthGuard],
				component: UsuarioListComponent
			},
			{
				path: 'importacaoarquivo',
				canActivate: [AuthGuard],
				component: ImportacaoArquivoListComponent
			},
			{
				path: 'importacaoarquivo/:id',
				canActivate: [AuthGuard],
				component: ImportacaoArquivoEditComponent
			},
			{
				path: 'importacaoArquivoCritica/:id',
				canActivate: [AuthGuard],
				component: ImportacaoArquivoCriticaComponent
			},		
			{
				path: 'faturas',
				canActivate: [AuthGuard],
				component: FaturaListComponent
			},
			{
				path: 'faturas/:id',
				canActivate: [AuthGuard],
				component: FaturaEditComponent
			},
			{
				path: 'configuracaoCteTransportador',
				canActivate: [AuthGuard],
				component: ConfiguracaoCteTransportadorListComponent
			},
			{
				path: 'configuracaoCteTransportador/:id',
				canActivate: [AuthGuard],
				component: ConfiguracaoCteTransportadorEditComponent
			},
			{
				path: 'contratoTransportador',
				canActivate: [AuthGuard],
				component: ContratoTransportadorListComponent
			},
			{
				path: 'contratoTransportador/:id',
				canActivate: [AuthGuard],
				component: ContratoTransportadorEditComponent
			},
			{
				path: 'importacaoConfiguracao',
				canActivate: [AuthGuard],
				component: ImportacaoConfiguracaoListComponent
			},
			{
				path: 'importacaoConfiguracao/:id',
				canActivate: [AuthGuard],
				component: ImportacaoConfiguracaoEditComponent
			},
			{
				path: 'importacaoempresa',
				canActivate: [AuthGuard],
				component: EmpresaImportacaoListComponent
			},
			{
				path: 'regraEstoque',
				canActivate: [AuthGuard],
				component: RegraEstoqueListComponent
			},
			{
				path: 'entregaDevolucao/:id',
				canActivate: [AuthGuard],
				component: EntregaDevolucaoEditComponent
			},
			{
				path: 'entregaDevolucao',
				canActivate: [AuthGuard],
				component: EntregaDevolucaoListComponent
			},	
			{
				path: 'entregaOcorrencia/:id',
				canActivate: [AuthGuard],
				component: EntregaOcorrenciaEditComponent
			},
			{
				path: 'entregaOcorrencia',
				canActivate: [AuthGuard],
				component: EntregaOcorrenciaListComponent
			},
			{
				path: 'ocorrenciaEntrega',
				canActivate: [AuthGuard],
				component: OcorrenciaEntregaListComponent
			},	
			{
				path: 'empresaIntegracao/:id',
				canActivate: [AuthGuard],
				component: EmpresaIntegracaoEditComponent
			},
			{
				path: 'ocorrenciaEmpresa',
				canActivate: [AuthGuard],
				component: OcorrenciaEmpresaListComponent
			},
			{
				path: 'dowloadArquivoOcorrencia',
				canActivate: [AuthGuard],
				component: DownloadArquivoOcorrenciaEditComponent
			},
			{
				path: 'ocorrenciaArquivo',
				canActivate: [AuthGuard],
				component: OcorrenciaArquivoListComponent
			},	
			{
				path: 'empresaIntegracaoItemDetalhe',
				canActivate: [AuthGuard],
				component: EmpresaIntegracaoItemDetalheListComponent
			},
			{
				path: 'empresaIntegracaoItemDetalhe/:id',
				canActivate: [AuthGuard],
				component: EmpresaIntegracaoItemDetalheEditComponent
			},
			{
				path: 'empresaIntegracao',
				canActivate: [AuthGuard],
				component: EmpresaIntegracaoListComponent
			},
			{
				path: 'integracao/:id',
				canActivate: [AuthGuard],
				component: IntegracaoEditComponent
			},
			{
				path: 'integracao',
				canActivate: [AuthGuard],
				component: IntegracaoListComponent
			},
			{
				path: 'testeIntegracao',
				canActivate: [AuthGuard],
				component: TesteIntegracaoComponent
			},
			{
				path: 'gerarFatura',
				//canActivate: [AuthGuard],
				component: GeradorFaturaComponent
			},
			{
				path: 'entregaDevolucaoOcorrencia',
				component: EntregaDevolucaoOcorrenciaComponent
			},
			{
				path: 'entregaDevolucaoHistoricoJson',
				component: EntregaDevolucaoHistoricoJsonComponent
			},
			{
				path: 'entregaDevolucaoHistoricoXML',
				component: EntregaDevolucaoHistoricoXMLComponent
			},
			{
				path: 'empresaTransporte/:id',				
				component: EmpresaTransporteConfiguracaoEditComponent
			},
			{
				path: 'empresaTransporte',				
				component: EmpresaTransporteConfiguracaoListComponent
			},
			{
				path: 'atualizacaoTabelasCorreios',				
				component: AtualizacaoTabelasCorreiosListComponent
			},
			{
				path: 'agendamentoExpedicao',
				component: AgendamentoExpedicaoListComponent				
			},
			{
				path: 'agendamentoExpedicao/:id',
				component: AgendamentoExpedicaoEditComponent				
			},
			{
				path: 'agendamentoEntrega',
				component: AgendamentoEntregaListComponent				
			},
			{
				path: 'agendamentoRegra',
				component: AgendamentoRegraListComponent				
			},
			{
				path: 'agendamentoRegra/:id',
				component: AgendamentoRegraEditComponent				
			},
			{
				path: 'faturaConciliacaoIntegracao',
				component: FaturaConciliacaoIntegracaoComponent				
			},
			{
				path: 'pesquisaProduto',
				component: PesquisaProdutoComponent				
			}
		]
	},
	{
		path: 'login',
		canActivate: [NgxPermissionsGuard],
		loadChildren: './auth/auth.module#AuthModule',
		data: {
			permissions: {
				//except: 'ADMIN'
			}
		},
	},
	{
		path: '404',
		component: ErrorPageComponent
	},
	{
		path: 'error/:type',
		component: ErrorPageComponent
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class PagesRoutingModule {
}
