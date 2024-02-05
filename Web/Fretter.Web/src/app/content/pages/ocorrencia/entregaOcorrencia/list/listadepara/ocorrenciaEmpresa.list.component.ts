import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertService } from '../../../../../../core/services/alert.service';
import { OcorrenciaEmpresa } from '../../../../../../core/models/ocorrenciaEmpresa.model';
import { EntregaOcorrenciaService } from '../../../../../../core/services/entregaOcorrencia.service';

@Component({
	selector: 'm-ocorrenciaEmpresa',
	templateUrl: './ocorrenciaEmpresa.list.component.html'
})
export class OcorrenciaEmpresaListComponent implements OnInit {
	dataSource: MatTableDataSource<OcorrenciaEmpresa>;
	displayedColumns: string[] = ['id', 'descricao', 'sigla', 'finalizadora'];
	viewLoading: boolean = true;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	constructor(
		private _service: EntregaOcorrenciaService, 
		public dialog: MatDialog, 
		private cdr: ChangeDetectorRef, 
		private _alertService: AlertService) {}

	ngOnInit() {
		this.load();
	}

	load() {
		this.viewLoading = true;
		this._service.getDePara().subscribe(data => {
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
			this.viewLoading = false;
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}
}
