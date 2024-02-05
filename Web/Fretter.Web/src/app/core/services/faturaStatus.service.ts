import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class FaturaStatusService extends BaseService {
	constructor() {
		super('/api/faturaStatus')
	}
}
