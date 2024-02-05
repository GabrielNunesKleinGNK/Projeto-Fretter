import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class NotificacaoRetornoService extends BaseService {
	constructor(){
		super('/api/notificacaoRetorno/')
	}
}
