import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { AgendamentoRegra } from '../models/agendamentoRegra.model';

@Injectable()
export class AgendamentoRegraService extends BaseService {
	constructor() {
		super('/api/agendamentoRegra/')
	}

	public getRegraTipoOperador(): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}RegraTipoOperador`);
	}

	public getRegraTipoItem(): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}RegraTipoItem`);
	}

	public getRegraTipo(): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}RegraTipo`);
	}
	
	public postIncluirRegra(agendamentoRegra: AgendamentoRegra) : Observable<any>{
		return this._http.post(`${this._config.baseUrl}${this._api}IncluirRegra`, agendamentoRegra);
	}

	public putAlterarRegra(agendamentoRegra: AgendamentoRegra) : Observable<any>{
		return this._http.put(`${this._config.baseUrl}${this._api}AlterarRegra`, agendamentoRegra);
	}

	public InativarRegra(id: number) : Observable<any>{
		return this._http.put(`${this._config.baseUrl}${this._api}InativarRegra/${id}`, id);
	}
}
