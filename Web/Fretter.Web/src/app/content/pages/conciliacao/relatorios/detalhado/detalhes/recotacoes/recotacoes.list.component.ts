import { array } from '@amcharts/amcharts4/core';
import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Input
} from '@angular/core';
import { t } from '@angular/core/src/render3';

import { MatPaginator, MatSnackBar, MatDialog, PageEvent } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import moment from 'moment';
import { combineLatest } from 'rxjs';
import { RelatorioDetalhadoFiltro } from '../../../../../../../core/filters/relatorioDetalhadoFiltro';
import { AlertService } from '../../../../../../../core/services/alert.service';
import { ConciliacaoService } from '../../../../../../../core/services/conciliacao.service';
import { ConfiguracaoCteTipoService } from '../../../../../../../core/services/configuracaoCteTipo.service';
import { JsonViewComponent } from '../../../../../components/logDashboard/logDashboard.jsonView/jsonView.component';


@Component({
	selector: 'm-relatorioDetalhado-detalhesRecotacoes',
	styleUrls: ['./recotacoes.list.component.scss'],
	templateUrl: './recotacoes.list.component.html'
})
export class RelatorioDetalhadoRecotacoesListComponent implements OnInit {

	dataSource: MatTableDataSource<any> = new MatTableDataSource(new Array<any>());	
	displayedColumns: string[] = ['valorCustoFrete', 'valorCustoAdicional', 'valorCustoReal', 'tabelaDescricao' , 'dataProcessamento', 'actions'];
	@Input() model: any;	
	viewLoading: boolean = false;
	configuracaoCteTipo: any[];
	composicaoCte: any[];
	composicaoRecotacao: any[];
	filtro: RelatorioDetalhadoFiltro = new RelatorioDetalhadoFiltro();

	paginatorLength: number = 0;	
	pageSize: number = 0;
	pageSelected: number = 20;	

	@ViewChild(MatSort) sort: MatSort;

	constructor(private _service: ConciliacaoService,
		private _serviceConfigCteTipo: ConfiguracaoCteTipoService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) {

	}
	ngOnInit() {
		this.filtro.PageSize = 50;
		this.filtro.PageSelected = 0;
		this.sort.direction = "desc";

		this.load();
	}

	load() {
		this.filtro.OrderByDirection = this.sort.direction;
		this.dataSource.sort = this.sort;
		this.dataSource.data = this.model.listRecotacoes;	
		this.paginatorLength = this.model.listRecotacoes.length;
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

	showObject(model){
		let obj = {
			json: model.jsonRetornoRecotacao
		};
		const dialogRef = this.dialog.open(JsonViewComponent, { data:{ obj } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
		});
	}
}


