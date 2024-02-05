import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CanalVenda } from '../models/Fusion/canalVenda';
import { BaseService } from './base.service';

@Injectable()
export class CanalVendaService extends BaseService {
	constructor(){
		super('/api/CanalVenda/')
	}

	public getCanalVendaPorEmpresa(model : CanalVenda): Observable<any> {
		let queryString = super.getQueryString(model);
		return this._http.get(`${this._config.baseUrl}${this._api}filter?${queryString}`);
	}
}
