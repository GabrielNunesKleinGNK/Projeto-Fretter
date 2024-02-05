import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class NotificacaoTemplateService extends BaseService {
	constructor(){
		super('/api/notificacaoTemplate/')
	}
}
