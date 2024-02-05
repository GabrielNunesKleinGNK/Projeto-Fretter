import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaFilialService extends BaseService {
	constructor(){
		super('/api/empresaFilial/')
	}
}
