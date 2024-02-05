import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { ImportacaoArquivo } from '../../../../../core/models/importacaoArquivo';
import { ImportacaoArquivoService } from "../../../../../core/services/importacaoArquivo.service";
import { ImportacaoArquivoEditComponent } from '../edit/importacaoArquivo.edit.component';
import { AlertService } from '../../../../../core/services/alert.service';
import { any } from '@amcharts/amcharts4/.internal/core/utils/Array';
import { ImportacaoArquivoFiltro } from '../../../../../core/models/Filters/importacaoArquivoFiltro';
import { ImportacaoArquivoCritica } from '../../../../../core/models/importacaoArquivoCritica';
import { ImportacaoArquivoCriticaComponent } from '../criticas/importacaoArquivo.critica.component';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { combineLatest } from 'rxjs';
import { ImportacaoArquivoStatusService } from '../../../../../core/services/importacaoArquivoStatus.service';
import { ImportacaoArquivoResumo } from '../../../../../core/models/importacaoArquivoResumo.model';
import moment from 'moment';

@Component({
	selector: 'm-importacaoarquivo',
	templateUrl: './importacaoArquivo.list.component.html',
	styleUrls: ['./importacaoArquivo.list.component.scss'],
})
export class ImportacaoArquivoListComponent implements OnInit{
	dataSource: MatTableDataSource<ImportacaoArquivo> = new MatTableDataSource(new Array<ImportacaoArquivo>());;
	displayedColumns: string[] = ['id', 'nome', /*'identificador',*/ 'importacaoArquivoStatusId', 'importacaoArquivoTipoId', 'dataCadastro', 'acoes'];
	filtro: ImportacaoArquivoFiltro = new ImportacaoArquivoFiltro();
	dadosCard: ImportacaoArquivoResumo = new ImportacaoArquivoResumo();

	transportadores: any[];
	importacaoArquivoStatus: any[];

	viewLoading: boolean = true;
	viewLoadingResumo: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;
	pageIndex: number = 0;

	

	constructor(
		private _service: ImportacaoArquivoService, 
		private _transportadorService: TransportadorService,
		private _importacaoArquivoStatusService: ImportacaoArquivoStatusService,
		public dialog: MatDialog, 
		private cdr: ChangeDetectorRef, 
		private _alertService : AlertService
	) {	}
	ngOnInit() {
		this.load();
	}

	load() {
		this.capturaDatas(this.filtro.periodoId);

		const transportadores$ = this._transportadorService.getTransportadoresPorEmpresa();
		const importacaoArquivoStatus$ = this._importacaoArquivoStatusService.get();
		const resumo$ = this._service.getResumo(this.filtro);

		combineLatest([transportadores$, importacaoArquivoStatus$, resumo$]).subscribe(([transportadores, importacaoArquivoStatus, resumo]) => {
			this.transportadores = transportadores;
			this.importacaoArquivoStatus = importacaoArquivoStatus;
			this.dadosCard = resumo;
			this.viewLoadingResumo = false;
		});
		
		this.pesquisar();
	}

	aplicarFiltro(){
		this.start = 0;
		this.size = 5;
		this.pageIndex = 0;
		this.resultsLength = 0;
		this.pesquisar();
		this.atualizarResumo();
	}
	
	pesquisar(){
		this.viewLoading = true;
		this._service.getFilter(this.filtro, this.start, this.size).subscribe(result => {
			this.dataSource.data = result.data;
			this.resultsLength = result.total;
			this.viewLoading = false;
			this.cdr.detectChanges();
		});
	}
	
	pageChange(event){		
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}

	novo() {
		var arquivo = new ImportacaoArquivo();
		const dialogRef = this.dialog.open(ImportacaoArquivoEditComponent, {data: { model: arquivo}});
		dialogRef.afterClosed().subscribe(res => {
			if(!res)
				return;
			this.load();
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	download(url: string , arquivo: string){
		this._service.download(url).subscribe((res: any)  =>{			
			let blob = new Blob([res]);
			let blobLink = window.URL.createObjectURL(blob);

			const link = document.createElement('a');
			link.href = blobLink;
			link.download = arquivo;
			link.click();
			window.URL.revokeObjectURL(url);
			link.remove();

		 	this._alertService.show("Sucesso.", "Arquivo baixado com sucesso.", 'success');
			this.viewLoading = false;
		}, error => {
			this.viewLoading = false;
		});
	}

	visualizarCriticas(criticas: Array<ImportacaoArquivoCritica>){
		const dialogRef = this.dialog.open(ImportacaoArquivoCriticaComponent, { data: { criticas } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		})
	}

	atualizarResumo() {
		this.viewLoadingResumo = true;
		this._service.getResumo(this.filtro).subscribe(result => {
			this.dadosCard = result;
			this.viewLoadingResumo = false;
			
        }, error => {
			this.viewLoadingResumo = false;
		});
    }

	filtroPeriodoChange(event: any) {
		if (event != undefined) {
			this.capturaDatas(event.id);
        }
	}

	filtroDataChange(event: any) {
		if (event != undefined){
			if(event.target.id == 'dataInicio'){
				this.filtro.startDataCadastro = moment(event.target.value).toDate().toISOString();
			}
			else{
				this.filtro.endDataCadastro =  moment(event.target.value).toDate().toISOString();
			}
			this.capturaDatas(11);
		}
	}

	filtroTransportadorChange(value) {
		if (!value)
			this.filtro.transportadorId = 0;
	}

	filtroStatusChange(value) {
		if (!value)
			this.filtro.importacaoArquivoStatusId = 0;
	}

	capturaDatas(id: number) {
		let dataInicio = new Date();
		let dataTermino = new Date();
		switch (id) {
			case 1: // Hoje
				dataInicio.setDate(dataInicio.getDate());
				break;
			case 2: // Ontem
				dataInicio = moment().subtract(1, 'days').startOf('day').toDate();
				dataTermino = moment().subtract(1, 'days').startOf('day').toDate();
				break;
			case 3: // Semana Atual
				dataInicio = moment().startOf('week').toDate();
				break;
			case 4: // Semana Passada
				dataInicio = moment().startOf('week').subtract(7, 'days').toDate();
				dataTermino = moment().startOf('week').subtract(1, 'days').toDate();
				break;
			case 5: // Mes Atual
				dataInicio = moment().startOf('month').toDate();
				dataTermino = moment().toDate();
				break;
			case 6: // Mes Passado
				dataInicio = moment().subtract(1, 'months').startOf('month').toDate();
				dataTermino = moment().startOf('month').subtract(1, 'days').toDate();
				break;
			case 7: // Trimestre
				dataInicio = moment().subtract(3, 'months').startOf('month').toDate();
				dataTermino = moment().toDate();
				break;
			case 8: // Semestre Atual
				dataInicio = moment().subtract(6, 'months').startOf('month').toDate();
				dataTermino = moment().toDate();
				break;
			case 9: // Ano Atual
				dataInicio = moment().startOf('year').toDate();
				dataTermino = moment().toDate();
				break;
			case 10: // 12 Meses
				dataInicio = moment().subtract(12, 'months').startOf('month').toDate();
				dataTermino = moment().toDate();
				break;
			case 11: // Custom
				dataInicio = moment(this.filtro.startDataCadastro).toDate();
				dataTermino = moment(this.filtro.endDataCadastro).toDate();;
				break;
		}
		this.filtro.periodoId = id;
		this.filtro.startDataCadastro = dataInicio.toISOString();
		this.filtro.endDataCadastro = dataTermino.toISOString();
	}
  }