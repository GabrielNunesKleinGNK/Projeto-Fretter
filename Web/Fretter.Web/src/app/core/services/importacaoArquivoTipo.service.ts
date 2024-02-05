import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ImportacaoArquivoTipoService extends BaseService {
	constructor() {
		super('/api/importacaoArquivoTipo/')
	}
 }
