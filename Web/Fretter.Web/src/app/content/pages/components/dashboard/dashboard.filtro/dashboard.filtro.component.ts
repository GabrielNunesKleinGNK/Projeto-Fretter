import { ChangeDetectionStrategy, Component, OnInit, AfterViewInit, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import * as objectPath from 'object-path';
import { LayoutConfigService } from '../../../../../core/services/layout-config.service';
import { SubheaderService } from '../../../../../core/services/layout/subheader.service';
import { DashboardService } from '../../../../../core/services/dashboard.service';
import { DashBoardFiltro } from '../../../../../core/models/dashboadFiltro';
import { UsuarioService } from '../../../../../core/services/usuario.service';
import * as moment from 'moment';
import { ClienteService } from '../../../../../core/services/cliente.service';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { CanalService } from '../../../../../core/services/canal.service';

@Component({
	selector: 'm-dashboard-filtro',
	templateUrl: './dashboard.filtro.component.html',
	styleUrls: ['./dashboard.filtro.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardFiltroComponent implements OnInit, AfterViewInit {

	filtro: DashBoardFiltro = new DashBoardFiltro();
	transportadores: any[];
	canais: any[];

	constructor(
		private service: DashboardService,
		private transportadorService: TransportadorService,
		private canalService: CanalService,

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

		this.canalService.getCanaisPorEmpresa().subscribe(data => {
			this.canais = data;
		});
	}

	filtroPeriodoChange(event: any) {
		if (event != undefined)
			this.capturaDatas(event.id);
	}

filtroDataChange(event: any) {
		if (event != undefined){
			if(event.target.id == 'dataInicio'){
				this.filtro.dataInicio = moment(event.target.value).toDate();
			}
			else{
				this.filtro.dataTermino =  moment(event.target.value).toDate();
			}
			this.capturaDatas(11);
		}
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
			case 11: // Custom
				dataInicio = this.filtro.dataInicio;
				dataTermino = this.filtro.dataTermino;
				break;
		}
		this.filtro.periodoId = id;
		this.filtro.dataInicio = dataInicio;
		this.filtro.dataTermino = dataTermino;
		this.pesquisar();
	}

	filtroTransportadorChange(event: any) {
		if (event != undefined)
			this.filtro.transportadorId = event.id;
		this.pesquisar();
	}

	filtroCanalChange(event: any) {
		if (event != undefined)
			this.filtro.canalId = event.id;
		this.pesquisar();
	}

	pesquisar() {
		this.service.onPesquisar(this.filtro);
	}
}
