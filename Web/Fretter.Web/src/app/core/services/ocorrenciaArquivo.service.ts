import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntregaEmAbertoFiltro } from '../models/Filters/entregaEmAbertoFiltro.model';
import { EntregaOcorrencia } from '../models/Fusion/entregaOcorrencia.model';

@Injectable()
export class OcorrenciaArquivoService extends BaseService {
	constructor(){
		super('/api/ocorrenciaArquivo/')
	}
}
