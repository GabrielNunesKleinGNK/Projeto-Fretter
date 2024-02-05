import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaTransportadorService extends BaseService {
	constructor(){
		super('/api/empresaTransportador/')
	}
}
