import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { AgendamentoDisponibilidadeFiltro } from '../models/Filters/agendamentoDisponibilidadeFiltro.model';

@Injectable()
export class AgendamentoEntregaService extends BaseService {
	constructor() {
		super('/api/agendamentoEntrega/')
	}

	obterDetalhesPorCep(cep: string): Observable<any>{
		var url = `${this._config.baseUrl}${this._api}cep/${cep}`;
		return this._http.get(url);
	}

	obterDisponibilidade(filtro : AgendamentoDisponibilidadeFiltro): Observable<any>{		
		var url = `${this._config.baseUrl}${this._api}disponibilidade`;
		return this._http.post(url, filtro);
	}
}