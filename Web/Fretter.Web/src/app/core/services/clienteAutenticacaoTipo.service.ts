import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ClienteAutenticacaoTipoService extends BaseService {
	constructor(){
		super('/api/clienteAutenticacaoTipo/')
	}
}
