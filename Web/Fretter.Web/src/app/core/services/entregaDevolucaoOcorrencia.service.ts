import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class EntregaDevolucaoOcorrenciaService extends BaseService {
	constructor(){
		super('/api/EntregaDevolucaoOcorrencia/')
	}

	getHistoricoEntregaDevolucao(entregaDevolucaoId : number): Observable<any> {
		const url = `Ocorrencias/${entregaDevolucaoId}`;
		return this._http.get(`${this._config.baseUrl}${this._api}${url}`);
	}
}
