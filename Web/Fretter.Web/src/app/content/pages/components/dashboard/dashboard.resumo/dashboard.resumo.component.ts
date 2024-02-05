import { ChangeDetectionStrategy, Component, OnInit, ChangeDetectorRef, AfterViewInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import * as objectPath from 'object-path';
import { AlertService } from '../../../../../core/services/alert.service';
import { DashboardService } from '../../../../../core/services/dashboard.service';
import { Subscription } from 'rxjs';
import { DashBoardFiltro } from '../../../../../core/models/dashboadFiltro';
import { ExcelService } from '../../../../../core/services/excel.service';
import { ConciliacaoService } from '../../../../../core/services/conciliacao.service';
import { RelatorioDetalhadoFiltro } from '../../../../../core/filters/relatorioDetalhadoFiltro';
import moment from 'moment';

@Component({
	selector: 'm-dashboard-resumo',
	templateUrl: './dashboard.resumo.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardResumoComponent implements OnInit, OnDestroy {
	viewLoading: boolean = false;
	viewLoadingExcel: boolean = false;
	public config: any;
	public subFiltro: Subscription;
	filtro: RelatorioDetalhadoFiltro = new RelatorioDetalhadoFiltro();
	public dados: any = { mediaGeral: 0, quantidadeTotal: 0, quantidadeMesAtual: 0, quantidadeMesPassado: 0 };
	constructor(private service: DashboardService,
		private conciliacaoService: ConciliacaoService,
		private excelService: ExcelService,
		private alertService: AlertService,
		private cdr: ChangeDetectorRef) {
	}

	ngOnInit(): void {
		this.subFiltro = this.service.onAtualizar.subscribe(data => {
			this.load(data);
		});
	}

	load(dashFiltro: DashBoardFiltro) {
		this.filtro.DataInicial = dashFiltro.dataInicio.toISOString();
		this.filtro.DataFinal = dashFiltro.dataTermino.toISOString();
		this.filtro.TransportadorId = dashFiltro.transportadorId;		
		localStorage.setItem('relatorioFiltro', JSON.stringify(this.filtro));
		this.service.getResumo(dashFiltro).subscribe(data => {

			if (data.length > 0)
				this.dados = data[0];

			this.cdr.detectChanges();
		});


		this.cdr.detectChanges();
	}

	ngOnDestroy(): void {
		if (this.subFiltro)
			this.subFiltro.unsubscribe();
	}

	exportarExcel_Dashboard(conciliacaoStatus, conciliacaoStatusId) {
		this.filtro = JSON.parse(localStorage.getItem('relatorioFiltro'));
		this.filtro.StatusId = conciliacaoStatusId;
		this.viewLoadingExcel = true;
		this.conciliacaoService.getRelatorioDetalhadoArquivo(this.filtro).subscribe(data => {			
			let excelData = data.map(object => ({ ...object }));
			this.excelService.generateExcel(`Relatorio_${conciliacaoStatus}_${moment().format('YYYY-MM-DDTHHmm')}`, excelData,
				{
					conciliacaoId: { titulo: 'Código Conciliacao' },
					transportador: { titulo: 'Transportador' },
					codigoEntrega: { titulo: 'Código Entrega' },
					codigoPedido: { titulo: 'Código Pedido' },
					dataEmissao: { titulo: 'Data Emissao', tipo: 'date' },
					entregaPeso: { titulo: 'Peso' },
					entregaAltura: { titulo: 'Altura' },
					entregaComprimento: { titulo: 'Comprimento' },
					entregaLargura: { titulo: 'Largura' },
					entregaValorDeclarado: { titulo: 'Vl. Declarado' },
					valorCustoFrete: { titulo: 'Custo Frete' },
					valorCustoAdicional: { titulo: 'Custo Adicional' },
					valorCustoReal: { titulo: 'Custo Real' },
					valorICMS: { titulo: 'Vl. ICMS' },
					valorGRIS: { titulo: 'Vl. GRIS' },
					valorADValorem: { titulo: 'Vl. ADValorem' },
					valorPedagio: { titulo: 'Vl. Pedagio' },
					valorFretePeso: { titulo: 'Vl. Frete Peso' },
					valorTaxaTRT: { titulo: 'Taxa TRT' },
					valorTaxaTDE: { titulo: 'Taxa TDE' },
					valorTaxaTDA: { titulo: 'Taxa TDA' },
					valorTaxaCTe: { titulo: 'Taxa CTe' },
					valorTaxaRisco: { titulo: 'Taxa Risco' },
					valorSuframa: { titulo: 'Suframa' },
					statusConciliacao: { titulo: 'Status' },
				});

			this.viewLoadingExcel = false;
			this.cdr.detectChanges();

		}, error => {
			if (error) this.alertService.show("Error", "Houve um erro ao fazer download relatório: " + error.error, 'error');
			else this.alertService.show("Error", "Houve um erro ao fazer download relatório.", 'error');

			this.viewLoadingExcel = false;
			this.cdr.detectChanges();
		});
	}
}
