import { any } from '@amcharts/amcharts4/.internal/core/utils/Array';
import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';
import { T } from '@angular/core/src/render3';

import { MatPaginator, MatSnackBar, MatDialog, PageEvent, MatCheckbox } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import moment from 'moment';
import { combineLatest } from 'rxjs';
import { RelatorioDetalhadoFiltro } from '../../../../../../core/filters/relatorioDetalhadoFiltro';
import { RelatorioDetalhadoExport } from '../../../../../../core/modelExport/relatorioDetalhado.export';
import { AlertService } from '../../../../../../core/services/alert.service';
import { CanalService } from '../../../../../../core/services/canal.service';
import { ConciliacaoService } from '../../../../../../core/services/conciliacao.service';
import { ExcelService } from '../../../../../../core/services/excel.service';
import { FaturaService } from '../../../../../../core/services/fatura.service';
import { TransportadorService } from '../../../../../../core/services/transportador.service';
import { RelatorioDetalhadoDetalhesComponent } from '../detalhes/relatorioDetalhado.detalhes.component';

@Component({
	selector: 'm-relatorioDetalhado',
	styleUrls: ['./relatorioDetalhado.list.component.scss'],
	templateUrl: './relatorioDetalhado.list.component.html'
})
export class RelatorioDetalhadoListComponent implements OnInit {

	dataSource: MatTableDataSource<any> = new MatTableDataSource(new Array<any>());
	displayedColumns: string[] = ['codigoConciliacao', 'transportador', 'tipoCobranca', 'codigoPedido', 'divergenciaPeso', 'divergenciaTarifa', 'valorCustoFrete', 'valorCustoAdicional', 'valorCustoReal', 'dataEmissao', 'status', 'acoes', 'selecionarTudo'];
	filtro: RelatorioDetalhadoFiltro = new RelatorioDetalhadoFiltro();
	viewLoading: boolean = false;
	viewLoadingExcel: boolean = false;
	viewLoadingRecalculoFrete: boolean = false;
	desabilitarBotaoRecalculo: boolean = true;
	todosSelecionado: boolean = false;
	conciliacaoStatus: any[];
	transportadores: any[];
	faturas: any[];

	paginatorLength: number = 0;
	pageSize: number = 0;
	pageSelected: number = 0;

	periodoId: number = 1;
	periodos: any[] = [
		{ id: 1, nome: "Hoje" },
		{ id: 2, nome: "Ontem" },
		{ id: 3, nome: "Semana Atual" },
		{ id: 4, nome: "Semana Passada" },
		{ id: 5, nome: "Mes Atual" },
		{ id: 6, nome: "Mes Passado" },
		{ id: 7, nome: "Trimestre" },
		{ id: 8, nome: "Semestre" },
		{ id: 9, nome: "Ano Atual" },
		{ id: 10, nome: "12 Meses" },
		{ id: 11, nome: "Custom" },
	];

	@ViewChild(MatSort) sort: MatSort;

	constructor(
		private _service: ConciliacaoService,
		private _faturaService: FaturaService,
		private _transportadorService: TransportadorService,
		private _excelService: ExcelService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) { }

	ngOnInit() {
		const transportadores$ = this._transportadorService.getTransportadoresPorEmpresa();
		const conciliacaoStatus$ = this._service.getStatus();
		const faturas$ = this._faturaService.getFaturasDaEmpresa(null);

		combineLatest([conciliacaoStatus$, transportadores$, faturas$]).subscribe(([conciliacaoStatus, transportadores, faturas]) => {
			this.conciliacaoStatus = conciliacaoStatus;
			this.transportadores = transportadores;
			this.faturas = faturas.data.filter(t => t.faturaPeriodoId > 0).map((i) => { i.faturaFormatada = moment(i.faturaPeriodo.dataInicio).format("DD/MM/YYYY") + " - " + moment(i.faturaPeriodo.dataFim).format("DD/MM/YYYY"); return i; });;
		});

		let dataInicio = moment();
		let dataTermino = moment();
		dataInicio = moment().startOf('day');
		this.filtro.DataInicial = dataInicio.toISOString();
		this.filtro.DataFinal = dataTermino.toISOString();
		this.filtro.PageSize = 20;
		this.filtro.PageSelected = 0;
		this.sort.direction = "desc";
		this.load();
	}

	load() {
		this.viewLoading = true;
		this.viewLoadingRecalculoFrete = true;
		this.filtro.OrderByDirection = this.sort.direction;
		this._service.getRelatorioDetalhado(this.filtro).subscribe(data => {
			this.dataSource.data = data;
			this.dataSource.sort = this.sort;
			this.viewLoading = false;
			this.viewLoadingRecalculoFrete = false;
			this.cdr.detectChanges();
			this.paginatorLength = data.length > 0 ? data.filter(x => typeof x !== undefined).shift().qtdRegistrosQuery : 0;

			if(this.todosSelecionado){
				this.dataSource.data.filter(x => !x.desabilitaEnvioRecalcular).forEach(function(item){ 
					item.selecionado = true;
				});
			}
		}, error => {
			if (error) this._alertService.show("Error", "Houve um erro ao carregar relatório: " + error.error.errors[0].message, 'error');
			else this._alertService.show("Error", "Houve um erro ao carregar relatório.", 'error');
			this.viewLoadingRecalculoFrete = false;
			this.viewLoading = false;
		});
	}

	limpar() {
		this.periodoId = 1;
		this.filtro = new RelatorioDetalhadoFiltro();
		this.filtro.PageSize = 20;
		this.filtro.PageSelected = 0;
		let dataInicio = moment();
		let dataTermino = moment();
		dataInicio = moment().startOf('day');
		this.filtro.DataInicial = dataInicio.toISOString();
		this.filtro.DataFinal = dataTermino.toISOString();
	}

	pesquisar() {
		this.filtro.PageSelected = 0;
		this.todosSelecionado = false
		this.load();
	}

	getTextoPeriodoSelecionado() {
		let format = 'DD/MM/YYYY HH:mm'
		return `${moment(this.filtro.DataInicial).format(format)} à ${moment(this.filtro.DataFinal).format(format)}`;
	}

	periodoChanged(value) {
		if (!value) {
			this.filtro.DataInicial = null;
			this.filtro.DataFinal = null;
		}
		else this.atualizarDatas();
	}

	filtroTransportadorChange(value) {
		if (!value)
			this.filtro.TransportadorId = 0;
	}

	filtroStatusChange(value) {
		if (!value)
			this.filtro.StatusId = 0;
	}

	atualizarDatas() {
		let dataInicio = moment();
		let dataTermino = moment();
		switch (this.periodoId) {
			case 1: // Hoje
				dataInicio = moment().startOf('day');
				break;
			case 2: // Ontem
				dataInicio = moment().subtract(1, 'days').startOf('day');
				dataTermino = moment().subtract(1, 'days').endOf('day');
				break;
			case 3: // Semana Atual
				dataInicio = moment().startOf('week');
				break;
			case 4: // Semana Passada
				dataInicio = moment().startOf('week').subtract(7, 'days');
				dataTermino = moment().startOf('week').subtract(1, 'days').endOf('day');
				break;
			case 5: // Mes Atual
				dataInicio = moment().startOf('month');
				dataTermino = moment();
				break;
			case 6: // Mes Passado
				dataInicio = moment().subtract(1, 'months').startOf('month');
				dataTermino = moment().startOf('month').subtract(1, 'days').endOf('day');
				break;
			case 7: // Trimestre
				dataInicio = moment().subtract(3, 'months').startOf('month');
				dataTermino = moment();
				break;
			case 8: // Semestre Atual
				dataInicio = moment().subtract(6, 'months').startOf('month');
				dataTermino = moment();
				break;
			case 9: // Ano Atual
				dataInicio = moment().startOf('year');
				dataTermino = moment();
				break;
			case 10: // 12 Meses
				dataInicio = moment().subtract(12, 'months').startOf('month');
				dataTermino = moment();
				break;
		}
		this.filtro.DataInicial = dataInicio.toISOString();
		this.filtro.DataFinal = dataTermino.toISOString();
	}

	handleSortChange(sort: MatSort): void {
		if (sort.active && sort.direction) {
			this.load();
		}
	}

	handlePageChange(pe: PageEvent): void {
		this.filtro.PageSize = pe.pageSize;
		this.filtro.PageSelected = pe.pageIndex;
		this.load();
	}

	exportarExcel() {
		this.viewLoadingExcel = true;
		this.filtro.OrderByDirection = this.sort.direction;
		this._service.getRelatorioDetalhadoArquivo(this.filtro).subscribe(data => {			
			let excelData = data.map(object => ({ ...object }));
			this._excelService.generateExcel(`Relatorio_Detalhado_${moment().format('YYYY-MM-DDTHHmm')}`, excelData,
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
			if (error) this._alertService.show("Error", "Houve um erro ao fazer download relatório: " + error.error, 'error');
			else this._alertService.show("Error", "Houve um erro ao fazer download relatório.", 'error');

			this.viewLoadingExcel = false;
			this.cdr.detectChanges();
		});
	}

	edit(model: any) {
		const dialogRef = this.dialog.open(RelatorioDetalhadoDetalhesComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if(dialogRef.componentInstance.loadRecalculo){
				this.load();
			}
		});
	}

	selecionarTodos(){
		let selecionado = !this.todosSelecionado
		this.todosSelecionado = selecionado;

		this.dataSource.data.filter(x => !x.desabilitaEnvioRecalcular).forEach(function(item){ 
			item.selecionado = selecionado;
		});

		this.desabilitarBotaoRecalculo = !this.dataSource.data.some(item => item.selecionado == true)
			
	}

	selecionarItem(){
		if(this.dataSource.data.filter(x => !x.desabilitaEnvioRecalcular).some(x => !x.selecionado) && this.todosSelecionado){
			this.todosSelecionado = false
		}
		
		this.desabilitarBotaoRecalculo = !this.dataSource.data.some(item => item.selecionado)
	}

	enviarRecalculoFrete(){
		let ids: number[] = [];
	
		this._alertService.confirmationMessage("",`Este recálculo utilizará as tabelas vigentes atuais, tem certeza que deseja seguir?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this.viewLoadingRecalculoFrete = true;

				if (this.todosSelecionado){
					this.filtro.ListSize = this.paginatorLength;
					this._service.postEnviaConciliacaoRecalculoFreteMassivo(this.filtro).subscribe(res => {
						this._alertService.show("Sucesso.", "Itens enviados para recálculo.", 'success');
						this.viewLoadingRecalculoFrete = false;
						this.load();
						this.todosSelecionado = false;
						this.desabilitarBotaoRecalculo = true;
					}, error => {			
						if (error.errors){
							if (error.errors.length > 0) {
								let errorMessage = error.errors[0].message || "Erro ao enviar para recálculo";
								this._alertService.show("Erro.", errorMessage, 'error');
							}
						}
			
						this.viewLoadingRecalculoFrete = false;
					});
				} else {
					let listSelecionado = this.dataSource.data.filter(x => x.selecionado)
		
					listSelecionado.forEach(x => ids.push(x.codigoConciliacao));
			
					this._service.postEnviaConciliacaoRecalculoFrete(ids).subscribe(res => {
						this._alertService.show("Sucesso.", "Itens enviados para recálculo.", 'success');
						this.viewLoadingRecalculoFrete = false;
						this.load();
						this.todosSelecionado = false;
						this.desabilitarBotaoRecalculo = true;
					}, error => {			
						if (error.errors){
							if (error.errors.length > 0) {
								let errorMessage = error.errors[0].message || "Erro ao enviar para recálculo";
								this._alertService.show("Erro.", errorMessage, 'error');
							}
						}
			
						this.viewLoadingRecalculoFrete = false;
					});
				}
			}
		});
	}
}