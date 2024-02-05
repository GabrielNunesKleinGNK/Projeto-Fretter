import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LayoutModule } from '../../../layout/layout.module';
import { ListTimelineModule } from '../../../partials/layout/quick-sidebar/list-timeline/list-timeline.module';
import { MatIconModule } from '@angular/material';

@NgModule({
	imports: [
		CommonModule,
		LayoutModule,
		ListTimelineModule,
		MatIconModule,
		RouterModule.forChild([
			{
				path: '',
				component: null
			}
		])
	],
	providers: [],
	declarations: [
		
	]
})
export class DashboardModule {}
