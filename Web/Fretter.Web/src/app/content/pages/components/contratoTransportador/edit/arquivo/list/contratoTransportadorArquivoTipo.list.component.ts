import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Inject,
	Input,
	Output,
	EventEmitter
} from '@angular/core';

import { MatPaginator, MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertService } from '../../../../../../../core/services/alert.service';
import { ContratoTransportador } from '../../../../../../../core/models/contratoTransportador';
import { ContratoTransportadorArquivoTipo } from '../../../../../../../core/models/contratoTransportadorArquivoTipo';
import { ImportacaoArquivoTipo } from '../../../../../../../core/models/importacaoArquivoTipo';
import { ImportacaoArquivoTipoService } from '../../../../../../../core/services/importacaoArquivoTipo.service';
import { ContratoTransportadorArquivoTipoEditComponent } from '../edit/contratoTransportadorArquivoTipo.edit.component';
import { ContratoTransportadorArquivoTipoService } from '../../../../../../../core/services/contratoTransportadorArquivoTipo.service';
import { ContratoTransportadorFiltro } from '../../../../../../../core/models/Filters/contratoTransportadorFiltro';
import { combineLatest } from 'rxjs';
import { ImportacaoArquivoTipoItemService } from '../../../../../../../core/services/importacaoArquivoTipoItem.service';
import { ImportacaoArquivoTipoItem } from '../../../../../../../core/models/importacaoArquivoTipoItem';


@Component({
	selector: 'm-contratoTransportadorArquivoTipo-list',
	styleUrls: ['./contratoTransportadorArquivoTipo.list.component.scss'],
	templateUrl: './contratoTransportadorArquivoTipo.list.component.html'
})
export class ContratoTransportadorArquivoTipoListComponent implements OnInit {
	@Input() value: ContratoTransportador;
	@Output() valueChange = new EventEmitter<ContratoTransportador>();
	dataSource: MatTableDataSource<ContratoTransportadorArquivoTipo> = new MatTableDataSource(new Array<ContratoTransportadorArquivoTipo>());
	displayedColumns: string[] = ['id', 'importacaoArquivoTipoId', 'importacaoArquivoTipoItemId', 'alias', 'dataCadastro', 'actions'];
	viewLoading: boolean = true;
	filtro: ContratoTransportadorFiltro = new ContratoTransportadorFiltro();
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;

	arquivoTipoItem: ImportacaoArquivoTipoItem = new ImportacaoArquivoTipoItem();
	tipoArquivo: Array<ImportacaoArquivoTipo> = new Array<ImportacaoArquivoTipo>();
	listaIncial: Array<ContratoTransportadorArquivoTipo> = new Array<ContratoTransportadorArquivoTipo>();
	listaFiltrada: Array<ContratoTransportadorArquivoTipo> = new Array<ContratoTransportadorArquivoTipo>();
	filtroInterno: any = {
		tipoArquivo: 0
	};

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: ContratoTransportadorArquivoTipoService,
		private _serviceArquivoTipo: ImportacaoArquivoTipoService,
		private _serviceArquivoTipoItem: ImportacaoArquivoTipoItemService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) {
	}
	ngOnInit() {
		this.filtro.transportadorId = this.value.transportadorId;
		const contratoTransportadorArquivoTipo$ = this._service.getFilter(this.filtro, this.start, this.size);
		const tipoArquivo$ = this._serviceArquivoTipo.get();


		combineLatest([contratoTransportadorArquivoTipo$, tipoArquivo$])
			.subscribe(([contratoTransportadorArquivoTipoData, tipoArquivoData]) => {

				if (this.value.id > 0)
					this.value.contratoTransportadorArquivoTipo = contratoTransportadorArquivoTipoData.data;

				if (this.value.contratoTransportadorArquivoTipo == null || this.value.contratoTransportadorArquivoTipo == undefined) {
					this.value.contratoTransportadorArquivoTipo = new Array<ContratoTransportadorArquivoTipo>();
				}

				this.value.contratoTransportadorArquivoTipo.forEach(x => {
					var t = tipoArquivoData.filter(f => f.id == x.importacaoArquivoTipoItem.importacaoArquivoTipoId)
					x.tipoArquivo = t[0].nome;

					if (x.importacaoArquivoTipoItem != null) {
						this._serviceArquivoTipoItem.getById(x.importacaoArquivoTipoItem.id).subscribe(item => {
							x.tipoCobranca = item.conciliacaoTipo.nome;
						});
					}
				});

				this.tipoArquivo = tipoArquivoData;
				this.viewLoading = false

				this.alterouLista();
				this.load();
			});
	}

	load() {
		if (this.value.id > 0) {
			this.dataSource = new MatTableDataSource(this.value.contratoTransportadorArquivoTipo);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		}
		else {
			if (this.value.contratoTransportadorArquivoTipo == null || this.value.contratoTransportadorArquivoTipo == undefined) {
				this.value.contratoTransportadorArquivoTipo = new Array<ContratoTransportadorArquivoTipo>();
			}
			this.dataSource = new MatTableDataSource(this.value.contratoTransportadorArquivoTipo);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		}
	}

	pesquisar() {
		if (this.listaIncial.length == 0) {
			this.listaIncial = this.value.contratoTransportadorArquivoTipo
		}

		if (this.filtroInterno.tipoArquivo > 0) {
			this.listaFiltrada = this.listaIncial;
			if (this.filtroInterno.tipoArquivo > 0) {
				this.listaFiltrada = this.listaFiltrada
					.filter(x => x.importacaoArquivoTipoItem.importacaoArquivoTipoId == this.filtroInterno.tipoArquivo);
			}

			this.value.contratoTransportadorArquivoTipo = this.listaFiltrada;
		}
		else {
			this.value.contratoTransportadorArquivoTipo = this.listaIncial
		}

		this.load();
	}

	pageChange(event) {
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}

	novo() {
		var model = new ContratoTransportadorArquivoTipo();
		model.transportadorId = this.value.transportadorId;

		const dialogRef = this.dialog.open(ContratoTransportadorArquivoTipoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (res) {
				res.forEach(r => {
					this._serviceArquivoTipoItem.getById(r.importacaoArquivoTipoItemId).subscribe(item => {
						r.importacaoArquivoTipoItem = null;
						r.tipoCobranca = item.conciliacaoTipo.nome;
						r.tipoArquivo = this.tipoArquivo.filter(f => f.id == item.importacaoArquivoTipoId)[0].nome
						this.value.contratoTransportadorArquivoTipo.push(r);

						this._alertService.show("Sucesso", "De/Para adicionado a lista com sucesso. Para finalizar a inclusão, clique em salvar.", 'success');
						this.alterouLista();
						this.load();
					});
				});
			}
		});
	}

	edit(model: ContratoTransportadorArquivoTipo) {
		const dialogRef = this.dialog.open(ContratoTransportadorArquivoTipoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}

			this._alertService.show("Sucesso", "De/Para alterado na lista com sucesso. Para finalizar a edição, clique em salvar.", 'success');
			this.alterouLista();
			this.load();
		});
	}

	delete(model: ContratoTransportadorArquivoTipo) {
		if (model.id == 0) {
			var indiceObjeto = this.value.contratoTransportadorArquivoTipo.indexOf(model);
			this.value.contratoTransportadorArquivoTipo.splice(indiceObjeto, 1)
			this._alertService.show("Sucesso", "De/Para removido da lista com sucesso. Para finalizar a remoção, clique em salvar.", 'success');
			this.load();
		}
		else {
			this._alertService.confirmationMessage("", `Esse De/Para será removido diretamente do banco pos já estava salva. Deseja realmente deletar essa regra? `, 'Confirmar', 'Cancelar').then((result) => {
				if (result.value) {
					model.ativo = false;
					this._service.delete(model.id).subscribe(r => {
						this._alertService.show("Sucesso", "De/Para deletado com sucesso.", 'success');

						var indiceObjeto = this.value.contratoTransportadorArquivoTipo.indexOf(model);
						this.value.contratoTransportadorArquivoTipo.splice(indiceObjeto, 1)
						this.load();
					});
				}
			});
		}

	}

	alterouLista() {
		this.valueChange.emit(this.value);
	}
}

