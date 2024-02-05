import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutConfigService } from '../../../../core/services/layout-config.service';
import { SubheaderService } from '../../../../core/services/layout/subheader.service';
import * as objectPath from 'object-path';

@Component({
	selector: 'm-arquivo-importacao-log',
	templateUrl: './arquivoImportacaoLog.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class ArquivoImportacaoLogComponent implements OnInit {

	public config: any;

	constructor(
		private router: Router,
		private layoutConfigService: LayoutConfigService,
		private subheaderService: SubheaderService
	) {
	}

	ngOnInit(): void {}
}
