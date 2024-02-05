import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable()
export class ContratoTransportadorArquivoTipoService extends BaseService {
	constructor() {
		super('/api/ContratoTransportadorArquivoTipoController/')
	}

	public postList(model : any[]): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}arquivotipo`, model);;
	}
}
