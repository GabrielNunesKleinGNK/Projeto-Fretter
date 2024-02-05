import {
	Component,
	OnInit
} from '@angular/core';

import { MatDialog } from '@angular/material';
import { MatTableDataSource } from '@angular/material/table';
import { combineLatest } from 'rxjs';
import { AgendamentoExpedicao } from '../../../../../core/models/agendamentoExpedicao.model';
import { AgendamentoExpedicaoFiltro } from '../../../../../core/models/agendamentoExpedicaoFiltro.model';
import { AgendamentoExpedicaoService } from '../../../../../core/services/agendamentoExpedicao.service';
import { CanalService } from '../../../../../core/services/canal.service';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { AgendamentoExpedicaoEditComponent } from '../../../agendamento/expedicao/edit/agendamentoExpedicao.edit.component';

@Component({
	selector: 'm-agendamentoExpedicao',
	templateUrl: './agendamentoExpedicao.list.component.html'
})
export class AgendamentoExpedicaoListComponent implements OnInit {

	dataSource: MatTableDataSource<AgendamentoExpedicao> = new MatTableDataSource(new Array<AgendamentoExpedicao>());
	displayedColumns: string[] = ['filial', 'transportador', 'transportadorFilial', 'geraNotifis', 'prazoMinimo', 'dataCadastro', 'actions'];
	filtro: AgendamentoExpedicaoFiltro = new AgendamentoExpedicaoFiltro();
	lstCanais: any[];
	lstTransportadores: any[];
	lstTransportadoresFiliais: any[];

	viewLoading: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;

	constructor(
		private _agendamentoExpedicaoService: AgendamentoExpedicaoService,
		private _transportadorService: TransportadorService,
		private _canalService: CanalService,
		public dialog: MatDialog
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
		this._agendamentoExpedicaoService.getFilter(this.filtro, this.start, this.size).subscribe(result => {
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
		var agendamentoExpedicao = new AgendamentoExpedicao();
		const dialogRef = this.dialog.open(AgendamentoExpedicaoEditComponent, { data: { model: agendamentoExpedicao } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: AgendamentoExpedicao) {
		const dialogRef = this.dialog.open(AgendamentoExpedicaoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

}
