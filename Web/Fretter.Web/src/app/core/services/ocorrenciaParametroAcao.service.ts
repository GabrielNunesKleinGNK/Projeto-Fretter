import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class OcorrenciaParametroAcaoService extends BaseService {
	constructor(){
		super('/api/ocorrenciaParametroAcao/')
	}
}
