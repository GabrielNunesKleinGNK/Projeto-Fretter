
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmpresaImportacao } from '../models/empresaImportacao.model';
import { EmpresaImportacaoDetalhe } from '../models/empresaImportacaoDetalhe.model';
import { EmpresaImportacaoFiltro } from '../models/empresaImportacaoFiltro';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaService extends BaseService {
	constructor() {
		super('/api/empresa/')
	}

	importarArquivo(form: FormData) {
		const url = `upload`;
		return this._http.post(`${this._config.baseUrl}${this._api}${url}`, form);
	}

	obterEmpresaImportacaoArquivo(model: EmpresaImportacaoFiltro): Observable<Array<EmpresaImportacao>> {
		const url = `importacao`;
		return this._http.post<Array<EmpresaImportacao>>(`${this._config.baseUrl}${this._api}${url}`, model);
	}

	obterEmpresaImportacaoDetalhe(importacaoArquivoId: number) {
		const url = `importacao/detalhe/${importacaoArquivoId}`;
		return this._http.get<Array<EmpresaImportacaoDetalhe>>(`${this._config.baseUrl}${this._api}${url}`);

	}

	download(arquivoId: number, arquivoNome: string) {
		const url1 = 'download';
		return this._http.post(`${this._config.baseUrl}${this._api}${url1}`, { id: arquivoId, fileName: arquivoNome }, { responseType: 'blob' });
	}

	downloadTemplate() {
		const url1 = 'downloadTemplate';
		return this._http.get(`${this._config.baseUrl}${this._api}${url1}`, { responseType: 'blob' });
	}

	
	obterEmpresaLogada() {
		const url = 'ObterEmpresaLogada';
		return this._http.get(`${this._config.baseUrl}${this._api}${url}`);
	}
}
