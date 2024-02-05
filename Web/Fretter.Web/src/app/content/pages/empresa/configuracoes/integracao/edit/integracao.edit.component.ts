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

import { FormBuilder, FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

import { AlertService } from '../../../../../../core/services/alert.service';
import { Integracao } from '../../../../../../core/models/Fusion/integracao';
import { IntegracaoService } from '../../../../../../core/services/integracao.service';
import { IntegracaoTipoService } from '../../../../../../core/services/integracaoTipo.service';
import { inject } from '@angular/core/testing';
import { arrayify } from 'tslint/lib/utils';
import { IntegracaoTipo } from '../../../../../../core/models/Fusion/integracaoTipo.model';

@Component({
	selector: 'm-integracao-edit',
	templateUrl: './integracao.edit.component.html',
	styleUrls: ['./integracao.edit.component.scss']
})
export class IntegracaoEditComponent implements OnInit {
	model: Integracao;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	lstVerbos : any[]
	lstCamposDePara : any[];
	lstIntegracaoTipo: Array<IntegracaoTipo>;

	constructor(public dialogRef: MatDialogRef<IntegracaoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: IntegracaoService,
		private _serviceIntegracaoTIpo: IntegracaoTipoService,
		private _alertService: AlertService,
	) {	}

	ngOnInit() {
		this.model = this.data.model;
		this.lstVerbos = [{verbo : "POST"}, {verbo : "PUT"}, {verbo: "GET"}]
		this.createForm();
		this.viewLoading = true;
		setTimeout(() => {this.viewLoading = false;}, 500);

		this._service.buscaCamposDePara().subscribe(ret =>{
			this.lstCamposDePara = ret;
		})

		this._serviceIntegracaoTIpo.get().subscribe(ret => {
			this.lstIntegracaoTipo = ret;
		})
	}
	
	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'integracaoTipoId': new FormControl(this.model.integracaoTipoId, [Validators.required]),
			'empresaeIntegracaoId': new FormControl(this.model.empresaeIntegracaoId),
			'url': new FormControl(this.model.url, [Validators.required]),
			'verbo': new FormControl(this.model.verbo, [Validators.required]),
			'lote': new FormControl(this.model.lote),
			'layoutHeader': new FormControl(this.model.layoutHeader),
			'layout': new FormControl(this.model.layout),
			'registros': new FormControl(this.model.registros),
			'paralelo': new FormControl(this.model.paralelo),
			'producao': new FormControl(this.model.producao),
			'envioBody': new FormControl(this.model.envioBody),
			'enviaOcorrenciaHistorico': new FormControl(this.model.enviaOcorrenciaHistorico),
			'envioConfigId': new FormControl(this.model.envioConfigId, [Validators.required]),
			'integracaoGatilho': new FormControl(this.model.integracaoGatilho),
		});
	 }
	get id() { return this.modelForm.get('id'); }
	get integracaoTipoId() { return this.modelForm.get('integracaoTipoId'); }
	get empresaeIntegracaoId() { return this.modelForm.get('empresaeIntegracaoId'); }
	get url() { return this.modelForm.get('url'); }
	get verbo() { return this.modelForm.get('verbo'); }
	get lote() { return this.modelForm.get('lote'); }
	get layoutHeader() { return this.modelForm.get('layoutHeader'); }
	get layout() { return this.modelForm.get('layout'); }
	get registros() { return this.modelForm.get('registros'); }
	get paralelo() { return this.modelForm.get('paralelo'); }
	get producao() { return this.modelForm.get('producao'); }
	get envioBody() { return this.modelForm.get('envioBody'); }
	get enviaOcorrenciaHistorico() { return this.modelForm.get('enviaOcorrenciaHistorico'); }
	get envioConfigId() { return this.modelForm.get('envioConfigId'); }
	get integracaoGatilho() { return this.modelForm.get('integracaoGatilho'); }

	getTitle(): string {
		if(this.model.id == 0)
    		return 'Nova Integração';
		else
			return 'Editar Integração';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	toModel(): Integracao {
		const controls = this.modelForm.controls;
		const _model = new Integracao();
		_model.id = controls['id'].value;
		_model.integracaoTipoId = controls['integracaoTipoId'].value;
		_model.empresaeIntegracaoId = controls['empresaeIntegracaoId'].value;
		_model.url = controls['url'].value;
		_model.verbo = controls['verbo'].value;
		_model.lote = controls['lote'].value;
		_model.layoutHeader = controls['layoutHeader'].value;
		_model.layout = controls['layout'].value;
		_model.registros = controls['registros'].value;
		_model.paralelo = controls['paralelo'].value;
		_model.producao = controls['producao'].value;
		_model.envioBody = controls['envioBody'].value;
		_model.enviaOcorrenciaHistorico = controls['enviaOcorrenciaHistorico'].value;
		_model.envioConfigId = controls['envioConfigId'].value;
		_model.integracaoGatilho = controls['integracaoGatilho'].value;

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

	save(_model: Integracao) {
		this.viewLoading = true;
		this.dialogRef.close(_model);
		// this._service.save(_model).subscribe(res => {
		// 	this._alertService.show("Sucesso.", "Integração salva com sucesso.", 'success');
		// 	this.viewLoading = false;
		// 	this.dialogRef.close({ _model, isEdit: true });
		// }, error => {
		// 	this.viewLoading = false;
		// });
	}

	validateLoteClick() {
		this.model.lote = !((this.model.lote || this.model.lote == undefined) ? false : true);
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	buscaCamposDeParaHeader(campo){
		this.model.layoutHeader += '$' + campo + '$';
	}

	buscaCamposDePara(campo : string){
		this.model.layout += '$' + campo + '$';
	}
}