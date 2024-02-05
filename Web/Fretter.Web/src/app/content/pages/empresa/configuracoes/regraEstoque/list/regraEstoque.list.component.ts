import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import { MatSort} from '@angular/material/sort';
import { MatTableDataSource} from '@angular/material/table';
import { combineLatest } from 'rxjs';
import { RegraEstoqueFiltro } from '../../../../../../core/filters/regraEstoqueFiltro';
import { EntregaDevolucaoFiltro } from '../../../../../../core/models/Fusion/entregaDevolucaoFiltro';
import { RegraEstoque } from '../../../../../../core/models/Fusion/regraEstoque';
import { AlertService } from '../../../../../../core/services/alert.service';
import { CanalService } from '../../../../../../core/services/canal.service';
import { RegraEstoqueService } from '../../../../../../core/services/regraEstoque.service';
import { RegraEstoqueEditComponent } from '../edit/regraEstoque.edit.component';

@Component({
  selector: 'm-regraEstoque',
  templateUrl: './regraEstoque.list.component.html'
})
export class RegraEstoqueListComponent implements OnInit {
	
	dataSource: MatTableDataSource<RegraEstoque>;
	displayedColumns: string[] = ['id', 'canalOrigem', 'canalDestino', 'grupo', 'skus', 'dataCadastro', 'actions'];
	filtro: RegraEstoqueFiltro;
	listCanais: any[];

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: RegraEstoqueService, 
				public dialog: MatDialog, 
				private cdr: ChangeDetectorRef, 
				private _alertService : AlertService,
				private _canalService: CanalService){
		
	}
	ngOnInit() {
		this.filtro = new RegraEstoqueFiltro();
		const canais$ = this._canalService.getCanaisPorEmpresa();
		combineLatest([canais$]).subscribe(([canais]) => {
			this.listCanais = canais;
		});
		this.load();
	}

	load() {
		this._service.get().subscribe(data => {
			let listaFiltrada = this.filtrarLista(data);
			this.dataSource = new MatTableDataSource(listaFiltrada);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		 });
	}

	pesquisar(){
		this.load(); //consultar backend
	}

	filtrarLista(data: any[]){
		return data.filter(x => (x.canalIdOrigem == this.filtro.canalIdOrigem || !this.filtro.canalIdOrigem) && (x.canalIdDestino == this.filtro.canalIdDestino || !this.filtro.canalIdDestino)  )
	}

	novo() {
		var regraEstoque = new RegraEstoque();
		const dialogRef = this.dialog.open(RegraEstoqueEditComponent, { data: { model: regraEstoque } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: RegraEstoque) {
		const dialogRef = this.dialog.open(RegraEstoqueEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: RegraEstoque) {
		this._alertService.confirmationMessage("",`Deseja realmente deletar a regra "${model.id}"?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this._service.delete(model.id).subscribe(r=> {
					this._alertService.show("Sucesso","Regra deletada com sucesso.",'success');
					this.load();
				});
			}
		});
	}

	applyFilter(filtro: RegraEstoqueFiltro) {

	}
}

