import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ClienteService extends BaseService {
	constructor(){
		super('/api/cliente/')
	}
}
