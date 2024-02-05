import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ClienteEnderecoService extends BaseService {
	constructor(){
		super('/api/clienteEndereco/')
	}
}
