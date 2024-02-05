import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable, forkJoin, of } from 'rxjs';
import { BaseService } from './base.service';
import { FaturaFiltro } from '../models/faturaFiltro';
import { FaturaPeriodoFiltro } from '../models/faturaPeriodoFiltro';
import { ImportacaoFatura } from '../models/importacaoFatura';

@Injectable()
export class GeradorFaturaService extends BaseService {
	onAtualizar: EventEmitter<any> = new EventEmitter();
	onAtualizarCriticas: EventEmitter<any> = new EventEmitter();
	onExibirListaCriticas: EventEmitter<boolean> = new EventEmitter();
	onLoad: EventEmitter<any> = new EventEmitter();
	onTipo: EventEmitter<number> = new EventEmitter();
	constructor() {
		super('/api/fatura/')
	}

	onCarregarCriticas(criticas: any[]) {
		this.onAtualizarCriticas.emit(criticas);
	}

	onExibirCriticas(exibir: boolean){
		this.onExibirListaCriticas.emit(exibir);
	}

	onCarregar(entrega: any[]) {
		this.onAtualizar.emit(entrega);
	}

	onCarregarLoad(viewLoad: any) {
		this.onLoad.emit(viewLoad);
	}

	onCarregarTipo(tipo: number) {
		this.onTipo.emit(tipo);
	}

	processarFaturaManual(model: ImportacaoFatura[]) {
		const url1 = 'processarFaturaManual';
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, model, { responseType: 'blob' });
	}

	processarFaturaAprovacao(model: ImportacaoFatura[]) {
		const url1 = 'processarFaturaAprovacao';
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, model);
	}

	getEntregaPorDoccob(files: FormData) {
		const url = 'upload';
		this.onLoad.emit(true);		
		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, files);
	}

	getEntregaPeriodo(model: FaturaPeriodoFiltro) {
		const url = 'entregaPorPeriodo';
		this.onLoad.emit(true);
		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, model);
	}
}