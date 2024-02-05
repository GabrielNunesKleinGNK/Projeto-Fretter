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
import { Usuario } from "../../../../../core/models/usuario";
import { UsuarioService } from "../../../../../core/services/usuario.service";
import { UsuarioEditComponent } from '../edit/usuario.edit.component';
import { AlertService } from '../../../../../core/services/alert.service';

@Component({
  selector: 'm-usuario',
  templateUrl: './usuario.list.component.html'
})
export class UsuarioListComponent implements OnInit {
	dataSource: MatTableDataSource<Usuario>;
	displayedColumns: string[] = ['id',  'avatar','nome', 'login', 'tipo', 'dataCadastro','actions'];
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: UsuarioService, public dialog: MatDialog, private cdr: ChangeDetectorRef, private _alertService : AlertService){
		
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
		var usuario = new Usuario();
		const dialogRef = this.dialog.open(UsuarioEditComponent, { data: { model:usuario } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: Usuario) {
		const dialogRef = this.dialog.open(UsuarioEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: Usuario) {
		this._alertService.confirmationMessage("",`Deseja realmente inativar o Usuario "${model.id}"?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this._service.delete(model.id).subscribe(r=> {
					this._alertService.show("Sucesso","Usuario inativado com sucesso.",'success');
					this.load();
				});
			}
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}
}

