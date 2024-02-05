import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable()
export class ProdutoService extends BaseService {
	constructor() {
		super('/api/Produto/')
	}

	public getProdutoPorSku(sku : string) : Observable<any>{
		return this._http.get(`${this._config.baseUrl}${this._api}obterProdutoPorSku/${sku}`);
	}

	public getAllProdutoPorDescricao(descricao : string) : Observable<any>{
		return this._http.get(`${this._config.baseUrl}${this._api}obterProdutoPorDescricao/${descricao}`);
	}
}
