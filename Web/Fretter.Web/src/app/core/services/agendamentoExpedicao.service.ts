import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class AgendamentoExpedicaoService extends BaseService {
	constructor() {
		super('/api/agendamentoExpedicao/')
	}
}
