import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import * as fs from 'file-saver';
import { Subscription } from 'rxjs';
import { MatPaginator,  MatDialog } from '@angular/material';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { TransportadorTotal } from '../../../../../core/models/transportadorTotal';
import { AlertService } from '../../../../../core/services/alert.service';
import { DashboardService } from '../../../../../core/services/dashboard.service';
import { DashBoardFiltro } from '../../../../../core/models/dashboadFiltro';

@Component({
	selector: 'm-dashboard-transportador-list',
	templateUrl: './dashboard.transportadorList.component.html',
	styleUrls: ['./dashboard.transportadorList.component.scss'],
})
export class DashboardTransportadoresListComponent implements OnInit{
	dataSource: MatTableDataSource<TransportadorTotal>;
	displayedColumns: string[] = ['transportador',    'qtdEntrega',    'qtdCte',    'qtdSucesso',   'qtdDivergencia',    'qtdDivergenciaPeso',    'qtdDivergenciaTarifa'];	viewLoading: boolean = false;
	transportadores : TransportadorTotal[];
	filtroItem : DashBoardFiltro;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	subFiltro: Subscription;
	constructor(private _service: DashboardService, public dialog: MatDialog, private cdr: ChangeDetectorRef, private _alertService : AlertService){
		
	}
	ngOnInit() {
		this.subFiltro = this._service.onAtualizar.subscribe(data => {
			this.load(data);
		});
	}


	load(filtro : DashBoardFiltro) {
		this.filtroItem = filtro;
		this._service.getTransportadoresTotal(filtro).subscribe(data =>{
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	downloadTransportadorTotal(){
		this._service.getTransportadoresTotalDownload(this.filtroItem).subscribe((response: any)  =>{			
			let blob = new Blob([response], { type: response.type });
			fs.saveAs(blob, 'Transportadores_Total.xlsx');
			this._alertService.show("Sucesso.", "Arquivo baixado com sucesso.", 'success');
			this.viewLoading = false;
		}, error => {
			this._alertService.show("Erro.", "Ocorreu um erro ao gerar arquivo.", 'error');
			this.viewLoading = false;
		});
	}
  }