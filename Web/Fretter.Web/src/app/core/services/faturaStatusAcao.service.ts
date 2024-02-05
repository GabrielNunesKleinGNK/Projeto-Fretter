import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable, forkJoin, of } from 'rxjs';
import { BaseService } from './base.service';


@Injectable()
export class FaturaStatusAcaoService extends BaseService {
	constructor(){
		super('/api/faturaStatusAcao/')
	}
}
