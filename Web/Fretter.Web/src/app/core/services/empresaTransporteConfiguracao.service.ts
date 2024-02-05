import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaTransporteConfiguracaoService extends BaseService {
	constructor(){
		super('/api/empresaTransporteConfiguracao/')
	}

	public testeIntegracao(model : any): Observable<any> {
		const url = `TesteIntegracao`;
		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, model);
	}
}
