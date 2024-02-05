import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Input,
} from '@angular/core';

import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { AlertService } from '../../../../../core/services/alert.service';
import { EmpresaTransporteConfiguracaoEditComponent } from '../edit/empresaTransporteConfiguracao.edit.component';
import { EmpresaTransporteConfiguracao } from '../../../../../core/models/Fusion/empresaTransporteConfiguracao.model';
import { EmpresaTransporteTipoItem } from '../../../../../core/models/Fusion/empresaTransporteTipoItem.model';
import { EmpresaTransporteConfiguracaoService } from '../../../../../core/services/empresaTransporteConfiguracao.service';
import { EmpresaTransporteConfiguracaoItem } from '../../../../../core/models/Fusion/empresaTransporteConfiguracaoItem.model';
import { EmpresaTransporteConfiguracaoItemListComponent } from '../item/empresaTransporteConfiguracaoItem.list.component';

@Component({
	selector: 'm-empresatransporteconfiguracao',
	templateUrl: './empresaTransporteConfiguracao.list.component.html',
	styleUrls: ['./empresaTransporteConfiguracao.list.component.scss'],
})
export class EmpresaTransporteConfiguracaoListComponent implements OnInit {
	dataSource: MatTableDataSource<EmpresaTransporteConfiguracao>;
	displayedColumns: string[] = ['id', 'transporteTipo', 'transportadorNome', 'codigoContrato', 'codigoIntegracao', 'codigoCartao', 'vigenciaInicial', 'vigenciaFinal',
		'producao', 'valido', 'actions'];
	viewLoading: boolean = false;
	@Input() value: EmpresaTransporteTipoItem;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	constructor(
		private _service: EmpresaTransporteConfiguracaoService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) {

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
			this.viewLoading = false;
			this.cdr.detectChanges();
		}, error => {
			if (error) this._alertService.show("Error", "Houve um erro ao carregar: " + error.error, 'error');
			else this._alertService.show("Error", "Houve um erro ao carregar.", 'error');
			this.viewLoading = false;
			this.cdr.detectChanges();
		});
	}

	novo() {
		var model = new EmpresaTransporteConfiguracao();
		const dialogRef = this.dialog.open(EmpresaTransporteConfiguracaoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (res) {
				this.load();
			}
		});
	}

	edit(model: EmpresaTransporteConfiguracao) {
		let modelData = Object.assign({}, model);
		const dialogRef = this.dialog.open(EmpresaTransporteConfiguracaoEditComponent, { data: { model: modelData } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: EmpresaTransporteConfiguracao) {
		if (model.id == 0) {
			var indiceObjeto = this.value.empresaTransporteConfiguracoes.indexOf(model);
			this.value.empresaTransporteConfiguracoes.splice(indiceObjeto, 1)
			this._alertService.show("Sucesso", "Acesso deletado com sucesso.", 'success');
			this.load();
		}
		else {
			this._alertService.confirmationMessage("", `Deseja realmente deletar o dado de acesso "${model.id}"?`, 'Confirmar', 'Cancelar')
				.then((result) => {
					if (result.value) {
						this._service.delete(model.id).subscribe(r => {
							this._alertService.show("Sucesso", "Dado de acesso deletado com sucesso.", 'success');
							this.load();
						});
					}
				});
		}
	}

	testarIntegracao(dadosTeste: EmpresaTransporteConfiguracao) {
		this.viewLoading = true;
		this._service.testeIntegracao(dadosTeste).subscribe(
			(res) => {
				this.edit(res);
				if (res.valido)
					this._alertService.show("Sucesso", "Integração validada com Sucesso !", 'success');
				else this._alertService.show("Error", "Houve um erro ao validar Integracao !", 'error');

				this.viewLoading = false;
				this.cdr.detectChanges();
			}, (error) => {
				if (error) this._alertService.show("Error", "Houve um erro ao validar Integracao", 'error');
				else this._alertService.show("Error", "Houve um erro ao validar Integracao", 'error');
				this.viewLoading = false;
			});
	}

	listarServicos(empresaTransporteConfiguracaoId: number) {
		var model = new EmpresaTransporteConfiguracaoItem();
		model.empresaTransporteConfiguracaoId = empresaTransporteConfiguracaoId;
		delete model.id;
		const dialogRef = this.dialog
			.open(EmpresaTransporteConfiguracaoItemListComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
		});
	}
}