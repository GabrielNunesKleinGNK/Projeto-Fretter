import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ContratoTransportadorHistoricoService extends BaseService {
	constructor() {
		super('/api/ContratoTransportadorHistorico/')
	}
}
