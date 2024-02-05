import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import * as fs from 'file-saver';
import { MatTableDataSource } from '@angular/material/table';
import { AlertService } from '../../../../../core/services/alert.service';
import { AtualizacaoTabelasCorreiosService } from '../../../../../core/services/atualizacaoTabelasCorreios.service';
import { TabelaCorreiosArquivos } from '../../../../../core/models/Fusion/tabelaCorreiosArquivos';


@Component({
	selector: 'm-atualizacaoTabelasCorreios',
	styleUrls: ['./atualizacaoTabelasCorreios.list.component.scss'],
	templateUrl: './atualizacaoTabelasCorreios.list.component.html'
})
export class AtualizacaoTabelasCorreiosListComponent implements OnInit {

	dataSource: MatTableDataSource<TabelaCorreiosArquivos>;
	displayedColumns: string[] = ['id', 'tabelaArquivoStatusId', 'url', 'criacao', 'importacaoDados', 
	'atualizacaoTabelas', 'actions'];
	viewLoading: boolean = false;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: AtualizacaoTabelasCorreiosService, public dialog: MatDialog,
		private _alertService: AlertService, private cdr: ChangeDetectorRef) {

	}

	ngOnInit() {
		this.pesquisar();
	}

	pesquisar() {
		this._service.getFilter(new TabelaCorreiosArquivos(), this.start, this.size).subscribe(result => {
			this.dataSource = new MatTableDataSource(result);
			this.dataSource.data = result.data;
			this.resultsLength = result.total;
			this.dataSource.paginator = this.paginator;

			this.viewLoading = false;
			this.cdr.detectChanges();
		});
	}

	pageChange(event) {
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}

	inputChange(fileInputEvent: any) {
		let file = fileInputEvent.target.files[0];
		if (file) {
			const formData = new FormData();
			formData.append('file', file);
			this.viewLoading = true;
			this._service.importarArquivo(formData).subscribe(() => {
				this._alertService.show("Sucesso", "Arquivo importado com sucesso!", 'success');
				this.viewLoading = false;
				this.pesquisar();
			}, (error) => {
				if (error) this._alertService.show("Error", "Erro ao importar arquivo. Erro: " + error.error, 'error');
				else this._alertService.show("Error", "Erro ao importar arquivo arquivo.", 'error');

				this.viewLoading = false;
				this.pesquisar();
			});
		}
	}

	reprocessar(model: TabelaCorreiosArquivos) {
		model.TabelaArquivoStatusId = 1;
		model.Erro = null;

		this._service.save(model).subscribe((res: any) => {	
			this._alertService.show("Sucesso.", "Arquivo inserido para reprocessamento.", 'success');
			this.viewLoading = false;

			this.pesquisar();
		}, error => {
			this.viewLoading = false;
		});
	}

	exibirDado(erro : string){
		this._alertService.show("Erro", erro, 'warning');
	}

	openInNewTab(url : string) {
		window.open(url, 'noopener');
	}
}

