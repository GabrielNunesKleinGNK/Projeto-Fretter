import {
	Component,
	OnInit
} from '@angular/core';

import { MatDialog } from '@angular/material';
import { MatTableDataSource } from '@angular/material/table';
import { combineLatest } from 'rxjs';
import { CanalService } from '../../../../../core/services/canal.service';
import { AgendamentoEntregaWizardComponent } from '../wizard/agendamentoEntrega.wizard.component';
import { AgendamentoEntregaService } from '../../../../../core/services/agendamentoEntrega.service';
import { AgendamentoEntregaFiltro } from '../../../../../core/models/agendamentoEntregaFiltro.model';
import { AgendamentoEntrega } from '../../../../../core/models/agendamentoEntrega';
import { MenuFretePeriodoService } from '../../../../../core/services/menuFretePeriodo.service';
import { MenuFretePeriodo } from '../../../../../core/models/menuFretePeriodo.model';

@Component({
	selector: 'm-agendamentoEntrega',
	templateUrl: './agendamentoEntrega.list.component.html',
	styleUrls: ['./agendamentoEntrega.list.component.scss']
})
export class AgendamentoEntregaListComponent implements OnInit {

	dataSource: MatTableDataSource<AgendamentoEntrega> = new MatTableDataSource(new Array<AgendamentoEntrega>());
	displayedColumns: string[] = ['id', 'cliente', 'data', 'periodo','qtdeItens', 'filial',  'actions'];
	filtro: AgendamentoEntregaFiltro = new AgendamentoEntregaFiltro();
	lstCanais: any[];
	periodos: Array<MenuFretePeriodo> = new Array<MenuFretePeriodo>();

	viewLoading: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;

	constructor(
		private _service : AgendamentoEntregaService,
		private _servicePeriodo: MenuFretePeriodoService,
		private _canalService: CanalService,
		public dialog: MatDialog
	) { }

	ngOnInit() {
		const canais$ = this._canalService.getCanaisPorEmpresa();
		const periodos$ = this._servicePeriodo.get();

		combineLatest([ canais$, periodos$])
			.subscribe(([ canais, periodos]) => {
				this.lstCanais = canais;
				this.periodos = periodos;

				this.load();
			});
	}

	load() {
		this.pesquisar();
	}

	pesquisar() {
		this.viewLoading = true;
		this._service.getFilter(this.filtro, this.start, this.size).subscribe(result => {
			let model  = result.data;
			model.forEach(m => {
				m.periodoSelecionado = this.periodos.filter(x => x.id == m.menuFreteRegiaoCepCapacidade.idPeriodo)[0]['dsNome']
			});

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
		var agendamentoExpedicao = new AgendamentoEntrega();
		const dialogRef = this.dialog.open(AgendamentoEntregaWizardComponent, { data: { model: agendamentoExpedicao } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: AgendamentoEntrega) {
		const dialogRef = this.dialog.open(AgendamentoEntregaWizardComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

}
