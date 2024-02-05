import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Fatura } from "../../../../../core/models/fatura";
import { FaturaService } from "../../../../../core/services/fatura.service";
import { FaturaEditComponent } from '../edit/fatura.edit.component';
import { AlertService } from '../../../../../core/services/alert.service';
import { FaturaFiltro } from '../../../../../core/models/faturaFiltro';
import * as fs from 'file-saver';
import * as moment from 'moment';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { CanalService } from '../../../../../core/services/canal.service';
import { FaturaStatusService } from '../../../../../core/services/faturaStatus.service';
import { FaturaItemListComponent } from '../faturaItem/faturaItem.list.component';
import { FaturaConciliacaoIntegracaoComponent } from '../faturaConciliacaoIntegracao/faturaConciliacaoIntegracao.component';

@Component({
	selector: 'm-fatura',
	styleUrls: ['./fatura.list.component.scss'],
	templateUrl: './fatura.list.component.html'
})
export class FaturaListComponent implements OnInit {
	//List
	dataSource: MatTableDataSource<Fatura>;
	displayedColumns: string[] = ['id', 'transportador', 'dataCadastro', 'quantidadeEntregas',
		'valorCustoFrete', 'valorCustoReal', 'qtdItensDoccob', 'valorTotalDoccob', 'faturaStatusId', 'actions'];
	//Filtro
	filtro: FaturaFiltro;
	transportadores: any[];
	canais: any[];
	status: any[];
	viewLoading: boolean = false;
	periodoId: number = 5;

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
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(
		//List
		private _service: FaturaService, public dialog: MatDialog,
		private _alertService: AlertService, private faturaService: FaturaService,
		//Filtro
		private service: FaturaService,
		private transportadorService: TransportadorService,
		private canalService: CanalService,
		private statusService: FaturaStatusService,

		//Comum
		private cdr: ChangeDetectorRef
	) {

	}
	ngOnInit() {
		this.filtro = new FaturaFiltro();
		this.load(new FaturaFiltro());
	}

	load(filtro: FaturaFiltro) {
		this.capturaDatas(this.periodoId);

		this.transportadorService.getTransportadoresPorEmpresa().subscribe(data => {
			this.transportadores = data;
		});

		this.canalService.getCanaisPorEmpresa().subscribe(data => {
			this.canais = data;
		});

		this.statusService.get().subscribe(data => {
			this.status = data;
		});
		this.cdr.detectChanges();
	}

	// view(model: Fatura) {
	// 	model.visualizar = true;
	// 	const dialogRef = this.dialog.open(FaturaEditComponent, {data: { model}});
	// 	dialogRef.afterClosed().subscribe(res => {
	// 		if(!res)
	// 			return;
	// 		this.load(new FaturaFiltro());
	// 	});
	// }

	edit(model: Fatura) {
		model.visualizar = false;
		const dialogRef = this.dialog.open(FaturaEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			this.load(this.filtro);
		});
	}

	filtroTransportadorChange(event: any) {
		this.pesquisar();
	}

	filtroCanalChange(event: any) {
		// if(event !== undefined)
		// 	this.filtro.canalId = event.id;
		this.pesquisar();
	}

	filtroStatusChange(event: any) {
		//this.filtro.faturaStatusId = event.id;
		this.pesquisar();
	}

	filtroPeriodoChange(event: any) {
		if (event !== undefined)
			this.capturaDatas(event.id);
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
			case 11: // Personalizado
				dataInicio = moment().startOf('month').toDate();
				dataTermino = moment().toDate();
				break;
		}
		this.periodoId = id;
		this.filtro.dataCadastroStart = dataInicio.toISOString();
		this.filtro.dataCadastroEnd = dataTermino.toISOString();
		this.pesquisar();
	}

	downloadDemonstrativo() {
		this._service.downloadDemonstrativo(this.filtro).subscribe((response: any) => {
			let blob = new Blob([response], { type: response.type });
			fs.saveAs(blob, 'Fatura_Demonstrativo.xlsx');
			this._alertService.show("Sucesso.", "Demonstrativo baixado com sucesso.", 'success');
			this.viewLoading = false;
		}, error => {
			this.viewLoading = false;
		});
	}

	pesquisar() {
		this.viewLoading = true;
		this._service.getFilter(this.filtro).subscribe(result => {
			this.dataSource = new MatTableDataSource(result.data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
			this.viewLoading = false;
		}, error => {
			if (error) this._alertService.show("Error", "Houve um erro ao carregar relatório: " + error.error, 'error');
			else this._alertService.show("Error", "Houve um erro ao carregar relatório.", 'error');
			this.viewLoading = false;
		});
	}

	detalhamento(id: number) {
		const dialogRef = this.dialog.open(FaturaItemListComponent, { data: id, width: '600px' });
		dialogRef.afterClosed().subscribe(res => {
			this.load(this.filtro);
		});
	}
	exibirResultadoIntegracao(id: number) {
		const dialogRef = this.dialog.open(FaturaConciliacaoIntegracaoComponent, { data: id });
		dialogRef.afterClosed().subscribe();
	}
}

