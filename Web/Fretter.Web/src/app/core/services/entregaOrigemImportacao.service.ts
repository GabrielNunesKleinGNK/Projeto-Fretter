import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EntregaOrigemImportacao } from '../models/Fusion/entregaOrigemImportacao.model';
import { BaseService } from './base.service';

@Injectable()
export class EntregaOrigemImportacaoService extends BaseService {
	constructor(){
		super('/api/EntregaOrigemImportacao/')
	}

	public  getAll(model : EntregaOrigemImportacao): Observable<any> {
		let queryString = super.getQueryString(model);
		return this._http.get(`${this._config.baseUrl}${this._api}filter?${queryString}`);
	}
}
