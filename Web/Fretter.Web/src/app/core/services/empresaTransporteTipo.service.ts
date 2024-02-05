import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaTransporteTipoService extends BaseService {
	constructor(){
		super('/api/empresaTransporteTipo/')
	}
}
