import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class TransportadorCnpjService extends BaseService {
	constructor(){
		super('/api/transportadorCnpj/')
	}
}
