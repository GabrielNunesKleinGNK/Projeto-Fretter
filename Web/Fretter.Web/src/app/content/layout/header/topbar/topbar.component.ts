import {
	Component,
	OnInit,
	Input,
	ChangeDetectorRef
} from '@angular/core';
import { LayoutConfigService } from '../../../../core/services/layout-config.service';
import { EmpresaService } from '../../../../core/services/empresa.service';

@Component({
	selector: 'm-topbar',
	templateUrl: './topbar.component.html',
	//changeDetection: ChangeDetectionStrategy.OnPush
})
export class TopbarComponent implements OnInit {
	//@HostBinding('id') id = 'm_header_nav';
	//@HostBinding('class')
	//classes = 'm-stack__item m-stack__item--fluid m-header-head';
	empresa: any = {nomeFantasia: "carregando..."};

	@Input() searchType: any;

	constructor(
		private layoutConfigService: LayoutConfigService,
		private empresaService: EmpresaService,
		private changeDetector: ChangeDetectorRef
	) {
		// this.layoutConfigService.onLayoutConfigUpdated$.subscribe(model => {
		// 	const config = model.config;
		// 	this.searchType = objectPath.get(config, 'header.search.type');
		// });
		
		this.empresaService.obterEmpresaLogada().subscribe(result => {
			this.empresa = result[0];
			this.changeDetector.detectChanges();
		})
	}

	ngOnInit(): void {
	}
}
