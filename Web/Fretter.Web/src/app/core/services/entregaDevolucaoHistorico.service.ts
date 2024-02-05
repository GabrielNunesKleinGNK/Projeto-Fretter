import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class EntregaDevolucaoHistoricoService extends BaseService {
	constructor(){
		super('/api/EntregaDevolucaoHistorico/')
	}

	getHistoricoEntregaDevolucao(entregaDevolucaoId : number): Observable<any> {
		const url = `obter/${entregaDevolucaoId}`;
		return this._http.get(`${this._config.baseUrl}${this._api}${url}`);
	}
}
