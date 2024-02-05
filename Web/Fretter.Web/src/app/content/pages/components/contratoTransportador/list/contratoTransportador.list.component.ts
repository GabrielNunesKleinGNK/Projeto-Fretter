import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertService } from '../../../../../core/services/alert.service';
import { ContratoTransportadorEditComponent } from '../edit/contratoTransportador.edit.component';
import { ContratoTransportadorService } from '../../../../../core/services/contratoTransportador.service';
import { ContratoTransportador } from '../../../../../core/models/contratoTransportador';
import { ContratoTransportadorFiltro } from '../../../../../core/models/Filters/contratoTransportadorFiltro';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { Transportador } from '../../../../../core/models/transportador.model';
import { ContratoTransportadorHistoricoListComponent } from '../historico/list/contratoTransportadorHistorico.list.component';

@Component({
	selector: 'm-contratoTransportador',
	styleUrls: ['./contratoTransportador.list.component.scss'],
	templateUrl: './contratoTransportador.list.component.html'
})
export class ContratoTransportadorListComponent implements OnInit {

	dataSource: MatTableDataSource<ContratoTransportador> = new MatTableDataSource(new Array<ContratoTransportador>());
	displayedColumns: string[] = ['id', 'descricao', 'transportador', 'cnpj', 'vigenciaInicial', 'vigenciaFinal', 'usuarioAlteracao', 'dataAlteracao', 'actions'];
	filtro: ContratoTransportadorFiltro = new ContratoTransportadorFiltro();
	transportadores: Array<Transportador> = new Array<Transportador>();


	viewLoading: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: ContratoTransportadorService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService,
		private _transportadorService: TransportadorService) {

	}
	ngOnInit() {
		this.load();
	}

	load() {
		this._transportadorService.getTransportadoresPorEmpresa().subscribe(result => {
			this.transportadores = result;
		});
		this.pesquisar();
	}

	pesquisar() {
		this.viewLoading = true;
		this._service.getFilter(this.filtro, this.start, this.size).subscribe(result => {
			this.dataSource.data = result.data;
			this.resultsLength = result.total;
			this.viewLoading = false;
		});
	}

	pageChange(event) {
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}


	novo() {
		var contratoTransportador = new ContratoTransportador();
		const dialogRef = this.dialog.open(ContratoTransportadorEditComponent, { data: { model: contratoTransportador } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	verHistorico(model: ContratoTransportador) {
		const dialogRef = this.dialog.open(ContratoTransportadorHistoricoListComponent, { data: { contratoTransportadorId: model.id }, minWidth: '100vh' });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: ContratoTransportador) {
		this._service.getById(model.id).subscribe(res => {
			const dialogRef = this.dialog.open(ContratoTransportadorEditComponent, { data: { model: res } });
			dialogRef.afterClosed().subscribe(res => {
				if (!res) {
					return;
				}
				this.load();
			});
		});
	}

	delete(model: ContratoTransportador) {
		this._alertService.confirmationMessage("", `Deseja realmente deletar o contrato "${model.id}"?`, 'Confirmar', 'Cancelar').then((result) => {
			if (result.value) {
				this._service.delete(model.id).subscribe(r => {
					this._alertService.show("Sucesso", "Contrato deletado com sucesso.", 'success');
					this.load();
				});
			}
		});
	}
}

