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
import { ContratoTransportadorRegra } from '../../../../../../../core/models/contratoTransportadorRegra';
import { ContratoTransportadorService } from '../../../../../../../core/services/contratoTransportador.service';
import { ContratoTransportadorRegraEditComponent } from '../edit/regra.edit.component';
import { ContratoTransportador } from '../../../../../../../core/models/contratoTransportador';
import { ConciliacaoTipo } from '../../../../../../../core/models/conciliacaoTipo';
import { ConciliacaoTipoService } from '../../../../../../../core/services/conciliacaoTipo.service';
import { ContratoTransportadorRegraFiltro } from '../../../../../../../core/models/Filters/contratoTransportadorRegraFiltro.model';

@Component({
	selector: 'm-regra-list',
	styleUrls: ['./regra.list.component.scss'],
	templateUrl: './regra.list.component.html'
})
export class ContratoTransportadorRegraListComponent implements OnInit {
	@Input() value: ContratoTransportador;
	@Output() valueChange = new EventEmitter<ContratoTransportador>();
	dataSource: MatTableDataSource<ContratoTransportadorRegra> = new MatTableDataSource(new Array<ContratoTransportadorRegra>());
	displayedColumns: string[] = ['id', 'sigla', 'tipoCondicao', 'operacao', 'valor', 'actions'];
	viewLoading: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;
	ocorrencias: any[];
	tipoConciliacao: Array<ConciliacaoTipo> = new Array<ConciliacaoTipo>();
	ocorrenciaId: number;
	filtro: ContratoTransportadorRegraFiltro = new ContratoTransportadorRegraFiltro();
	listaIncial: Array<ContratoTransportadorRegra> = new Array<ContratoTransportadorRegra>();
	listaFiltrada: Array<ContratoTransportadorRegra> = new Array<ContratoTransportadorRegra>();

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: ContratoTransportadorService,
		private _conciliacaoTipoService: ConciliacaoTipoService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) {
	}

	ngOnInit() {
		this.filtro.transportadorId = this.value.transportadorId;
		this._service.obterRegras(this.filtro).subscribe(generalidades => {
			this.value.contratoTransportadorRegra = generalidades;
			if (this.value.contratoTransportadorRegra == null || this.value.contratoTransportadorRegra == undefined) {
				this.value.contratoTransportadorRegra = new Array<ContratoTransportadorRegra>();
			}
			this.load();
		});
	}

	load() {
		if (this.value.id > 0) {
			this.dataSource = new MatTableDataSource(this.value.contratoTransportadorRegra);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		}
		else {
			if (this.value.contratoTransportadorRegra == null || this.value.contratoTransportadorRegra == undefined) {
				this.value.contratoTransportadorRegra = new Array<ContratoTransportadorRegra>();
			}
			this.dataSource = new MatTableDataSource(this.value.contratoTransportadorRegra);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		}

		this._service.obterOcorrencias().subscribe(data => {
			this.ocorrencias = data;
			this.viewLoading = false;
		});

		this._conciliacaoTipoService.get().subscribe(data => {
			this.tipoConciliacao = data;
			this.viewLoading = false
		})
	}

	pesquisar() {
		this.viewLoading = true;
		this.filtro.transportadorId = this.value.transportadorId;
		this._service.obterRegras(this.filtro).subscribe(regras => {
			this.dataSource = new MatTableDataSource(regras);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
			this.viewLoading = false;
		});
	}

	pageChange(event) {
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}

	novo() {
		var model = new ContratoTransportadorRegra();
		model.transportadorId = this.value.transportadorId;

		const dialogRef = this.dialog.open(ContratoTransportadorRegraEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (res) {
				res.forEach(r => {
					this.value.contratoTransportadorRegra.push(r);
				});

				this._alertService.show("Sucesso", "Regra adicionada a lista com sucesso. Para finalizar a inclusão, clique em salvar.", 'success');
				this.alterouLista();
				this.load();
			}
		});
	}

	edit(model: ContratoTransportadorRegra) {
		const dialogRef = this.dialog.open(ContratoTransportadorRegraEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}

			this._alertService.show("Sucesso", "Regra alterada na lista com sucesso. Para finalizar a edição, clique em salvar.", 'success');
			this.alterouLista();
			this.load();
		});
	}

	delete(model: ContratoTransportadorRegra) {
		if (model.id == 0) {
			var indiceObjeto = this.value.contratoTransportadorRegra.indexOf(model);
			this.value.contratoTransportadorRegra.splice(indiceObjeto, 1)
			this._alertService.show("Sucesso", "Regra removida da lista com sucesso. Para finalizar a remoção, clique em salvar.", 'success');
			this.load();
		}
		else {
			this._alertService.confirmationMessage("", `Essa regra será removida diretamente do banco pos já estava salva. Deseja realmente deletar essa regra? `, 'Confirmar', 'Cancelar').then((result) => {
				if (result.value) {
					model.ativo = false;
					var listModel = new Array<ContratoTransportadorRegra>(model);
					this._service.saveRegras(listModel).subscribe(r => {
						this._alertService.show("Sucesso", "Regra deletada com sucesso.", 'success');

						var indiceObjeto = this.value.contratoTransportadorRegra.indexOf(model);
						this.value.contratoTransportadorRegra.splice(indiceObjeto, 1)
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

