import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaTransporteTipoItemService extends BaseService {
	constructor(){
		super('/api/empresaTransporteTipoItem/')
	}

	public getTransporteTipoItemPorTipo(transporteTipoId : number): Observable<any> {
		const url = `obterPorTipo/${transporteTipoId}`;
		return this._http.get(`${this._config.baseUrl}${this._api}${url}`);
	}
}
