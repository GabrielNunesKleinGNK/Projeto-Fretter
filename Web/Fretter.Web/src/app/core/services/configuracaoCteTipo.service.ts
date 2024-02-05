import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ConfiguracaoCteTipoService extends BaseService {
	constructor() {
		super('/api/ConfiguracaoCteTipo/')
	}

}
