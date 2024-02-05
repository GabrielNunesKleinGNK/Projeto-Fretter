
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmpresaIntegracaoFiltro } from '../models/Fusion/empresaIntegracaoFiltro.model';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaIntegracaoService extends BaseService {
	constructor() {
		super('/api/empresaIntegracao/')
	}

	public getEmpresaIntegracao(model : EmpresaIntegracaoFiltro): Observable<any> {
		let queryString = super.getQueryString(model);
		return this._http.get(`${this._config.baseUrl}${this._api}filter?${queryString}`);
	}
}
