import { Injectable } from '@angular/core';
import { BaseService } from './base.service';


@Injectable()
export class EntregaDevolucaoStatusAcaoService extends BaseService {
	constructor(){
		super('/api/entregaDevolucaoStatusAcao/')
	}
}
