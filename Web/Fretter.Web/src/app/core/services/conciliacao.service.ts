import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { RelatorioDetalhadoFiltro } from '../filters/relatorioDetalhadoFiltro';

@Injectable()
export class ConciliacaoService extends BaseService {
	constructor() {
		super('/api/Conciliacao/')
	}

	public getRelatorioDetalhado(filtro: RelatorioDetalhadoFiltro): Observable<any> {
		var queryString = super.getQueryString(filtro);
		return this._http.get(`${this._config.baseUrl}${this._api}Relatorio/Detalhe?${queryString}`);
	}

	public getRelatorioDetalhadoArquivo(filtro: RelatorioDetalhadoFiltro): Observable<any> {
		var queryString = super.getQueryString(filtro);
		return this._http.get(`${this._config.baseUrl}${this._api}Relatorio/DetalheArquivo?${queryString}`);
	}


	public getStatus(): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}Status`);
	}

	public getArquivo(conciliacaoId: number): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}Arquivo/Detalhe/${conciliacaoId}`);
	}

	public postEnviaConciliacaoRecalculoFrete(ids: Number[]): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}EnviarParaRecalculoFrete`, ids);
	}

	public postEnviaConciliacaoRecalculoFreteMassivo(filtro: RelatorioDetalhadoFiltro): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}EnviarParaRecalculoFreteMassivo`, filtro);
	}
}
