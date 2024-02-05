import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ImportacaoConfiguracaoTipoService extends BaseService {
	constructor(){
		super('/api/importacaoConfiguracaoTipo/')
	}
}
