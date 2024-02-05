import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

import { AlertService } from '../../../../../../core/services/alert.service';
import { EmpresaIntegracaoFiltro } from '../../../../../../core/models/Fusion/empresaIntegracaoFiltro.model';
import { EmpresaIntegracao } from '../../../../../../core/models/Fusion/empresaIntegracao';
import { EmpresaIntegracaoService } from '../../../../../../core/services/empresaIntegracao.service';
import { EmpresaIntegracaoEditComponent } from '../edit/empresaIntegracao.edit.component';
import { Integracao } from '../../../../../../core/models/Fusion/integracao';
import { IntegracaoService } from '../../../../../../core/services/integracao.service';
import { CanalVenda } from '../../../../../../core/models/Fusion/canalVenda';
import { CanalVendaService } from '../../../../../../core/services/canalVenda.service';

@Component({
	selector: 'm-empresaIntegracao',
	templateUrl: './empresaIntegracao.list.component.html',
	styleUrls: ['./empresaIntegracao.list.component.scss'],
})
export class EmpresaIntegracaoListComponent implements OnInit{
	dataSource: MatTableDataSource<EmpresaIntegracao>;
	filtro: EmpresaIntegracaoFiltro;
	integracaoService: IntegracaoService;
	lstIntgracoes: Array<Integracao>;
	lstCanaisVenda : Array<CanalVenda>;
	displayedColumns: string[] = ['id', 'urlBase'
	//, 'urlToken', 'apiKey', 'usuario', 'senha', 'clientId'
	//,'clientSecret', 'clientScope', 'entregaOrigemImportacaoId',
	,'actions'];
	viewLoading: boolean = false;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: EmpresaIntegracaoService, 
		private _serviceCanalVenda : CanalVendaService,
		public dialog: MatDialog, 
		private cdr: ChangeDetectorRef, 
		private _alertService : AlertService){
		
	}
	ngOnInit() {
		this.load();
	}

	load() {
		this.filtro = new EmpresaIntegracaoFiltro();
		this._service.getEmpresaIntegracao(this.filtro).subscribe(result => {
			this.dataSource = new MatTableDataSource(result.data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		 });

		//  this._serviceCanalVenda.getCanalVendaPorEmpresa(null).subscribe(result => {
		// 	this.lstCanaisVenda = result.data;
		// });
	}

	novo() {
		var model = new EmpresaIntegracao();
		const dialogRef = this.dialog.open(EmpresaIntegracaoEditComponent, {data: { model}});
		dialogRef.afterClosed().subscribe(res => {
			if(!res)
				return;
			this.load();
		});
	}

	edit(model: EmpresaIntegracao) {
		model.visualizar = false;
		const dialogRef = this.dialog.open(EmpresaIntegracaoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			this.load();
		});
	}

	view(model: EmpresaIntegracao) {
		model.visualizar = true;
		const dialogRef = this.dialog.open(EmpresaIntegracaoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: EmpresaIntegracao) {
		this._alertService.confirmationMessage("",`Deseja realmente deletar a integração "${model.id}"?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this._service.delete(model.id).subscribe(r=> {
					this._alertService.show("Sucesso","Integração deletada com sucesso.",'success');
					this.load();
				});
			}
		});
	}
	 
	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}
  }