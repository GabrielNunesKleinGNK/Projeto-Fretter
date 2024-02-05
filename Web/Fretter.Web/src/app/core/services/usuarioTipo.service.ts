import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class UsuarioTipoService extends BaseService {
	constructor(){
		super('/api/usuarioTipo/')
	}
}
