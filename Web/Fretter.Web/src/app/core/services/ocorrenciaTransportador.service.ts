import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class OcorrenciaTransportadorService extends BaseService {
	constructor(){
		super('/api/ocorrenciaTransportador/')
	}
}
