import { EventEmitter, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable()
export class ImportacaoArquivoService extends BaseService {

	constructor() {
		super('/api/importacaoArquivo/')
	}

	getResumo(model: any ): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}${'resumo'}`, model);
	}

	importarArquivos(files : FormData) {
		const url = 'upload';
 		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, files);
 	}
	 
	download(url : string){
		const url1 = 'download';
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, { url });
	}
 }
