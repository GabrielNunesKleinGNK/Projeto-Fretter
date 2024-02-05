import { Injectable, EventEmitter } from '@angular/core';
import { BaseService } from './base.service';
import { RelatorioFiltro } from '../models/relatorio.filtro';
import { map } from 'rxjs/operators';

@Injectable()
export class RelatorioService extends BaseService {
	constructor(){
		super('/api/relatorio')
	}
	onAtualizar: EventEmitter<any> = new EventEmitter();

	onPesquisar(filtro: RelatorioFiltro) {
        this.onAtualizar.emit(filtro);
    }

	getFiltro(){
		return this._http.get<Array<RelatorioFiltro>>(`${this._config.baseUrl}${this._api}/filtro`).pipe(map(e=>e));
  	}
  
	getRelatorioLoja(filtro : RelatorioFiltro){
		return this._http.post<Array<RelatorioFiltro>>(`${this._config.baseUrl}${this._api}/loja`,filtro).pipe(map(e=>e));
	}

	getRelatorioCategoria(filtro : RelatorioFiltro){
		return this._http.post<Array<RelatorioFiltro>>(`${this._config.baseUrl}${this._api}/categoria`,filtro).pipe(map(e=>e));
	}

	getRelatorioEmpresa(filtro : RelatorioFiltro){
		return this._http.post<Array<RelatorioFiltro>>(`${this._config.baseUrl}${this._api}/empresa`,filtro).pipe(map(e=>e));
	}

	getRelatorioEmpresaDetalhe(filtro: RelatorioFiltro) {
	return this._http.post<Array<RelatorioFiltro>>(`${this._config.baseUrl}${this._api}/empresa/categoria`,filtro).pipe(map(e=>e));
	}

	getRelatorioVendedor(filtro : RelatorioFiltro){
		return this._http.post<Array<RelatorioFiltro>>(`${this._config.baseUrl}${this._api}/vendedor`,filtro).pipe(map(e=>e));
	}

	getRelatorioProduto(filtro : RelatorioFiltro){
		return this._http.post<Array<RelatorioFiltro>>(`${this._config.baseUrl}${this._api}/produtomediapreco`,filtro).pipe(map(e=>e));
	}
}
