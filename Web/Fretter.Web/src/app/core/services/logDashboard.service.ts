import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { LogDashboardFiltro } from '../models/logDashboadFiltro';

@Injectable()
export class LogDashboardService extends BaseService {

	onAtualizar: EventEmitter<any> = new EventEmitter();
	constructor(){
		super('/api/logDashboard/')
	}

	onPesquisar(filtro: LogDashboardFiltro) {
        this.onAtualizar.emit(filtro);
    }

	public getResumo(model : LogDashboardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}resumoMensagem`, model);
	}
	public getProcesso(model : LogDashboardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}resumoProcesso`, model);
	}

	public getGrafico(model : LogDashboardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}diario`, model);
	} 
	
	public getLista(model : LogDashboardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}lista`, model);
	}
}
