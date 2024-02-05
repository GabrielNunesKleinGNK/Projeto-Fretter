import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Input,
	Output,
	EventEmitter,
	Inject
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { EmpresaTransporteConfiguracaoItem } from '../../../../../core/models/Fusion/empresaTransporteConfiguracaoItem.model';
import { AlertService } from '../../../../../core/services/alert.service';
import { EmpresaTransporteConfiguracaoItemService } from '../../../../../core/services/empresaTransporteConfiguracaoItem.service';

@Component({
	selector: 'm-empresatransporteconfiguracaoitem',
	templateUrl: './empresaTransporteConfiguracaoItem.list.component.html'	
})
export class EmpresaTransporteConfiguracaoItemListComponent implements OnInit {
	dataSource: MatTableDataSource<EmpresaTransporteConfiguracaoItem> = new MatTableDataSource(new Array<EmpresaTransporteConfiguracaoItem>());;
	displayedColumns: string[] = ['id', 'codigoServico', 'codigoServicoCategoria',
		'codigoIntegracao', 'nome', 'vigenciaInicial', 'vigenciaFinal', 'dataCadastroServico'];
	viewLoading: boolean = false;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;
	EntregaConfiguracaoId: number;
	@Input() empresaTransporteConfiguracaoId: number;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(
		private _service: EmpresaTransporteConfiguracaoItemService,
		@Inject(MAT_DIALOG_DATA) public data: any,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) {

	}

	ngOnInit() {
		this.load();
		this.EntregaConfiguracaoId = this.data.model.empresaTransporteConfiguracaoId;
	}

	ngAfterContentChecked(): void {
		this.cdr.detectChanges();
	}

	load() {
		this.pesquisar();
	}

	pesquisar() {
		this.viewLoading = true;
		this._service.getFilter(this.data.model, this.start, this.size).subscribe(result => {
			this.dataSource.data = result.data;
			this.resultsLength = result.total;
			this.viewLoading = false;
		});
	}

	pageChange(event) {
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}

	getTitle(): string {
		return `Serviços da configuração  ${this.EntregaConfiguracaoId}`;
	}
}