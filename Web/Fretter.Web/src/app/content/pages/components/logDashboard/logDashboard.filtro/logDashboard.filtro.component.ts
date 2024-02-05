import { ChangeDetectionStrategy, Component, OnInit, AfterViewInit, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import * as objectPath from 'object-path';
import { LayoutConfigService } from '../../../../../core/services/layout-config.service';
import { SubheaderService } from '../../../../../core/services/layout/subheader.service';
import { DashBoardFiltro } from '../../../../../core/models/dashboadFiltro';
import { UsuarioService } from '../../../../../core/services/usuario.service';
import * as moment from 'moment';
import { ClienteService } from '../../../../../core/services/cliente.service';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { CanalService } from '../../../../../core/services/canal.service';
import { LogDashboardFiltro } from '../../../../../core/models/logDashboadFiltro';
import { LogDashboardService } from '../../../../../core/services/logDashboard.service';

@Component({
	selector: 'm-log-dashboard-filtro',
	templateUrl: './logDashboard.filtro.component.html',
	styleUrls: ['./logDashboard.filtro.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class LogDashboardFiltroComponent implements OnInit, AfterViewInit {

	filtro: LogDashboardFiltro = new LogDashboardFiltro();
	transportadores: any[];
	canais: any[];

	constructor(
		private service: LogDashboardService,

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
