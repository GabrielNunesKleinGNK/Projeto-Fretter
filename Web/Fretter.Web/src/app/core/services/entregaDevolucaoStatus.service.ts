import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class EntregaDevolucaoStatusService extends BaseService {
	constructor(){
		super('/api/entregaDevolucaoStatus/')
	}	
}
