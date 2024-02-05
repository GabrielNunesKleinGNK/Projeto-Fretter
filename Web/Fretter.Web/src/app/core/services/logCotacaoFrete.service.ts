import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { LogCotacaoFreteFiltro } from '../models/logCotacaoFreteFiltro';

@Injectable()
export class LogCotacaoFreteService extends BaseService {

	onAtualizar: EventEmitter<any> = new EventEmitter();
	constructor(){
		super('/api/logCotacaoFrete/')
	}

	onPesquisar(filtro: LogCotacaoFreteFiltro) {
        this.onAtualizar.emit(filtro);
    }
	
	public getLista(model : LogCotacaoFreteFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}lista`, model);
	}
}
