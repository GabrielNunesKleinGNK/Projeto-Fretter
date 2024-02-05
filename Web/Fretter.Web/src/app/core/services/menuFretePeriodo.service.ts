import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';


@Injectable()
export class MenuFretePeriodoService extends BaseService {
	constructor() {
		super('/api/menuPeriodo/')
	}
}