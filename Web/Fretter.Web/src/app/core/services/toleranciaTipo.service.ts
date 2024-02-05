import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { AppConfig } from '../models/app.config';

@Injectable()
export class ToleranciaTipoService extends BaseService {
	constructor() {
		super('/api/ToleranciaTipo/')
	}

}
