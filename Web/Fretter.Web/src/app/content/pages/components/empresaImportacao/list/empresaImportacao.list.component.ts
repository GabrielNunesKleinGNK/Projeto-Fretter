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
import { EmpresaService } from '../../../../../core/services/empresa.service';
import { EmpresaImportacaDetalheComponent } from './detalhe/empresaImportacao.detalhe.component';
import { EmpresaImportacao } from '../../../../../core/models/empresaImportacao.model';
import { EmpresaImportacaoFiltro } from '../../../../../core/models/empresaImportacaoFiltro';


@Component({
	selector: 'm-empresaImportacao',
	styleUrls: ['./empresaImportacao.list.component.scss'],
	templateUrl: './empresaImportacao.list.component.html'
})
export class EmpresaImportacaoListComponent implements OnInit {

	dataSource: MatTableDataSource<EmpresaImportacao>;
	displayedColumns: string[] = ['id', 'nome', 'descricao', 'quantidadeEmpresa', 'dataCadastro', 'sucesso', 'actions'];
	viewLoading: boolean = false;
	//Filtro
	filtro: EmpresaImportacaoFiltro;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: EmpresaService, public dialog: MatDialog,
		private _alertService: AlertService, private cdr: ChangeDetectorRef) {

	}

	ngOnInit() {
		this.filtro = new EmpresaImportacaoFiltro();
		this.pesquisar();
	}

	pesquisar() {
		if (this.filtro.cnpj == '' && this.filtro.cnpj == '')
			this.filtro = new EmpresaImportacaoFiltro();

		this._service.obterEmpresaImportacaoArquivo(this.filtro).subscribe(result => {
			this.dataSource = new MatTableDataSource(result);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		});
	}

	limpar() {
		this.filtro = new EmpresaImportacaoFiltro();
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	view(importacaoArquivo) {
		const dialogRef = this.dialog.open(EmpresaImportacaDetalheComponent, { data: { importacaoArquivoId: importacaoArquivo.id }, width: '1200px' });
		dialogRef.afterClosed().subscribe(res => {
			this.pesquisar();
		});
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
				if (error) this._alertService.show("Error", "Erro ao importar arquivo da Empresa! Erro: " + error.error, 'error');
				else this._alertService.show("Error", "Erro ao importar arquivo da Empresa!", 'error');

				this.viewLoading = false;
				this.pesquisar();
			});
		}
	}

	download(arquivoId: number, arquivoNome: string) {
		this._service.download(arquivoId, arquivoNome).subscribe((res: any) => {
			let blob = new Blob([res], { type: res.type });
			fs.saveAs(blob, arquivoNome);

			this._alertService.show("Sucesso.", "Arquivo baixado com sucesso.", 'success');
			this.viewLoading = false;
		}, error => {
			this.viewLoading = false;
		});
	}

	downloadTemplate() {
		this._service.downloadTemplate().subscribe((response: any) => {
			let blob = new Blob([response], { type: response.type });
			fs.saveAs(blob, 'importacao_Empresa.xlsx');
			this._alertService.show("Sucesso.", "Template baixado com sucesso.", 'success');
			this.viewLoading = false;
		}, error => {
			this.viewLoading = false;
		});
	}

}

