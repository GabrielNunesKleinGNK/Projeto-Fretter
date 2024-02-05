import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class UsuarioService extends BaseService {
	constructor() {
		super('/api/usuario/')
	}

	public getPerfil(): Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}perfil`);
	}


	savePermissoes(usuarioId: number, menus: number[]) {
		const url = usuarioId.toString() + '/Permissoes';

		const data = {
			Menus: menus
		}

		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, data);
	}

	getMenus(usuarioId: number) : Observable<any> {
		const url = usuarioId.toString() + '/Menus';
		return this._http.get(`${this._config.baseUrl}${this._api}${url}`);
	}

	getCanaisUsuario() : Observable<any> {
		return this._http.get(`${this._config.baseUrl}${this._api}Canais`);
	}
}
