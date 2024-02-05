
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { EmpresaIntegracaoItemDetalheFiltro } from '../models/Filters/empresaIntegracaoItemDetalheFiltro.model';
import { EmpresaIntegracaoItemDetalhe } from '../models/Fusion/empresaIntegracaoItemDetalhe.model';

@Injectable()
export class EmpresaIntegracaoItemDetalheService extends BaseService {
	constructor() {
		super('/api/empresaIntegracaoItemDetalhe/')
	}

	public obterDados(model : EmpresaIntegracaoItemDetalheFiltro): Observable<any> {
		const url = `ObterDados`;
		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, model);
	}

	public reprocessarLote(ids : Array<number>): Observable<any> {
		const url = `ReprocessarLote`;
		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, ids);
	}
}
