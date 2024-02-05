
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class IntegracaoService extends BaseService {
	constructor() {
		super('/api/integracao/')
	}

	buscaCamposDePara() : Observable<any> {
		const url = `DePara`;
		return this._http.get(`${this._config.baseUrl}${this._api}${url}`);
	}
	
	public testeIntegracao(model : any): Observable<any> {
		const url = `TesteIntegracao`;
		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, model);
	}
}
