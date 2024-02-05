import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class IntegracaoTipoService extends BaseService {
	constructor(){
		super('/api/integracaoTipo/')
	}
}
