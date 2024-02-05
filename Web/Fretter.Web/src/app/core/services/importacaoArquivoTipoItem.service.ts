import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable()
export class ImportacaoArquivoTipoItemService extends BaseService {
	constructor() {
		super('/api/importacaoArquivoTipoItem/')
	}

	public getTipoCobrancaPorTipoArquivo(importacaoArquivoTipoId : number): Observable<any> {
		
		return this._http.get(`${this._config.baseUrl}${this._api}getTipoCobrancaPorTipoArquivo/${importacaoArquivoTipoId}`);
	}
 }
