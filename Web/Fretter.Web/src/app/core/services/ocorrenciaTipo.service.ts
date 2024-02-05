import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class OcorrenciaTipoService extends BaseService {
	constructor(){
		super('/api/ocorrenciaTipo/')
	}
}
