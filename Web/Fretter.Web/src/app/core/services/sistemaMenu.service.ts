import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { TokenStorage } from '../auth/token-storage.service';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/internal/operators/map';

@Injectable()
export class SistemaMenuService extends BaseService {
	API_URL = this._config.baseUrl;
	constructor(
		private tokenStorage: TokenStorage,
		private http: HttpClient
		){
		super('/api/SistemaMenu/');
	}

	getMenus(): Observable<any> {
		let userMenus = <any>this.tokenStorage.getUserMenu();
		if(userMenus.value!=null){
			return Observable.create( observer => {
				observer.next(userMenus.value);
				observer.complete();
			});
		}
		else{
			return this.http.get<any>(this.API_URL + "/api/SistemaMenu/Menus").pipe(
				map((result: any) => {
					return result.menuItens;
				})
			);
		}
	}
}
