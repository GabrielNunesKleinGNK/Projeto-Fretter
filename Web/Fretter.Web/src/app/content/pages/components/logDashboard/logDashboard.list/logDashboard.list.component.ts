import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator,  MatDialog } from '@angular/material';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

import { Subscription } from 'rxjs';
import { LogDashboardService } from '../../../../../core/services/logDashboard.service';
import { LogDashboardFiltro } from '../../../../../core/models/logDashboadFiltro';
import { LogDashboardLista } from '../../../../../core/models/logDashboadLista';
import { JsonViewComponent } from '../logDashboard.jsonView/jsonView.component';


@Component({
  selector: 'm-logDashboard-list',
  styleUrls: ['./logDashboard.list.component.scss'],
  templateUrl: './logDashboard.list.component.html'
})
export class LogDashboardListComponent implements OnInit {
	//List
	dataSource: MatTableDataSource<LogDashboardLista>;
	displayedColumns: string[] = ['applicationName',
	'environmentName', 
	'level', 
	'processName', 
	'message', 
	'exceptionType', 
	'timestamp',
	'actions',];

	viewLoading: boolean = false;
	subFiltro: Subscription;
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(
		public dialog: MatDialog,
		private service : LogDashboardService,

		//Comum
		private cdr: ChangeDetectorRef
		){
		
	}
	ngOnInit() {
		this.subFiltro = this.service.onAtualizar.subscribe(data => {
			this.load(data);
		});
	}

	load(filtro : LogDashboardFiltro) {

		this.service.getLista(filtro).subscribe(result => {
			this.dataSource = new MatTableDataSource(result);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		});
	}
	
	showObject(model){
		let obj = {
			json: model.objectJson
		};
		const dialogRef = this.dialog.open(JsonViewComponent, { data:{ obj } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
		});
	}

}

