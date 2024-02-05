import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Inject
} from '@angular/core';

import { MatPaginator, MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertService } from '../../../../../../core/services/alert.service';

import { EntregaOcorrenciaService } from '../../../../../../core/services/entregaOcorrencia.service';
import { EntregaOcorrencia } from '../../../../../../core/models/Fusion/entregaOcorrencia.model';

@Component({
	selector: 'm-ocorrenciaentrega',
	templateUrl: './ocorrenciaEntrega.list.component.html'
})
export class OcorrenciaEntregaListComponent implements OnInit {
	dataSource: MatTableDataSource<EntregaOcorrencia>;
	displayedColumns: string[] = ['id', 'entregaId', 'ocorrencia', 'dataOcorrencia', 'sigla', 'finalizar'];
	viewLoading: boolean = true;
	filtro: any;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	constructor(@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: EntregaOcorrenciaService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) { 
			this.filtro = {
				entregaId: data.entregaId
			};
		}

	ngOnInit() {
		this.load();
	}

	load() {
		this.viewLoading = true;
		this.bindList();
	}

	bindList() {
		this._service.getFilter(this.filtro).subscribe(data => {
			this.dataSource = new MatTableDataSource(data.data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
			this.viewLoading = false;
		}
		// , error => {
		// 	if (error) this._alertService.show("Error", "Houve um erro ao carregar os dados: " + error.error, 'error');
		// 	else this._alertService.show("Error", "Houve um erro ao carregar os dados.", 'error');
		// 	this.viewLoading = false;
		// }
		);
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}
}
