import { Injectable } from '@angular/core';
import { BaseService } from './base.service';


@Injectable()
export class EntregaDevolucaoAcaoService extends BaseService {
	constructor(){
		super('/api/entregaDevolucaoAcao/')
	}
}
