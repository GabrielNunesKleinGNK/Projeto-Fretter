import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class CanalService extends BaseService {
	constructor(){
		super('/api/canal/')
	}
	
	public getCanaisPorEmpresa() : Observable<any>{
		return this._http.get(`${this._config.baseUrl}${this._api}canaisPorEmpresa`);
	}
}
