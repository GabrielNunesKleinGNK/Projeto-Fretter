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
import { JsonViewComponent } from '../../logDashboard/logDashboard.jsonView/jsonView.component';
import { ArquivoImportacaoLogFiltro } from '../../../../../core/models/arquivoImportacaoLogFiltro';
import { ArquivoImportacaoLogService } from '../../../../../core/services/arquivoImportacaoLog.service';
import { ArquivoImportacaoLogLista } from '../../../../../core/models/arquivoImportacaoLogLista';


@Component({
  selector: 'm-arquivo-importacao-log-list',
  styleUrls: ['./arquivoImportacaoLog.list.component.scss'],
  templateUrl: './arquivoImportacaoLog.list.component.html'
})
export class ArquivoImportacaoLogListComponent implements OnInit {
	//List
	dataSource: MatTableDataSource<ArquivoImportacaoLogLista>;
	displayedColumns: string[] = ['dsNome',
	'numeroPedido', 
	'dtImportacao', 
	'dtImportacaoDATE', 
	'actions'];

	viewLoading: boolean = false;
	subFiltro: Subscription;
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(
		public dialog: MatDialog,
		private service : ArquivoImportacaoLogService,

		//Comum
		private cdr: ChangeDetectorRef
		){
		
	}
	ngOnInit() {
		this.subFiltro = this.service.onAtualizar.subscribe(data => {
			this.load(data);
		});
	}

	load(filtro : ArquivoImportacaoLogFiltro) {
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

