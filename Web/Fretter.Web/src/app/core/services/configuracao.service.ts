import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ConfiguracaoService extends BaseService {
	constructor(){
		super('/api/configuracao/')
	}
}
