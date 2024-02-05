import { array } from '@amcharts/amcharts4/core';
import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Input
} from '@angular/core';
import { t } from '@angular/core/src/render3';

import { MatDialog, PageEvent } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { combineLatest } from 'rxjs';
import { RelatorioDetalhadoFiltro } from '../../../../../../../core/filters/relatorioDetalhadoFiltro';
import { AlertService } from '../../../../../../../core/services/alert.service';
import { ConciliacaoService } from '../../../../../../../core/services/conciliacao.service';
import { ConfiguracaoCteTipoService } from '../../../../../../../core/services/configuracaoCteTipo.service';

@Component({
	selector: 'm-relatorioDetalhado-detalhesPesos',
	styleUrls: ['./pesos.list.component.scss'],
	templateUrl: './pesos.list.component.html'
})
export class RelatorioDetalhadoPesosListComponent implements OnInit {

	dataSource: MatTableDataSource<any> = new MatTableDataSource(new Array<any>());
	displayedColumns: string[] = ['chave', 'cte', 'recotacao', 'diferenca'];
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
		this.filtro.PageSize = 20;
		this.filtro.PageSelected = 0;
		this.sort.direction = "desc";
		this.load();
	}

	load() {
		this.filtro.OrderByDirection = this.sort.direction;
		const cteTipo$ = this._serviceConfigCteTipo.get();
		combineLatest([cteTipo$]).subscribe(([cteTipo]) => {
			this.configuracaoCteTipo = cteTipo;
			this.composicaoCte = this.model.listComposicaoCte;
			this.composicaoRecotacao = this.model.listComposicaoRecotacao;
			var objValor = [];
			var objPeso = [];

			if (this.composicaoCte !== undefined && this.composicaoRecotacao !== undefined) {
				this.composicaoCte.forEach(comp => {
					var diferenca = 0;
					var cteTipo = this.model.listComposicaoCte.filter(c => c.chave == comp.chave);
					var recotacao = this.model.listComposicaoRecotacao.filter(r => r.chave === comp.chave);

					if (cteTipo.length && recotacao.length) {
						recotacao[0].comparado = true;
						diferenca = comp.valor - recotacao[0].valor;
						if (diferenca < 0)
							diferenca = diferenca * -1;
					}
					//Peso
					if (comp.tipo == 2) {
						objPeso.push({
							'chave': comp.chave,
							'cte': comp.valor,
							'recotacao': recotacao.length ? recotacao[0].valor : 0,
							'diferenca': diferenca
						});
					}
				});
			}
			else {
				this.configuracaoCteTipo.forEach(tipo => {
					var diferenca = 0;
					var cte = this.model.listComposicaoCte.filter(c => c.chave == tipo.chave);
					var recotacao = this.model.listComposicaoRecotacao.filter(r => r.chave === tipo.chave);

					if (cte.length && recotacao.length) {
						diferenca = cte[0].valor - recotacao[0].valor;
						if (diferenca < 0)
							diferenca = diferenca * -1;

						if (cte.length && cte[0].tipo == 2)
							objPeso.push({
								'chave': tipo.chave,
								'cte': cte.length ? cte[0].valor : 0,
								'recotacao': recotacao.length ? recotacao[0].valor : 0,
								'diferenca': diferenca
							});
					}
				})
			}


			this.dataSource.data = objPeso;
			this.dataSource.sort = this.sort;
			this.paginatorLength = objPeso.length > 0 ? objPeso.filter(x => typeof x !== undefined).shift().qtdRegistrosQuery : 0;
		});
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
}


