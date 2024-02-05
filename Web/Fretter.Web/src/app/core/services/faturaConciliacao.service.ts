import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable, forkJoin, of } from 'rxjs';
import { BaseService } from './base.service';
import { FaturaConciliacaoReenvio } from '../models/faturaConciliacaoReenvio.model';


@Injectable()
export class FaturaConciliacaoService extends BaseService {
	constructor(){
		super('/api/faturaConciliacao/')
	}

    public GetAllFaturaConciliacaoIntegracao(faturaId : number): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}GetAllFaturaConciliacaoIntegracao/${faturaId}`);
	}
	public GetJsonIntegracaoFaturaConciliacao(empresaIntegracaoItemDetalheId : number): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}GetJsonIntegracaoFaturaConciliacao/${empresaIntegracaoItemDetalheId}`);
	}
	public ReenviarFaturaConciliacaoIndividual(conciliacaoReenvio : FaturaConciliacaoReenvio): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}ReenviarFaturaConciliacaoIndividual`, conciliacaoReenvio);
	}
	public ReenviarFaturaConciliacaoMassivo(lstConciliacaoReenvio : Array<FaturaConciliacaoReenvio>): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}ReenviarFaturaConciliacaoMassivo`, lstConciliacaoReenvio);
	}
}
