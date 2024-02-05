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

import { OcorrenciaArquivo } from '../../../../../../core/models/Fusion/ocorrenciaArquivo.model';
import { OcorrenciaArquivoService } from '../../../../../../core/services/ocorrenciaArquivo.service';

@Component({
	selector: 'm-ocorrenciaarquivo',
	styleUrls: ['./ocorrenciaArquivo.list.component.scss'],
	templateUrl: './ocorrenciaArquivo.list.component.html'
})
export class OcorrenciaArquivoListComponent implements OnInit {
	dataSource: MatTableDataSource<OcorrenciaArquivo>;
	displayedColumns: string[] = ['id', 'nomeArquivo', 'retorno', 'qtAdvertencia', 'percentualAtualizacao', 'ultimaAtualizacao', 'atualizar'];
	viewLoading: boolean = true;
	filtro: any;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	constructor(
		//@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: OcorrenciaArquivoService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) { 
			this.filtro = {
				id: null
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
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}
}
