import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class ArquivoCobrancaService extends BaseService {
	constructor() {
		super('/api/ArquivoCobranca/')
	}

	getAll(faturaId: number): Observable<any>{
		return this._http.get(`${this._config.baseUrl}${this._api}${faturaId}`);
	}

	importarArquivo(form : FormData, faturaId: number) {
		const url = `upload/${faturaId}`;
 		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, form);
 	}
}
