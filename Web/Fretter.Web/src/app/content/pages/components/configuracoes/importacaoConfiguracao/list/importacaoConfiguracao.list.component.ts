import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ImportacaoConfiguracao } from '../../../../../../core/models/importacaoConfiguracao.model';
import { AlertService } from '../../../../../../core/services/alert.service';
import { ImportacaoConfiguracaoService } from '../../../../../../core/services/importacaoConfiguracao.service';
import { ImportacaoConfiguracaoEditComponent } from '../edit/importacaoConfiguracao.edit.component';


@Component({
	selector: 'm-importacaoconfiguracao',
	templateUrl: './importacaoConfiguracao.list.component.html'
})
export class ImportacaoConfiguracaoListComponent implements OnInit {
	dataSource: MatTableDataSource<ImportacaoConfiguracao>;
	displayedColumns: string[] = ['id', 'importacaoConfiguracaoTipoId', 'empresaId', 'transportadorId', 'importacaoArquivoTipoId',
		'diretorio', 'usuario', 'dataCadastro', 'compactado', 'actions'];
	viewLoading: boolean = true;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: ImportacaoConfiguracaoService, public dialog: MatDialog, private cdr: ChangeDetectorRef, private _alertService: AlertService) {

	}
	ngOnInit() {
		this.load();
	}

	load() {
		this.viewLoading = true;
		this._service.get().subscribe(data => {
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
			this.viewLoading = false;
		}, error => {
			if (error) this._alertService.show("Error", "Houve um erro ao carregar os dados: " + error.error, 'error');
			else this._alertService.show("Error", "Houve um erro ao carregar os dados.", 'error');

			this.viewLoading = false;
		});
	}


	novo() {
		var importacaoConfiguracao = new ImportacaoConfiguracao();
		const dialogRef = this.dialog.open(ImportacaoConfiguracaoEditComponent, { data: { model: importacaoConfiguracao } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: ImportacaoConfiguracao) {
		const dialogRef = this.dialog.open(ImportacaoConfiguracaoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: ImportacaoConfiguracao) {
		this._alertService.confirmationMessage("", `Deseja realmente inativar o ImportacaoConfiguracao "${model.id}"?`, 'Confirmar', 'Cancelar').then((result) => {
			if (result.value) {
				this._service.delete(model.id).subscribe(r => {
					this._alertService.show("Sucesso", "ImportacaoConfiguracao inativado com sucesso.", 'success');
					this.load();
				});
			}
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}
}
