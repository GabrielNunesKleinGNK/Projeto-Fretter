import {
	Component,
  	OnInit,
  	Inject,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy
} from '@angular/core';

import 
{ 
  MatPaginator, 
  MatSort, 
  MatSnackBar, 
  MatDialog,
  MatTableDataSource ,
  MatDialogRef, 
  MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup,FormControl, Validators,ReactiveFormsModule  } from '@angular/forms';

import { UsuarioTipo } from "../../../../../core/models/usuarioTipo.model";
import { UsuarioTipoService } from "../../../../../core/services/usuarioTipo.service";
import { AlertService } from '../../../../../core/services/alert.service';

@Component({
  	selector: 'm-cliente-edit',
	templateUrl: './usuarioTipo.edit.component.html'
})
export class UsuarioTipoEditComponent implements OnInit {
  	model: UsuarioTipo;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
  	loadingAfterSubmit: boolean = false;
  constructor(public dialogRef: MatDialogRef<UsuarioTipoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: UsuarioTipoService,
		private _alertService: AlertService) { }

  ngOnInit() {
	this.model = this.data.model;
    this.createForm();
    this.viewLoading = true;
	this.bindList();
	setTimeout(() => {this.viewLoading = false;}, 500);
  }

  bindList(){
  }

  createForm() {
		this.modelForm = new FormGroup({
			'descricao' : new FormControl(this.model.descricao),
		});

  }

  	get descricao() { return this.modelForm.get('descricao'); }

	getTitle(): string {
		return this.model.id > 0? `Edição usuariotipo: ${this.model.id}` : 'Novo usuariotipo';
	}


	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}
	toModel(): UsuarioTipo {
		const controls = this.modelForm.controls;
		const _model = new UsuarioTipo();
		_model.id = this.model.id;	

		Object.keys(controls).forEach(controlName =>{
				_model[controlName] = controls[controlName].value;
			}
		);

		return _model;
	}

	onSubmit() {
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;
		const controls = this.modelForm.controls;

		if (this.modelForm.invalid) {
			Object.keys(controls).forEach(controlName => {
					controls[controlName].markAsTouched();
					if(!controls[controlName].valid)
						console.log(controlName);
				}
			);
			this.hasFormErrors = true;
			return;
		}

		const model = this.toModel();
		this.save(model);
	}

	save(_model: UsuarioTipo) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		this._service.save(_model).subscribe(res => {
			this._alertService.show("Sucesso.","UsuarioTipo cadastrado / Alterado com sucesso.",'success');
			this.viewLoading = false;
			this.dialogRef.close({_model,isEdit: true});
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

