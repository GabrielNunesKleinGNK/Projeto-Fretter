import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from "@angular/common/http";
import { AppModule } from "../../../app/app.module";
import { AppConfig } from '../models/app.config';
import { AppConfigService } from './app.config.service';

@Injectable()
export class BaseService {

	_http: HttpClient;
	_api: string;
	_router: Router;
	_config: AppConfig;

	constructor(api: string) {
		this._api = api;
		this._http = AppModule.injector.get(HttpClient);
		this._router = AppModule.injector.get(Router);
		this._config = AppModule.injector.get(AppConfigService);
	}

	getById(id: any) {
		return this._http.get<any>(`${this._config.baseUrl}${this._api}${id}`).pipe(map(e => e));
	}

	get(params: string = '') {
		return this._http.get<Array<any>>(`${this._config.baseUrl}${this._api}?${params}`).pipe(map(e => e));
	}

	getFilter(model: any = null, start: number = 0, limit: number = 100): Observable<any> {
		let queryString = this.getQueryString(model);
		return this._http.get<Array<any>>(`${this._config.baseUrl}${this._api}filter?start=${start}&limit=${limit}&${queryString}`);
	}

	save(input: any): Observable<any> {

		if (input.id == 0)
			return this._http.post(`${this._config.baseUrl}${this._api}`, input);
		else
			return this._http.put(`${this._config.baseUrl}${this._api}${input.id}`, input);
	}

	post(input: any): Observable<any> {
		return this._http.post(`${this._config.baseUrl}${this._api}`, input);
	}

	delete(id: Number): Observable<any> {
		return this._http.delete(`${this._config.baseUrl}${this._api}${id}`);
	}

	getQueryString(object: any) {
		var queryString = "";
		if (object)
			var queryString = Object.keys(object).map(function (key) {
				return key + '=' + (object[key] === null ? "" : object[key])
			}).join('&');
		return queryString;
	}
}
