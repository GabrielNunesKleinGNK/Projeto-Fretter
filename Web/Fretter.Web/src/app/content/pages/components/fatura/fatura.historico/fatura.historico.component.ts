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
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { FaturaHistoricoService } from "../../../../../core/services/faturaHistorico.service";
import { AlertService } from '../../../../../core/services/alert.service';
import { FaturaHistorico } from '../../../../../core/models/faturaHistorico';

@Component({
  selector: 'm-faturaHistorico',
  templateUrl: './fatura.historico.component.html',
  styleUrls: ['./fatura.historico.component.scss']
})
export class FaturaHistoricoComponent implements OnInit {
	dataSource: MatTableDataSource<FaturaHistorico>;
	displayedColumns: string[] = [ 'Id', 'Descricao', 'Motivo', 'StatusAnterior','Status', 'DataCadastro'];
	@Input() faturaId : number;
	viewLoading: boolean = false;

  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: FaturaHistoricoService, public dialog: MatDialog, private cdr: ChangeDetectorRef, private _alertService : AlertService
		){
		
	}
	ngOnInit() {
		this.load();
		
	}

	load() {
		this._service.getHistoricoFaturasDaEmpresa(this.faturaId).subscribe(data => {
			let model = data;
			model.forEach(historico => {
				let textoSeparado = historico.descricao.split('Motivo:');
				if(textoSeparado.length > 1){
					historico.descricao = textoSeparado[0];
					historico.motivo = textoSeparado[1];
				}
				
			});

			this.dataSource = new MatTableDataSource(model);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		 });
	}
}

