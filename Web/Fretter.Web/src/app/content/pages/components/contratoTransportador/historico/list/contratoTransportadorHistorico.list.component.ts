import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Inject
} from '@angular/core';

import { MatPaginator, MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { MatSort} from '@angular/material/sort';
import { MatTableDataSource} from '@angular/material/table';
import { ContratoTransportador } from '../../../../../../core/models/contratoTransportador';
import { ContratoTransportadorHistorico } from '../../../../../../core/models/contratoTransportadorHistorico';
import { ContratoTransportadorHistoricoFiltro } from '../../../../../../core/models/Filters/contratoTransportadorHistoricoFiltro';
import { AlertService } from '../../../../../../core/services/alert.service';
import { ContratoTransportadorHistoricoService } from '../../../../../../core/services/contratoTransportadorHistorico.service';
import { ContratoTransportadorHistoricoEditComponent } from '../edit/contratoTransportadorHistorico.edit.component';

@Component({
  selector: 'm-contratoTransportadorHistorico',
  styleUrls: ['./contratoTransportadorHistorico.list.component.scss'],
  templateUrl: './contratoTransportadorHistorico.list.component.html'
})
export class ContratoTransportadorHistoricoListComponent implements OnInit {
	
	dataSource: MatTableDataSource<ContratoTransportador> = new MatTableDataSource(new Array<ContratoTransportador>());
	displayedColumns: string[] = ['id', 'transportador', 'descricao', 'cnpj', 'usuarioCadastro', 'dataCadastro', 'actions'];	
	filtro: ContratoTransportadorHistoricoFiltro = new ContratoTransportadorHistoricoFiltro();
	viewLoading: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(@Inject(MAT_DIALOG_DATA) public data: any,
				private _service: ContratoTransportadorHistoricoService, 
				public dialog: MatDialog,
				private cdr: ChangeDetectorRef, 
				private _alertService : AlertService){
		this.filtro.contratoTransportadorId = data.contratoTransportadorId;
	}
	ngOnInit() {
		this.load();
	}

	load() {
		this.pesquisar();
	}

	pesquisar(){
		this.viewLoading = true;
		this._service.getFilter(this.filtro, this.start, this.size).subscribe(result => {
			this.dataSource.data = result.data;
			this.resultsLength = result.total;
			this.viewLoading = false;
		});
	}
	
	pageChange(event){
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}
	
	edit(model: ContratoTransportadorHistorico) {
		const dialogRef = this.dialog.open(ContratoTransportadorHistoricoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}
}

