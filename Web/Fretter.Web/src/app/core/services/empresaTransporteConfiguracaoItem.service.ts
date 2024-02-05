import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaTransporteConfiguracaoItemService extends BaseService {
	constructor(){
		super('/api/empresaTransporteConfiguracaoItem/')
	}
}
