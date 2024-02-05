import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class OcorrenciaEmpresaService extends BaseService {
	constructor(){
		super('/api/ocorrenciaEmpresa/')
	}
}
