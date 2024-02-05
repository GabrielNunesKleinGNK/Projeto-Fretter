import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { DashBoardFiltro } from '../models/dashboadFiltro';

@Injectable()
export class DashboardService extends BaseService {

	onAtualizar: EventEmitter<any> = new EventEmitter();
	constructor(){
		super('/api/dashboard/')
	}

	onPesquisar(filtro: DashBoardFiltro) {
        this.onAtualizar.emit(filtro);
    }

	public getResumo(model : DashBoardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}resumo`, model);
	}

	public getEntregasGrafico(model : DashBoardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}entregas`, model);
	} 

	public getTransportadoresQuantidade(model : DashBoardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}transportadoresQuantidade`, model);
	}

	public getTransportadoresTotal(model : DashBoardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}transportadoresLista`, model);
	}
	public getTransportadoresTotalDownload(model : DashBoardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}transportadoresListaDownload`, model,{ responseType: 'blob' });
	}

	public getTransportadoresValor(model : DashBoardFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}transportadoresValor`, model);
	}
}
