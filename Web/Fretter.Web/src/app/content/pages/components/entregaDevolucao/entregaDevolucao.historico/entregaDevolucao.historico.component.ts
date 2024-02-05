import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Input
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertService } from '../../../../../core/services/alert.service';
import { EntregaDevolucaoHistorico } from '../../../../../core/models/Fusion/entregaDevolucaoHistorico.model';
import { EntregaDevolucaoHistoricoService } from '../../../../../core/services/entregaDevolucaoHistorico.service';
import { EntregaDevolucaoHistoricoJsonComponent } from '../entregaDevolucao.historicoJson/entregaDevolucao.historicoJson.component';
import { EntregaDevolucaoHistoricoXMLComponent } from '../entregaDevolucao.historicoXML/entregaDevolucao.historicoXML.component';

@Component({
	selector: 'm-entregaDevolucaoHistorico',
	templateUrl: './entregaDevolucao.historico.component.html',
	styleUrls: ['./entregaDevolucao.historico.component.scss']
})
export class EntregaDevolucaoHistoricoComponent implements OnInit {
	dataSource: MatTableDataSource<EntregaDevolucaoHistorico>;
	displayedColumns: string[] = ['entregaDevolucaoId', 'codigoColeta', 'codigoRastreio', 'mensagem', 'xmlRetorno',
		'entregaDevolucaoStatusAnteriorId', 'entregaDevolucaoStatusAtualId', 'inclusao'];
	@Input() entregaDevolucaoId: number;
	viewLoading: boolean = false;


	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: EntregaDevolucaoHistoricoService, public dialog: MatDialog, private cdr: ChangeDetectorRef, private _alertService: AlertService
	) {

	}
	ngOnInit() {
		this.load();
	}

	load() {
		this._service.getHistoricoEntregaDevolucao(this.entregaDevolucaoId).subscribe(data => {
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		});
	}

	json(jsonData: any) {
		const dialogRef = this.dialog.open(EntregaDevolucaoHistoricoJsonComponent, { data: { jsonData } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	xml(xmlData: any) {
		const dialogRef = this.dialog.open(EntregaDevolucaoHistoricoXMLComponent, { data: { xmlData } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}
}

