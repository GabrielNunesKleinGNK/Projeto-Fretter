import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class GrupoService extends BaseService {
	constructor() {
		super('/api/Grupo/')
	}

	public getGruposPorEmpresa() : Observable<any>{
		return this._http.get(`${this._config.baseUrl}${this._api}GruposPorEmpresa`);
	}
}
