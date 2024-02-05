import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class NotificacaoStatusService extends BaseService {
	constructor(){
		super('/api/notificacaoStatus/')
	}
}
