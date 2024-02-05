import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class TransportadorService extends BaseService {
	constructor(){
		super('/api/transportador/')
	}

	public getTransportadoresPorEmpresa() : Observable<any>{
		return this._http.get(`${this._config.baseUrl}${this._api}transportadoresPorEmpresa`);
	}

	public getTransportadoresCnpj(transportadorId : Number) : Observable<any>{
		return this._http.get(`${this._config.baseUrl}${this._api}CNPJ/`+ transportadorId);
	}
}
