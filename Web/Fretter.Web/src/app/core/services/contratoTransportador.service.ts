import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { ContratoTransportadorRegraFiltro } from '../models/Filters/contratoTransportadorRegraFiltro.model';

@Injectable()
export class ContratoTransportadorService extends BaseService {
	constructor() {
		super('/api/ContratoTransportador/')
	}

	obterMicroServicos() {
		return this._http.get<any>(`${this._config.baseUrl}${this._api}ObterMicroServico`);
	}

	obterOcorrencias() {
		return this._http.get<any>(`${this._config.baseUrl}${this._api}ObterOcorrencias`);
	}

	obterRegras(model: ContratoTransportadorRegraFiltro): Observable<any> {
		return this._http.post<any>(`${this._config.baseUrl}${this._api}ObterRegras`, model);
	}

	saveRegras(input: any): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}Regras`, input);
	}
}
