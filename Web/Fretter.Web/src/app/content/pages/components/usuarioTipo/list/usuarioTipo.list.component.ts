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
import { UsuarioTipo } from "../../../../../core/models/usuarioTipo.model";
import { UsuarioTipoService } from "../../../../../core/services/usuarioTipo.service";
import { UsuarioTipoEditComponent } from '../edit/usuarioTipo.edit.component';
import { AlertService } from '../../../../../core/services/alert.service';

@Component({
  selector: 'm-usuariotipo',
  templateUrl: './usuarioTipo.list.component.html'
})
export class UsuarioTipoListComponent implements OnInit {
	dataSource: MatTableDataSource<UsuarioTipo>;
	displayedColumns: string[] = ['id', 'descricao', 'dataCadastro','actions'];
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: UsuarioTipoService, public dialog: MatDialog, private cdr: ChangeDetectorRef, private _alertService : AlertService){
		
	}
	ngOnInit() {
		this.load();
	}

	load() {
		this._service.get().subscribe(data => {
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		 });
	}


	novo() {
		var usuarioTipo = new UsuarioTipo();
		const dialogRef = this.dialog.open(UsuarioTipoEditComponent, { data: { model:usuarioTipo } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: UsuarioTipo) {
		const dialogRef = this.dialog.open(UsuarioTipoEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: UsuarioTipo) {
		this._alertService.confirmationMessage("",`Deseja realmente inativar o UsuarioTipo "${model.id}"?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this._service.delete(model.id).subscribe(r=> {
					this._alertService.show("Sucesso","UsuarioTipo inativado com sucesso.",'success');
					this.load();
				});
			}
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}
}
