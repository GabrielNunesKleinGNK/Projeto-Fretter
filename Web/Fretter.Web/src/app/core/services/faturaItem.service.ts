import { Injectable, EventEmitter } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class FaturaItemService extends BaseService {
	onAtualizar: EventEmitter<any> = new EventEmitter();
	constructor() {
		super('/api/faturaItem/')	
	}
}
