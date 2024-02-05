import {
	Component,
	OnInit,
	Inject,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy
} from '@angular/core';

import {
	MatPaginator,
	MatSort,
	MatSnackBar,
	MatDialog,
	MatTableDataSource,
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

import { Usuario } from "../../../../../core/models/usuario";
import { UsuarioService } from "../../../../../core/services/usuario.service";
import { AlertService } from '../../../../../core/services/alert.service';
import { UsuarioTipoService } from '../../../../../core/services/usuarioTipo.service';
import { UsuarioMenu } from '../../../../../core/models/usuarioMenu';

@Component({
	selector: 'm-usuario-edit',
	templateUrl: './usuario.edit.component.html',
	styleUrls: ['./usuario.edit.component.scss']
})
export class UsuarioEditComponent implements OnInit {
	model: Usuario;
	disabled: boolean = false;
	tipos: any[];
	usuarios: any[];
	regionais: any[];
	grupos: any[];
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	permissoes = Array<UsuarioMenu>();


	constructor(public dialogRef: MatDialogRef<UsuarioEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: UsuarioService,
		private _alertService: AlertService,
		private tipoService: UsuarioTipoService) {


	}

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;
		setTimeout(() => { this.viewLoading = false; }, 500);

		this.tipoService.get().subscribe(data => {
			this.tipos = data;
		});

		this._service.get().subscribe(data => {
			this.usuarios = data.filter((item) => {
				return item.usuarioTipoId == 1 || item.usuarioTipoId == 2 || item.usuarioTipoId == 5;
			});
		});

		this._service.getMenus(this.model.id).subscribe(data => {
			this.permissoes = data;
		});
	}

	createForm() {
		this.modelForm = new FormGroup({
			'nome': new FormControl(this.model.nome, [Validators.required]),
			'login': new FormControl(this.model.login, [Validators.required]),
			'email': new FormControl(this.model.email, [Validators.required]),
			'senha': new FormControl(this.model.senha),
			'alterarSenha': new FormControl(this.model.alterarSenha),
			'administrador': new FormControl(this.model.administrador),
			'avatar': new FormControl(this.model.avatar),
			'usuarioTipoId': new FormControl(this.model.usuarioTipoId),
			'grupoPosVendaId': new FormControl(this.model.grupoPosVendaId),
			'bloqueado': new FormControl(this.model.bloqueado),
			'telefone': new FormControl(this.model.telefone),
			'superior': new FormControl(this.model.superior),
			'regional': new FormControl(this.model.regionalId),
		});

	}
	get nome() { return this.modelForm.get('nome'); }
	get login() { return this.modelForm.get('login'); }
	get email() { return this.modelForm.get('email'); }
	get senha() { return this.modelForm.get('senha'); }
	get avatar() { return this.modelForm.get('avatar'); }
	get alterarSenha() { return this.modelForm.get('alterarSenha'); }
	get bloqueado() { return this.modelForm.get('bloqueado'); }
	get telefone() { return this.modelForm.get('telefone'); }
	get superior() { return this.modelForm.get('superior'); }
	get regional() { return this.modelForm.get('regional'); }
	get usuarioTipoId() { return this.modelForm.get('usuarioTipoId'); }
	get grupoPosVendaId() { return this.modelForm.get('grupoPosVendaId'); }


	getTitle(): string {
		return this.model.id > 0 ? `Edição usuario: ${this.model.nome}` : 'Novo Usuario';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}
	toModel(): Usuario {
		const controls = this.modelForm.controls;
		const _model = new Usuario();
		_model.id = this.model.id;
		_model.nome = controls['nome'].value;
		_model.login = controls['login'].value;
		_model.email = controls['email'].value;
		_model.senha = controls['senha'].value;
		_model.administrador = controls['administrador'].value;
		_model.avatar = this.model.avatar;
		_model.senhaAlterada = controls['alterarSenha'].value;
		_model.bloqueado = controls['bloqueado'].value;
		_model.telefone = controls['telefone'].value;
		_model.superior = controls['superior'].value;
		_model.usuarioTipoId = controls['usuarioTipoId'].value;
		_model.grupoPosVendaId = controls['grupoPosVendaId'].value;


		if (!this.model.avatar)
			_model.avatar = "./../../../../../../assets/app/media/img/users/user.png";

		_model.alterarSenha = controls['alterarSenha'].value;
		return _model;
	}
	onSubmit() {
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;
		const controls = this.modelForm.controls;

		if (this.modelForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		}

		const model = this.toModel();
		this.save(model);
	}

	save(_model: Usuario) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;

		this._service.save(_model).subscribe(res => {

			if (_model.id > 0) {
				const menus = this.getMenusSelecionados();
				this._service.savePermissoes(_model.id, menus).subscribe(res => {

				});
			}

			this._alertService.show("Sucesso.", "Usuario cadastrado / Alterado com sucesso.", 'success');
			this.viewLoading = false;
			this.dialogRef.close({ _model, isEdit: true });
		}, error => {
			this.viewLoading = false;
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	processFile(imageInput: any) {
		const file: File = imageInput.files[0];
		const reader = new FileReader();

		reader.addEventListener('load', (event: any) => {
			this.model.avatar = new ImageSnippet(event.target.result, file).src;
		});

		reader.readAsDataURL(file);
	}

	public getMenusSelecionados() {
		var menus = [];

		this.permissoes.forEach(p => {

			if (p.hasSubMenu) {
				p.subMenus.forEach(s => {
					if (s.hasPermission) {
						menus.push(s.id);
					}
				});
				if (p.hasPermission) {
					menus.push(p.id);
				}
			} else {
				if (p.hasPermission) {
					menus.push(p.id);
				}
			}
		});

		return menus;
	}

	public selecionarTodos() {
		this.permissoes.forEach(p => {
			if (p.hasSubMenu) {
				p.subMenus.forEach(s => {
					s.hasPermission = true;
				});

				p.hasPermission = true;

			} else {
				p.hasPermission = true;
			}
		});
	}

	public removerTodos() {
		this.permissoes.forEach(p => {
			if (p.hasSubMenu) {
				p.subMenus.forEach(s => {
					s.hasPermission = false;
				});

				p.hasPermission = false;

			} else {
				p.hasPermission = false;
			}
		});
	}


}
class ImageSnippet {
	constructor(public src: string, public file: File) { }
}