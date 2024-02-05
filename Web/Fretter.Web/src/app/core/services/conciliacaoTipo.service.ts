import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ConciliacaoTipoService extends BaseService {
	constructor() {
		super('/api/ConciliacaoTipo/')
	}

}
