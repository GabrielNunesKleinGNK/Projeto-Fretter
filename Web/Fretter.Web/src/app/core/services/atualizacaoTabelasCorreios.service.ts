import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TabelaCorreiosArquivos } from '../models/Fusion/tabelaCorreiosArquivos';
import { BaseService } from './base.service';

@Injectable()
export class AtualizacaoTabelasCorreiosService extends BaseService {
	constructor() {
		super('/api/atualizacaotabelacorreios/')
	}

	importarArquivo(form: FormData) {
		const url = `upload`;
		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, form);
	}
}
