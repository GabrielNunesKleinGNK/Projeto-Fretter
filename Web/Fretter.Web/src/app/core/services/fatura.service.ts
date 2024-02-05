import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { FaturaFiltro } from '../models/faturaFiltro';

@Injectable()
export class FaturaService extends BaseService {
	onAtualizar: EventEmitter<any> = new EventEmitter();
	constructor() {
		super('/api/fatura/')	
	}

	public getFaturasDaEmpresa(model : FaturaFiltro): Observable<any> {
		let queryString = super.getQueryString(model);
		return this._http.get(`${this._config.baseUrl}${this._api}filter?${queryString}`);
	}
	
	public getCiclos(): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}ciclos`);
	}

	public Acao(model : any): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}acao`, model);;
	}
	
	saveStatus(faturaId: number, status: number) {
		const url = faturaId.toString() + '/status';

		const data = {
			Fatura: status
		}

		return this._http.put(`${this._config.baseUrl}${this._api}${url}`, data);
	}

	downloadDemonstrativo(model : FaturaFiltro) {
		const url1 = 'downloadDemonstrativo';
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, model,{ responseType: 'blob' });
	}
}
