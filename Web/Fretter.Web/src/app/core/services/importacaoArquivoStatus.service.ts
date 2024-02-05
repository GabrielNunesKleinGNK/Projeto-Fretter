import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ImportacaoArquivoStatusService extends BaseService {

	constructor() {
		super('/api/importacaoArquivoStatus/')
	}
 }
