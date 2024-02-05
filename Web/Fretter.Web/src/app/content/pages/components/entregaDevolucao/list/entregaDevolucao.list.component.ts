import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import * as fs from 'file-saver';
import { EntregaDevolucao } from "../../../../../core/models/Fusion/entregaDevolucao.model";
import { EntregaDevolucaoService } from "../../../../../core/services/entregaDevolucao.service";
import { EntregaDevolucaoEditComponent } from '../edit/entregaDevolucao.edit.component';
import { AlertService } from '../../../../../core/services/alert.service';
import { EntregaDevolucaoFiltro } from '../../../../../core/models/Fusion/entregaDevolucaoFiltro';
import moment from 'moment';
import { EntregaDevolucaoStatusService } from '../../../../../core/services/entregaDevolucaoStatus.service';
import { EntregaDevolucaoOcorrenciaComponent } from '../entregaDevolucao.ocorrencia/entregaDevolucao.ocorrencia.component';
import { EntregaDevolucaoOcorrencia } from '../../../../../core/models/Fusion/entregaDevolucaoOcorrencia.model';

@Component({
	selector: 'm-entregaDevolucao',
	templateUrl: './entregaDevolucao.list.component.html',
	styleUrls: ['./entregaDevolucao.list.component.scss']
})
export class EntregaDevolucaoListComponent implements OnInit {
	dataSource: MatTableDataSource<EntregaDevolucao>;
	displayedColumns: string[] = ['id', 'entrega', 'codigoColeta', 'codigoRastreio', 'ultimaOcorrencia', 'dataOcorrencia', 'validade', 'inclusao', 'entregaDevolucaoStatus', 'actions'];
	filtro: EntregaDevolucaoFiltro;
	lstStatus: any[];
	lstGridParaDownload: EntregaDevolucao[];
	viewLoading: boolean = false;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	constructor(
		private _service: EntregaDevolucaoService,
		private _serviceStatus: EntregaDevolucaoStatusService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) {
	}

	ngOnInit() {
		this.filtro = new EntregaDevolucaoFiltro();
		this.load();
		this.pesquisar();
	}

	load() {
		this.capturaDatas(this.filtro.periodoId);

		this._serviceStatus.get().subscribe(data => {
			this.lstStatus = data;
		});
	}

	edit(model: EntregaDevolucao) {
		const dialogRef = this.dialog.open(EntregaDevolucaoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	ocorrencias(model: EntregaDevolucaoOcorrencia, entregaDevolucaoId: Number) {
		const dialogRef = this.dialog.open(EntregaDevolucaoOcorrenciaComponent, { data: { model, entregaDevolucaoId } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	filtroPeriodoChange(event: any) {
		if (event !== undefined)
			this.capturaDatas(event.id);
	}

	copiarCodigo(model) {
		if (model.codigoColeta) {
			const selBox = document.createElement('textarea');
			selBox.style.position = 'absolute';
			selBox.style.left = '0';
			selBox.style.top = '0';
			selBox.style.opacity = '0';
			selBox.value = model.codigoColeta;
			selBox.setAttribute('readonly', '');
			document.body.appendChild(selBox);
			selBox.focus();
			selBox.select();
			document.execCommand('copy');
			document.body.removeChild(selBox);
		}
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
				dataTermino = moment().subtract(1, 'days').startOf('day').endOf('day').toDate();
				break;
			case 3: // Semana Atual
				dataInicio = moment().startOf('week').endOf('day').toDate();
				break;
			case 4: // Semana Passada
				dataInicio = moment().startOf('week').subtract(7, 'days').toDate();
				dataTermino = moment().startOf('week').endOf('day').subtract(1, 'days').toDate();
				break;
			case 5: // Mes Atual
				dataInicio = moment().startOf('month').toDate();
				dataTermino = moment().endOf('day').endOf('day').toDate();
				break;
			case 6: // Mes Passado
				dataInicio = moment().subtract(1, 'months').startOf('month').startOf('day').toDate();
				dataTermino = moment().startOf('month').subtract(1, 'days').endOf('day').toDate();
				break;
			case 7: // Trimestre
				dataInicio = moment().subtract(3, 'months').startOf('month').toDate();
				dataTermino = moment().endOf('day').toDate();
				break;
			case 8: // Semestre Atual
				dataInicio = moment().subtract(6, 'months').startOf('month').toDate();
				dataTermino = moment().endOf('day').toDate();
				break;
			case 9: // Ano Atual
				dataInicio = moment().startOf('year').toDate();
				dataTermino = moment().endOf('day').toDate();
				break;
			case 10: // 12 Meses
				dataInicio = moment().subtract(12, 'months').startOf('month').toDate();
				dataTermino = moment().endOf('day').toDate();
				break;
		}
		this.filtro.periodoId = id;
		this.filtro.dataInicio = dataInicio;
		this.filtro.dataTermino = dataTermino;
	}

	pesquisar() {
		this.viewLoading = true;
		this._service.GetEntregasDevolucoes(this.filtro).subscribe(data => {
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
			this.lstGridParaDownload = data;
			this.viewLoading = false;
		}, error => {
			this._alertService.show("Atenção.", "Falha ao Buscar os Registros.", 'warning');
			this.viewLoading = false;
		})
	}

	download() {
		if (this.lstGridParaDownload !== undefined && this.lstGridParaDownload.length > 0) {
			this._service.Download(this.lstGridParaDownload).subscribe((response: any) => {
				let blob = new Blob([response], { type: response.type });
				fs.saveAs(blob, 'Entrega_Reversa.xlsx');
				this._alertService.show("Sucesso.", "Dados baixados com sucesso.", 'success');
				this.viewLoading = false;
			},
				error => {
					this._alertService.show("Atenção.", "Falha ao efetuar download dos dados.", 'warning');
					this.viewLoading = false;
				})
		}
		else {
			this._alertService.show("Atenção.", "Não existem dados a serem baixados.", 'warning');
		}

	}
}
