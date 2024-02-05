import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Input,
	Inject
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { AlertService } from '../../../../../core/services/alert.service';
import { EntregaDevolucaoOcorrencia } from '../../../../../core/models/Fusion/entregaDevolucaoOcorrencia.model';
import { EntregaDevolucaoOcorrenciaService } from '../../../../../core/services/entregaDevolucaoOcorrencia.service';

@Component({
  selector: 'm-entregaDevolucaoOcorrencia',
  templateUrl: './entregaDevolucao.ocorrencia.component.html',
  styleUrls: ['./entregaDevolucao.ocorrencia.component.scss']
})
export class EntregaDevolucaoOcorrenciaComponent implements OnInit {
	model : EntregaDevolucaoOcorrencia;
	dataSource: MatTableDataSource<EntregaDevolucaoOcorrencia>;
	displayedColumns: string[] = [ 'id', 'entregaDevolucao', 'ocorrencia', 'observacao','sigla', 'dataOcorrencia'];
	viewLoading: boolean = false;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	constructor(
		public dialogRef: MatDialogRef<EntregaDevolucaoOcorrenciaComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: EntregaDevolucaoOcorrenciaService,
		private _alertService: AlertService,
		public dialog: MatDialog, 
		private cdr: ChangeDetectorRef, 
		
	){}

	ngOnInit() {
		this.load();
	}

	load() {
		this.dataSource = new MatTableDataSource(this.data.model);
		this.dataSource.sort = this.sort;
		this.dataSource.paginator = this.paginator;
		this.cdr.detectChanges();
	}

	atualizarListaDeOcorrencias(){
		this._service.getHistoricoEntregaDevolucao(this.data.entregaDevolucaoId).subscribe(data => {
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		});
	}

	getTitle(): string {
		//var idEntrega = !this.data.model[0] ? "" : this.data.model[0].entregaDevolucao;
		return `Ocorrências da entrega devolução: ${this.data.entregaDevolucaoId}`;
	}
	
	close() {
		this.dialogRef.close();
	}
}

