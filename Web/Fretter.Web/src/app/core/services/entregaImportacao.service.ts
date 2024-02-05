import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class EntregaImportacaoService extends BaseService {
	constructor(){
		super('/api/entregaImportacao/')
	}

	
}
