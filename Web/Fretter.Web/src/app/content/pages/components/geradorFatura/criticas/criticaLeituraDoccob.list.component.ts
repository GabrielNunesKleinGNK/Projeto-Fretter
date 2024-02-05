import { Component,	OnInit,	ViewChild,	ChangeDetectorRef, Input} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ImportacaoFatura } from '../../../../../core/models/importacaoFatura';
import { GeradorFaturaService } from "../../../../../core/services/geradorFatura.service";
import { AlertService } from '../../../../../core/services/alert.service';
import { ImportacaoArquivo } from '../../../../../core/models/importacaoArquivo';
import { ExcelService } from '../../../../../core/services/excel.service';
import moment from 'moment';

@Component({
	selector: 'm-critica-Leitura-Doccob-list',
	templateUrl: './criticaLeituraDoccob.list.component.html',
})
export class CriticaLeituraDoccobListComponent implements OnInit {
	dataSource: MatTableDataSource<any> = new MatTableDataSource(new Array<any>());
	displayedColumns: string[] = ['linha', 'posicao', 'descricao'];
	criticas: Subscription;
	exibirLista: Subscription;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	viewList: boolean = false;
	viewLoadingExcel: boolean = false;

	constructor(
		private _service: GeradorFaturaService,
		private cdr: ChangeDetectorRef,
		private _excelService: ExcelService,
	) {	}

	ngOnInit() {
		this.criticas = this._service.onAtualizarCriticas.subscribe(data => {
			this.criticas = data;
			this.load(data);
		});
		this.exibirLista = this._service.onExibirListaCriticas.subscribe(data =>{
			this.viewList = data;
		});
	}

	load(data: any[]) {
		this.dataSource = new MatTableDataSource(data);
		this.dataSource.sort = this.sort;
		this.dataSource.paginator = this.paginator;
		this.cdr.detectChanges();
	}

	exportarExcel() {
		this.viewLoadingExcel = true;

		let excelData = this.dataSource.data.map(object => ({ ...object }));
		this._excelService.generateExcel(
			`Criticas_Leitura_Doccob_${moment().format('YYYY-MM-DDTHHmm')}`, 
			excelData,
			{
				linha: { titulo: 'Linha' },
				posicao: { titulo: 'Posição' },
				descricao: { titulo: 'Descrição' }
			});

		this.viewLoadingExcel = false;
		this.cdr.detectChanges();
	}
}