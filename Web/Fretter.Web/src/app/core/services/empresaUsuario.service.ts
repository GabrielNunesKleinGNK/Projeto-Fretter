import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class EmpresaUsuarioService extends BaseService {
	constructor(){
		super('/api/empresaUsuario/')
	}
}
