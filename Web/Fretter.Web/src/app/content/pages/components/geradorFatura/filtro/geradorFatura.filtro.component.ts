import { ChangeDetectionStrategy, Component, OnInit, AfterViewInit, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
import { MatDialog } from '@angular/material';
import { FaturaPeriodoFiltro } from '../../../../../core/models/faturaPeriodoFiltro';
import { ImportacaoFatura } from '../../../../../core/models/importacaoFatura';
import { TransportadorService } from "../../../../../core/services/transportador.service";
import { AlertService } from "../../../../../core/services/alert.service";
import { GeradorFaturaEditComponent } from '../edit/geradorFatura.edit.component';

import { GeradorFaturaService } from '../../../../../core/services/geradorFatura.service';

@Component({
	selector: 'm-gerador-fatura-filtro',
	templateUrl: './geradorFatura.filtro.component.html',
	styleUrls: ['./geradorFatura.filtro.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class GeradorFaturaFiltroComponent implements OnInit, AfterViewInit {

	filtro: FaturaPeriodoFiltro = new FaturaPeriodoFiltro();
	transportadores: any[];
	canais: any[];
	entregas: any[];
	viewLoading: boolean = false;
	tipoProcesso: number = 0; //1 pesquisa 2 doccob
	constructor(
		private service: GeradorFaturaService,
		private transportadorService: TransportadorService,
		private _alertService: AlertService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef) {
	}

	ngOnInit(): void {
	}

	ngAfterViewInit(): void {
		this.load();
	}

	load() {
		this.capturaDatas(this.filtro.periodoId);
		this.transportadorService.getTransportadoresPorEmpresa().subscribe(data => {
			this.transportadores = data;
		});
	}

	novo() {
		var arquivo = new ImportacaoFatura();
		const dialogRef = this.dialog.open(GeradorFaturaEditComponent, { data: { model: arquivo } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res)
				return;
			this.load();
		});
	}

	filtroPeriodoChange(event: any) {
		if (event != undefined)
			this.capturaDatas(event.id);
	}

	capturaDatas(id: number) {
		this.filtro.periodoId = id;
		this.filtro.dataInicio = moment().subtract(1, 'days').toDate();
		this.filtro.dataTermino = moment().toDate();
	}

	validaTipoProcesso(id: number) {
		this.tipoProcesso = id;
		if (this.tipoProcesso == 2)
			this.novo();
	}

	parseDate(dateString: string): Date {
		if (dateString) {
			return new Date(dateString);
		}
		return null;
	}
	pesquisar() {
		this.service.getEntregaPeriodo(this.filtro).subscribe(res => {
			this.entregas = res as any[];
			if (this.entregas == undefined || this.entregas.length == 0)
				this._alertService.show("Atenção.", "Não foi encontrada nenhuma entrega para esse período.", 'warning');
			this.service.onCarregar(this.entregas);
			this.service.onCarregarLoad(false);
			this.service.onCarregarTipo(this.tipoProcesso);
		}, error => {
			this._alertService.show("Atenção.", "Ocorreu um erro ao buscar as entregas.", 'error');
		}, () => {
			this.viewLoading = false;
		});
	}
}
