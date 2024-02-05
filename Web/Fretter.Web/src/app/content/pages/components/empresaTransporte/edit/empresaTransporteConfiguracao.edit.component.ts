import {
	Component,
	OnInit,
	Inject,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	Input
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

import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

import { AlertService } from '../../../../../core/services/alert.service';
import { EmpresaTransporteConfiguracao } from '../../../../../core/models/Fusion/empresaTransporteConfiguracao.model';
import { EmpresaTransporteConfiguracaoService } from '../../../../../core/services/empresaTransporteConfiguracao.service';
import { EmpresaTransporteTipo } from '../../../../../core/models/Fusion/empresaTransporteTipo.model';
import { EmpresaTransporteTipoItem } from '../../../../../core/models/Fusion/empresaTransporteTipoItem.model';
import { EmpresaTransporteTipoService } from '../../../../../core/services/empresaTransporteTipo.service';
import { EmpresaTransporteTipoItemService } from '../../../../../core/services/empresaTransporteTipoItem.service';
import { combineLatest } from 'rxjs';

@Component({
	selector: 'm-empresatransporteconfiguracao-edit',
	templateUrl: './empresaTransporteConfiguracao.edit.component.html',
	styleUrls: ['./empresaTransporteConfiguracao.edit.component.scss']
})
export class EmpresaTransporteConfiguracaoEditComponent implements OnInit {
	model: EmpresaTransporteConfiguracao;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	lstEmpresaTransporteTipo: Array<EmpresaTransporteTipo> = new Array<EmpresaTransporteTipo>();
	lstEmpresaTransporteItem: Array<EmpresaTransporteTipoItem> = new Array<EmpresaTransporteTipoItem>();

	constructor(public dialogRef: MatDialogRef<EmpresaTransporteConfiguracaoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: EmpresaTransporteConfiguracaoService,
		private _serviceEmpresaTransporteTipo: EmpresaTransporteTipoService,
		private _serviceEmpresaTransporteTipoItem: EmpresaTransporteTipoItemService,
		private _alertService: AlertService,
	) { }

	ngOnInit() {
		this.model = this.data.model;		
		this.createForm();				
		const tipos$ = this._serviceEmpresaTransporteTipo.get();
		const tipoItems$ = this._serviceEmpresaTransporteTipoItem.get();

		combineLatest([tipos$, tipoItems$]).subscribe(([tipos, tipoItems]) => {
			this.lstEmpresaTransporteTipo = tipos;
			this.lstEmpresaTransporteItem = tipoItems;
		});
	}

	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'empresaTransporteTipoId': new FormControl(this.model.empresaTransporteTipoItem.empresaTransporteTipoId, [Validators.required]),
			'empresaTransporteTipoItemId': new FormControl(this.model.empresaTransporteTipoItemId, [Validators.required]),
			'codigoContrato': new FormControl(this.model.codigoContrato, [Validators.required]),
			'codigoIntegracao': new FormControl(this.model.codigoIntegracao, [Validators.required]),
			'codigoCartao': new FormControl(this.model.codigoCartao, [Validators.required]),
			'usuario': new FormControl(this.model.usuario),
			'senha': new FormControl(this.model.senha),
			'vigenciaInicial': new FormControl(this.model.vigenciaInicial),
			'vigenciaFinal': new FormControl(this.model.vigenciaFinal),
			'diasValidade': new FormControl(this.model.diasValidade),
			'retornoValidacao': new FormControl(this.model.retornoValidacao),
			'valido': new FormControl(this.model.valido),
			'producao': new FormControl(this.model.producao),
		});
	}
	get id() { return this.modelForm.get('id'); }
	get empresaTransporteTipoId() { return this.modelForm.get('empresaTransporteTipoId'); }
	get empresaTransporteTipoItemId() { return this.modelForm.get('empresaTransporteTipoItemId'); }
	get codigoContrato() { return this.modelForm.get('codigoContrato'); }
	get codigoIntegracao() { return this.modelForm.get('codigoIntegracao'); }
	get codigoCartao() { return this.modelForm.get('codigoCartao'); }
	get usuario() { return this.modelForm.get('usuario'); }
	get senha() { return this.modelForm.get('senha'); }
	get vigenciaInicial() { return this.modelForm.get('vigenciaInicial'); }
	get vigenciaFinal() { return this.modelForm.get('vigenciaFinal'); }
	get diasValidade() { return this.modelForm.get('diasValidade'); }
	get retornoValidacao() { return this.modelForm.get('retornoValidacao'); }
	get dataValidacao() { return this.modelForm.get('dataValidacao'); }
	get valido() { return this.modelForm.get('valido'); }
	get producao() { return this.modelForm.get('producao'); }

	getTitle(): string {
		return this.model.id > 0 ? `Edição de Configuração: ${this.model.id}` : 'Nova Configuração';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	toModel(): EmpresaTransporteConfiguracao {
		const controls = this.modelForm.controls;
		const _model = new EmpresaTransporteConfiguracao();
		_model.id = controls['id'].value;
		_model.empresaTransporteTipoItemId = controls['empresaTransporteTipoItemId'].value;
		_model.codigoContrato = controls['codigoContrato'].value;
		_model.codigoIntegracao = controls['codigoIntegracao'].value;
		_model.codigoCartao = controls['codigoCartao'].value;
		_model.usuario = controls['usuario'].value;
		_model.senha = controls['senha'].value;
		_model.vigenciaInicial = controls['vigenciaInicial'].value;
		_model.vigenciaFinal = controls['vigenciaFinal'].value;
		_model.diasValidade = controls['diasValidade'].value;
		_model.retornoValidacao = controls['retornoValidacao'].value;
		_model.valido = controls['valido'].value;
		_model.producao = controls['producao'].value;

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

	filtroTransporteTipoChange(value) {
		this.model.empresaTransporteTipoItemId = 0;
		if (!value)
			this._serviceEmpresaTransporteTipoItem.getTransporteTipoItemPorTipo(value.id).subscribe(ret => {
				this.lstEmpresaTransporteItem = ret;
			});
	}

	save(_model: EmpresaTransporteConfiguracao) {
		this._service.save(_model).subscribe(res => {
			this._alertService.show("Sucesso.", "Configuração cadastrada / alterada com sucesso.", 'success');
			this.dialogRef.close({ _model, isEdit: true });
		}, error => {
			let errorMessage = error.errors[0].message || "Erro ao cadastrar / alterar Configuração";
			this._alertService.show("Erro.", errorMessage, 'error');
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}