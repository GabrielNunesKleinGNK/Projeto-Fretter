import {
	Component,
	OnInit,
	Inject,
	AfterContentInit
} from '@angular/core';

import {
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

import { RegraEstoque } from '../../../../../../core/models/Fusion/regraEstoque';
import { AlertService } from '../../../../../../core/services/alert.service';
import { RegraEstoqueService } from '../../../../../../core/services/regraEstoque.service';
import { CanalService } from '../../../../../../core/services/canal.service';
import moment from 'moment';
import { DatePipe } from '@angular/common';
import { combineLatest } from 'rxjs';
import { string } from '@amcharts/amcharts4/core';
import { GrupoService } from '../../../../../../core/services/grupo.service';

@Component({
	selector: 'm-regraEstoque-edit',
	templateUrl: './regraEstoque.edit.component.html',
	styleUrls: ['./regraEstoque.edit.component.scss']
})
export class RegraEstoqueEditComponent implements OnInit, AfterContentInit  {

	model: RegraEstoque;
	disabled: boolean = false;
	listCanais: any[];
	listGrupos: any[];

	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	loadingAfterSubmit: boolean = false;
	viewLoading: boolean = false;

	constructor(public dialogRef: MatDialogRef<RegraEstoqueEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: RegraEstoqueService,
		private _canalService: CanalService,
		private _grupoService: GrupoService,
		private _alertService: AlertService) {
	}

	ngOnInit() {
		this.viewLoading = true;
		//const transportadoresCnpj$ = this._transportadorService.getTransportadoresCnpj();
		const canais$ = this._canalService.getCanaisPorEmpresa();
		const grupos$ = this._grupoService.getGruposPorEmpresa();

		combineLatest([canais$, grupos$]).subscribe(([canais, grupos]) => {
			this.listCanais = canais;
			this.listGrupos = grupos;
		// 	this.transportadoresCnpj = transportadoresCnpj;
		// 	this.ciclos = ciclos;
		 	this.viewLoading = false;
		});

		this.model = this.data.model;
		this.createForm();
	}

	ngAfterContentInit(){
	}

	createForm() {
		this.modelForm = new FormGroup({
			'codigo': new FormControl(this.model.id, []),
			'canalOrigemId': new FormControl({value: this.model.canalIdOrigem, disabled: this.model.id > 0 }, [Validators.required]),
			'canalDestinoId': new FormControl({value: this.model.canalIdDestino, disabled: this.model.id > 0}, [Validators.required]),
			'grupoId': new FormControl( this.model.grupoId, []),
			'skus': new FormControl( this.model.skus, []),
		}, [ this.canaisDevemSerDiferentes, this.grupoOuSkuObrigatorio ]);

	}

	private canaisDevemSerDiferentes(fGroup: FormGroup) {
		var origem = fGroup.get('canalOrigemId').value;
		var destino = fGroup.get('canalDestinoId').value;
		return (origem !== destino) ? null : {'mismatch': true};
	}

	private grupoOuSkuObrigatorio(fGroup: FormGroup) {
		var grupoId = fGroup.get('grupoId').value;
		var sku = fGroup.get('skus').value;
		return (sku || grupoId) ? null : {'grupoOuSku': true};
	}

	get canalOrigemId() { return this.modelForm.get('canalOrigemId'); }
	get canalDestinoId() { return this.modelForm.get('canalDestinoId'); }
	get grupoId() { return this.modelForm.get('grupoId'); }
	get skus() { return this.modelForm.get('skus');}

	getTitle(): string {
		return this.model.id > 0 ? `Editar Regra Estoque` : 'Cadastrar Regra Estoque';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}
	toModel(): RegraEstoque {
		const controls = this.modelForm.controls;
		const _model = new RegraEstoque();

		_model.id = this.model.id;
		_model.empresaId = this.model.empresaId;
		_model.canalIdOrigem = controls['canalOrigemId'].value;
		_model.canalIdDestino = controls['canalDestinoId'].value;
		_model.grupoId = controls['grupoId'].value;
		_model.skus = controls['skus'].value;

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

	save(_model: RegraEstoque) {
		this.loadingAfterSubmit = true;

		this._service.save(_model).subscribe(res => {
			console.log('sucesso', res);
			this._alertService.show("Sucesso.", "Regra cadastrada / alterada com sucesso.", 'success');
			this.dialogRef.close({ _model, isEdit: true });
		}, respError => {
			this._alertService.show("Erro.", respError.error.Message, 'error');
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	trocarCanais(){
		var origem = this.model.canalIdOrigem;
		var destino = this.model.canalIdDestino;

		this.model.canalIdDestino = origem;
		this.model.canalIdOrigem = destino;
	}
}