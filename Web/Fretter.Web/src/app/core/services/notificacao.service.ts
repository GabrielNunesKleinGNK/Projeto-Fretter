import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class NotificacaoService extends BaseService {
	constructor(){
		super('/api/notificacao/')
	}
}
