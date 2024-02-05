import {
	Component,
	OnInit
} from '@angular/core';

import { MatDialog } from '@angular/material';
import { MatTableDataSource } from '@angular/material/table';
import { combineLatest } from 'rxjs';
import { AgendamentoRegra } from '../../../../../core/models/agendamentoRegra.model';
import { AgendamentoRegraFiltro } from '../../../../../core/models/agendamentoRegraFiltro.model';
import { AgendamentoRegraService } from '../../../../../core/services/agendamentoRegra.service';
import { AgendamentoRegraEditComponent } from '../edit/agendamentoRegra.edit.component';
import { CanalService } from '../../../../../core/services/canal.service';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { AlertService } from '../../../../../core/services/alert.service';

@Component({
	selector: 'm-agendamentoRegra',
	templateUrl: './agendamentoRegra.list.component.html'
})
export class AgendamentoRegraListComponent implements OnInit {
	dataSource: MatTableDataSource<AgendamentoRegra> = new MatTableDataSource(new Array<AgendamentoRegra>());
	displayedColumns: string[] = ['nome', 'filial', 'transportador', 'transportadorFilial', 'dataCadastro', 'actions'];

	filtro: AgendamentoRegraFiltro = new AgendamentoRegraFiltro();
	lstCanais: any[];
	lstTransportadores: any[];
	lstTransportadoresFiliais: any[];
	
	viewLoading: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;

	constructor(
		private _agendamentoRegraService: AgendamentoRegraService,
		public dialog: MatDialog,
		private _transportadorService: TransportadorService,
		private _canalService: CanalService,
		private _alertService: AlertService
	) { }

	ngOnInit() {
		const transportadores$ = this._transportadorService.getTransportadoresPorEmpresa();
		const canais$ = this._canalService.getCanaisPorEmpresa();

		combineLatest([transportadores$, canais$])
			.subscribe(([transportadores, canais]) => {
				this.lstTransportadores = transportadores;
				this.lstCanais = canais;
			}
			);

		this.load();
	}

	load() {
		this.pesquisar();
	}

	pesquisar() {
		this.viewLoading = true;
		this._agendamentoRegraService.getFilter(this.filtro, this.start, this.size).subscribe(result => {
			this.dataSource.data = result.data;
			this.resultsLength = result.total;
			this.viewLoading = false;
		});
	}

	pageChange(event) {
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
	}

	getTransportadoresCnpj(transportadorId: number) {
		this._transportadorService.getTransportadoresCnpj(transportadorId).subscribe(transportadoresCnpj => {
			this.lstTransportadoresFiliais = transportadoresCnpj;
		});

	}
	limparComboTransportadorFilial() {
		this.lstTransportadoresFiliais = [];
		this.filtro.transportadorCnpjId = null;
	}
	
	novo() {
		var agendamentoRegra = new AgendamentoRegra();
		const dialogRef = this.dialog.open(AgendamentoRegraEditComponent, { data: { model: agendamentoRegra } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: AgendamentoRegra) {
		const dialogRef = this.dialog.open(AgendamentoRegraEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: AgendamentoRegra) {
		this._alertService.confirmationMessage("",`Deseja realmente deletar a regra?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this._agendamentoRegraService.InativarRegra(model.id).subscribe(r=> {
					this._alertService.show("Sucesso","Regra deletada com sucesso.",'success');
					this.load();
				});
			}
		});
	}
}