import { AfterViewInit, Component, OnInit,ChangeDetectorRef } from '@angular/core';
import { SistemaMenuService } from '../../../../core/services/sistemaMenu.service';

@Component({
	selector: 'm-home',
	templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit,AfterViewInit {

	public config: any;
	public data: any[];
	public dataMenu: any[];
	viewLoading: boolean = false;

	constructor(
		private sistemaMenuService: SistemaMenuService,
		private changeDetector: ChangeDetectorRef
	) {
	}

	ngOnInit(): void {
		this.viewLoading=true;
		this.dataMenu= [];
		this.sistemaMenuService.getMenus().subscribe((data) => {
			this.load(data);
			this.changeDetector.detectChanges();
		});
	}

	load(data : any[]) {
		montarMenu(this.dataMenu ,data,"");
		this.viewLoading=false;
	}
	
	ngAfterViewInit(): void {	
		//this.load(this.data);
	}
}

function montarMenu(dataMenu:any[], menu:any[], titleHeader: string):void{
	menu.forEach(function (item) {
		if(item.submenu!=undefined)
			montarMenu(dataMenu, item.submenu, item.title);
		else
			adicionaItemMenu(dataMenu, item, titleHeader);
	  }); 
}

function adicionaItemMenu(dataMenu:any[] ,item: any, titleHeader: string):void{
	dataMenu.push(
		{
			titleHeader:titleHeader,
			title: item.title,
			icon: item.icon,
			page: item.page
		}
	);
}