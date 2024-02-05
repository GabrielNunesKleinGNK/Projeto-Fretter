import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntregaEmAbertoFiltro } from '../models/Filters/entregaEmAbertoFiltro.model';
import { EntregaOcorrencia } from '../models/Fusion/entregaOcorrencia.model';

@Injectable()
export class EntregaOcorrenciaService extends BaseService {
	constructor(){
		super('/api/entregaOcorrencia/')
	}

	public  getDePara(): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}obterdepara`);
	}

	public  obterEntragasEmAberto(filtro: EntregaEmAbertoFiltro): Observable<any> {
		const url1 = 'entragasemaberto';
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, filtro);
	}

	public  inserir(model: Array<EntregaOcorrencia>): Observable<any> {
		const url1 = 'inserir';
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, model);
	}

	public  upload(form: FormData) {
		const url1 = 'upload';
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, form);
	}

	download(comEntregas: boolean, filtro: EntregaEmAbertoFiltro): Observable<any> {
		const url1 = `download/${comEntregas}`;
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, filtro, { responseType: 'blob' });
	}
}
