import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Input,
	Output,
	EventEmitter
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

import { AlertService } from '../../../../../../core/services/alert.service';
import { Integracao } from '../../../../../../core/models/Fusion/integracao';
import { IntegracaoService } from '../../../../../../core/services/integracao.service';
import { IntegracaoEditComponent } from '../edit/integracao.edit.component';
import { TesteIntegracao } from '../../../../../../core/models/Fusion/testeIntegracao';
import { TesteIntegracaoComponent } from './testeIntegracao/testeIntegracao.component';
import { EmpresaIntegracao } from '../../../../../../core/models/Fusion/empresaIntegracao';

@Component({
	selector: 'm-integracao',
	templateUrl: './integracao.list.component.html',
	styleUrls: ['./integracao.list.component.scss'],
})
export class IntegracaoListComponent implements OnInit{
	dataSource: MatTableDataSource<Integracao>;
	displayedColumns: string[] = ['id', 'url', 'verbo', 'lote', 'layoutHeader', 'layout',
	 'producao', 'envioBody', 'envioConfigId', 'integracaoGatilho', 'actions'];
	viewLoading: boolean = false;
	@Input() value : EmpresaIntegracao;
	@Input() isView : boolean;
	@Output() valueChange = new EventEmitter<EmpresaIntegracao>();

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(
		private _service: IntegracaoService, 
		public dialog: MatDialog, 
		private cdr: ChangeDetectorRef, 
		private _alertService : AlertService){
		
	}
	ngOnInit() {
		this.load();
	}

	load() {
		this.dataSource = new MatTableDataSource(this.value.listaIntegracoes);
		this.dataSource.sort = this.sort;
		this.dataSource.paginator = this.paginator;
		this.cdr.detectChanges();
	}

	novo() {
		var model = new Integracao();
		const dialogRef = this.dialog.open(IntegracaoEditComponent, {data: { model}});
		dialogRef.afterClosed().subscribe(res => {
			if(res)
			{
				this.value.listaIntegracoes.push(res);
				this.alterouLista();
				this.load();
			}
		});
	}

	edit(model: Integracao) {
		const dialogRef = this.dialog.open(IntegracaoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: Integracao) {
		if(model.id == 0){
			var indiceObjeto = this.value.listaIntegracoes.indexOf(model);
			this.value.listaIntegracoes.splice(indiceObjeto, 1)
			this._alertService.show("Sucesso","Consumo deletado com sucesso.",'success');
			this.load();
		}
		else{
			this._alertService.confirmationMessage("",`Deseja realmente deletar o consumo "${model.id}"?`,'Confirmar','Cancelar').then((result) => {
				if (result.value) {
					this._service.delete(model.id).subscribe(r=> {
						this._alertService.show("Sucesso","Consumo deletado com sucesso.",'success');
						this.load();
					});
				}
			});
		}
	}

	alterouLista(){
		this.valueChange.emit(this.value);
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	visualizarDado(dado){
		this._alertService.show("Url", JSON.stringify(dado, undefined, 4) ,'info');
	}

	copiarTexto(texto : string) {
		if (texto) {
			const selBox = document.createElement('textarea');			
			selBox.style.position = 'absolute';
			selBox.style.left = '0';
			selBox.style.top = '0';
			selBox.style.opacity = '0';
			selBox.value = texto;
			selBox.setAttribute('readonly', '');
			document.body.appendChild(selBox);
			selBox.focus();
			selBox.select();
			document.execCommand('copy');
			document.body.removeChild(selBox);			
		}
	}

	testarIntegracao(Integracao : any){
		let model = this.value;
		model.listaIntegracoes = Integracao;

		const dialogRef = this.dialog.open(TesteIntegracaoComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
		});
	}
  }