import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class OcorrenciaImportacaoService extends BaseService {
	constructor(){
		super('/api/ocorrenciaImportacao/')
	}
}
