import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { MenuConfigService } from '../menu-config.service';
import { ClassInitService } from '../class-init.service';
import * as objectPath from 'object-path';
import { LayoutConfigService } from "../layout-config.service";
import { SistemaMenuService } from '../sistemaMenu.service';

@Injectable()
export class MenuAsideService {
	classes: string;
	menuList$: BehaviorSubject<any[]> = new BehaviorSubject([]);

	isDropdown: number = 0;
	dropdownTimeout: number;
	isScrollable: number = 0;

	constructor(
		private menuConfigService: MenuConfigService,
		private classInitService: ClassInitService,
		private layoutConfigService: LayoutConfigService,
		private sistemaMenuService: SistemaMenuService
	) {
		// get menu list
		this.menuConfigService.onMenuUpdated$.subscribe(model => {
			this.sistemaMenuService.getMenus().subscribe((data) => {
				//const data = objectPath.get(model.config, 'aside.items');
				this.menuList$.next(data);
			});
		});

		this.layoutConfigService.onLayoutConfigUpdated$.subscribe(config => {
			if (objectPath.get(config, 'config.aside.left.fixed')) {
				this.isScrollable = 1;
				this.isDropdown = 0;
			}

			if (!objectPath.get(config, 'config.aside.left.fixed') && !objectPath.get(config, 'config.menu.aside.desktop_and_mobile.submenu.accordion')) {
				this.isScrollable = 0;
				this.isDropdown = 1;
				this.dropdownTimeout = objectPath.get(config, 'config.menu.aside.desktop_and_mobile.submenu.dropdown.hover_timeout');
			}
		});
	}
}
