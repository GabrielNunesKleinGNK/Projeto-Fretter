import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable()
export class ConfiguracaoCteTransportadorService extends BaseService {
	constructor() {
		super('/api/ConfiguracaoCteTransportador/')
	}

}
