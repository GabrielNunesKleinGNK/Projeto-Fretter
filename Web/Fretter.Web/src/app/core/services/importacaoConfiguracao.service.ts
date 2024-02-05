import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ImportacaoConfiguracaoService extends BaseService {
	constructor(){
		super('/api/importacaoConfiguracao/')
	}
}
