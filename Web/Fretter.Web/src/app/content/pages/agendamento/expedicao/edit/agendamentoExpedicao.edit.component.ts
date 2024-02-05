import {
	Component,
	OnInit,
	Inject,	
} from '@angular/core';

import {	
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AgendamentoExpedicao } from '../../../../../core/models/agendamentoExpedicao.model';
import { AgendamentoExpedicaoService } from '../../../../../core/services/agendamentoExpedicao.service';
import { AlertService } from '../../../../../core/services/alert.service';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { combineLatest } from 'rxjs';
import { Canal } from '../../../../../core/models/Fusion/Canal';
import { CanalService } from '../../../../../core/services/canal.service';
import { Transportador } from '../../../../../core/models/transportador.model';

@Component({
	selector: 'm-agendamentoExpedicao-edit',
	templateUrl: './agendamentoExpedicao.edit.component.html',
})

export class AgendamentoExpedicaoEditComponent implements OnInit {
	model: AgendamentoExpedicao;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	lstCanais : Array<Canal>;
	transportadores: Array<Transportador>;
	transportadoresCnpj: any[];
	prazoMinimo = [1,2,3,4,5,6,7,8,9,10];

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: any,
		public dialogRef: MatDialogRef<AgendamentoExpedicaoEditComponent>,
		private fb: FormBuilder,
		private _service: AgendamentoExpedicaoService,
		private _transportadorService: TransportadorService,
		private _canalService: CanalService,
		private _alertService: AlertService,
	) {	}

	ngOnInit() {
		const transportadores$ = this._transportadorService.getTransportadoresPorEmpresa();
		const canais$ = this._canalService.getCanaisPorEmpresa();

		combineLatest([transportadores$, canais$])
			.subscribe(([transportadores, canais]) => {
				this.transportadores = transportadores;
				this.lstCanais = canais;

				if (this.model.id > 0) {
					this.getTransportadoresCnpj(this.model.transportadorId);
				}

				this.viewLoading = false;
			}
		);

		this.model = this.data.model;
		this.createForm();
	}

	getTransportadoresCnpj(transportadorId: number) {
		this._transportadorService.getTransportadoresCnpj(transportadorId).subscribe(transportadoresCnpj => {
			this.transportadoresCnpj = transportadoresCnpj;
		});
	}

	getTransportadoresCnpjChange(transportadorId: number) {
		this.getTransportadoresCnpj(transportadorId);
		this.modelForm.controls["transportadorCnpjId"].reset();
	}

	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'canalId': new FormControl(this.model.canalId, []),
			'transportadorId': new FormControl(this.model.transportadorId, [Validators.required]),
			'transportadorCnpjId': new FormControl(this.model.transportadorCnpjId, []),
			'expedicaoAutomatica': new FormControl(this.model.expedicaoAutomatica, []),
			'prazoComercial': new FormControl(this.model.prazoComercial, [Validators.required])
		});
	}

	get id() { return this.modelForm.get('id'); }
	get canalId() { return this.modelForm.get('canalId'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get transportadorCnpjId() { return this.modelForm.get('transportadorCnpjId'); }
	get expedicaoAutomatica() { return this.modelForm.get('expedicaoAutomatica'); }
	get prazoComercial() { return this.modelForm.get('prazoComercial'); }

	getTitle(): string {
		return this.model.id > 0 ? `Edição de Gerenciamento de Expedição` : 'Gerenciamento de Expedição';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	toModel(): AgendamentoExpedicao {
		const controls = this.modelForm.controls;
		const _model = new AgendamentoExpedicao();
		_model.id = controls['id'].value;
		_model.canalId = controls['canalId'].value;
		_model.transportadorId = controls['transportadorId'].value;
		_model.transportadorCnpjId = controls['transportadorCnpjId'].value;
		_model.expedicaoAutomatica = controls['expedicaoAutomatica'].value;
		_model.prazoComercial = controls['prazoComercial'].value;
		_model.dataCadastro = this.model.dataCadastro

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

	limparComboTransportadorFilial(){
		this.transportadoresCnpj = [];
		this.modelForm.controls["transportadorCnpjId"].reset();
	}

	save(_model: AgendamentoExpedicao) {
		this._service.save(_model).subscribe(res => {
			this._alertService.show("Sucesso.", "Configuração cadastrada / alterada com sucesso.", 'success');
			this.dialogRef.close({ _model, isEdit: true });
		}, error => {
			let errorMessage = error.errors[0].message || "Erro ao cadastrar / alterar Configuração";
			this._alertService.show("Erro.", errorMessage, 'error');
		});
	}
}
