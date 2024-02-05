import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class RegraEstoqueService extends BaseService {
	constructor() {
		super('/api/RegraEstoque/')
	}

}
