import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ProvedorService extends BaseService {
	constructor(){
		super('/api/provedor/')
	}
}
