import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class EntregaService extends BaseService {
	constructor(){
		super('/api/entrega/')
	}
}
