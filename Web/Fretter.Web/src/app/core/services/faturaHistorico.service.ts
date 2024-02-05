import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class FaturaHistoricoService extends BaseService {
	constructor() {
		super('/api/FaturaHistorico')
	}

	getHistoricoFaturasDaEmpresa(faturaId : number): Observable<any> {
		const url = '/historico/' + faturaId.toString();
		return this._http.get(`${this._config.baseUrl}${this._api}${url}`);
	}
}
