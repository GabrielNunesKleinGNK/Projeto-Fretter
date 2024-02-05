import { ChangeDetectionStrategy, Component, OnInit, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import * as moment from 'moment';
import { ArquivoImportacaoLogFiltro } from '../../../../../core/models/arquivoImportacaoLogFiltro';
import { ArquivoImportacaoLogService } from '../../../../../core/services/arquivoImportacaoLog.service';

@Component({
	selector: 'm-arquivo-importacao-log-filtro',
	templateUrl: './arquivoImportacaoLog.filtro.component.html',
	styleUrls: ['./arquivoImportacaoLog.filtro.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class ArquivoImportacaoLogFiltroComponent implements OnInit, AfterViewInit {

	filtro: ArquivoImportacaoLogFiltro = new ArquivoImportacaoLogFiltro();
	transportadores: any[];
	canais: any[];

	constructor(
		private service: ArquivoImportacaoLogService,

		private cdr: ChangeDetectorRef) {
	}

	ngOnInit(): void {
	}

	ngAfterViewInit(): void {
		this.load();
	}

	load() {
		this.capturaDatas(this.filtro.periodoId);

		
	}

	filtroPeriodoChange(event: any) {
		if (event != undefined)
			this.capturaDatas(event.id);
	}

	capturaDatas(id: number) {
		let dataInicio = new Date();
		let dataTermino = new Date();
		switch (id) {
			case 1: // Hoje
				dataInicio.setDate(dataInicio.getDate());
				dataTermino = moment().toDate();
				break;
			case 2: // Ontem
				dataInicio = moment().subtract(1, 'days')
				.startOf('day').set({hour:0,minute:0,second:0,millisecond:0}).toDate();
				
				dataTermino = moment().subtract(1, 'days')
				.startOf('day').set({hour:23,minute:59,second:59,millisecond:0}).toDate();
				break;
			case 3: // Semana Atual
				dataInicio = moment().startOf('week').set({hour:0,minute:0,second:0,millisecond:0}).toDate();
				dataTermino = moment().toDate();
				break;
			case 4: // Semana Passada
				dataInicio = moment().startOf('week').subtract(7, 'days')
				.set({hour:0,minute:0,second:0,millisecond:0}).toDate();
				
				dataTermino = moment().startOf('week').subtract(1, 'days')
				.set({hour:23,minute:59,second:59,millisecond:0}).toDate();
				break;
			case 5: // Mes Atual
				dataInicio = moment().startOf('month')
				.set({hour:0,minute:0,second:0,millisecond:0}).toDate();
				dataTermino = moment().toDate();
				break;
			case 6: // Mes Passado
				dataInicio = moment().subtract(1, 'months').startOf('month')
				.set({hour:0,minute:0,second:0,millisecond:0}).toDate();

				dataTermino = moment().startOf('month').subtract(1, 'days').toDate();
				break;
			case 7: // Trimestre
				dataInicio = moment().subtract(3, 'months').startOf('month')
				.set({hour:0,minute:0,second:0,millisecond:0}).toDate();

				dataTermino = moment().toDate();
				break;
			case 8: // Semestre Atual
				dataInicio = moment().subtract(6, 'months').startOf('month').toDate();
				dataTermino = moment().toDate();
				break;
			case 9: // Ano Atual
				dataInicio = moment().startOf('year').set({hour:0,minute:0,second:0,millisecond:0}).toDate();
				dataTermino = moment().toDate();
				break;
			case 10: // 12 Meses
				dataInicio = moment().subtract(12, 'months').startOf('month')
				.set({hour:0,minute:0,second:0,millisecond:0}).toDate();
				dataTermino = moment().toDate();
				break;
		}
		
		
		this.filtro.periodoId = id;
		this.filtro.dataInicio = dataInicio;
		this.filtro.dataTermino = dataTermino;
		this.pesquisar();
	}

	pesquisar() {
		this.service.onPesquisar(this.filtro);
	}
}
