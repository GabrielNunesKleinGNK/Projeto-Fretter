import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ArquivoImportacaoLogFiltro } from '../models/arquivoImportacaoLogFiltro';

@Injectable()
export class ArquivoImportacaoLogService extends BaseService {

	onAtualizar: EventEmitter<any> = new EventEmitter();
	constructor(){
		super('/api/arquivoImportacaoLog/')
	}

	onPesquisar(filtro: ArquivoImportacaoLogFiltro) {
        this.onAtualizar.emit(filtro);
    }

	public getLista(model : ArquivoImportacaoLogFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}lista`, model);
	}
}
