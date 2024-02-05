import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { EntregaDevolucaoFiltro } from '../models/Fusion/entregaDevolucaoFiltro';

@Injectable()
export class EntregaDevolucaoService extends BaseService {
	constructor(){
		super('/api/entregaDevolucao/')
	}

	public GetEntregasDevolucoes(model : EntregaDevolucaoFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}entregas`, model);
	}

	public Download(model : any[]): Observable<any> {
		return  this._http.post(`${this._config.baseUrl}${this._api}download`, model,  { responseType: 'blob' });;
	}

	public Acao(model : any): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}acao`, model);;
	}
}
