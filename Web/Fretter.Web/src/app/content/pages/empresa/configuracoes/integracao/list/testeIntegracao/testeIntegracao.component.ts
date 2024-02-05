import {
	Component,
	OnInit,
	Inject,
} from '@angular/core';

import {
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormGroup, FormControl } from '@angular/forms';
import { IntegracaoService } from '../../../../../../../core/services/integracao.service';
import { EmpresaIntegracao } from '../../../../../../../core/models/Fusion/empresaIntegracao';

@Component({
	selector: 'm-testeIntegracao',
	templateUrl: './testeIntegracao.component.html',
	styleUrls: ['./testeIntegracao.component.scss']
})
export class TesteIntegracaoComponent implements OnInit {
	model: any;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	body: string;
	statusCode: number = 0;
	tempo: number;

	constructor(public dialogRef: MatDialogRef<TesteIntegracaoComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: IntegracaoService
	) { }

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;
		setTimeout(() => { this.viewLoading = false; }, 500);
	}

	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'canalVendaId': new FormControl(this.model.canalVendaId),
			'urlBase': new FormControl(this.model.urlBase),
			'urlToken': new FormControl(this.model.urlToken),
			'apiKey': new FormControl(this.model.apiKey),
			'usuario': new FormControl(this.model.usuario),
			'senha': new FormControl(this.model.senha),
			'clientId': new FormControl(this.model.clientId),
			'clientSecret': new FormControl(this.model.clientSecret),
			'clientScope': new FormControl(this.model.clientScope),
			'entregaOrigemImportacaoId': new FormControl(this.model.entregaOrigemImportacaoId),			

			'verbo': new FormControl(this.model.listaIntegracoes.verbo),
			'url': new FormControl(this.model.listaIntegracoes.url),
			'layoutHeader': new FormControl(this.model.listaIntegracoes.layoutHeader),
			'layout': new FormControl(this.model.listaIntegracoes.layout),

			'body': new FormControl(this.body),
			'statusCode': new FormControl(this.statusCode),
			'tempo': new FormControl(this.tempo)
		});
	}
	get id() { return this.modelForm.get('id'); }
	get canalVendaId() { return this.modelForm.get('canalVendaId'); }
	get urlBase() { return this.modelForm.get('urlBase'); }
	get urlToken() { return this.modelForm.get('urlToken'); }
	get apiKey() { return this.modelForm.get('apiKey'); }
	get usuario() { return this.modelForm.get('usuario'); }
	get senha() { return this.modelForm.get('senha'); }
	get clientId() { return this.modelForm.get('clientId'); }
	get clientSecret() { return this.modelForm.get('clientSecret'); }
	get clientScope() { return this.modelForm.get('clientScope'); }
	get entregaOrigemImportacaoId() { return this.modelForm.get('entregaOrigemImportacaoId'); }	
	get verbo() { return this.modelForm.get('verbo'); }

	getTitle(): string {
		return 'Teste da Integração';
	}

	toModel(): EmpresaIntegracao {
		const controls = this.modelForm.controls;
		const _model = new EmpresaIntegracao();
		_model.id = controls['id'].value;
		_model.canalVendaId = controls['canalVendaId'].value;
		_model.urlBase = controls['urlBase'].value;
		_model.urlToken = controls['urlToken'].value;
		_model.apiKey = controls['apiKey'].value;
		_model.usuario = controls['usuario'].value;
		_model.senha = controls['senha'].value;
		_model.clientId = controls['clientId'].value;
		_model.clientSecret = controls['clientSecret'].value;
		_model.clientScope = controls['clientScope'].value;
		_model.entregaOrigemImportacaoId = controls['entregaOrigemImportacaoId'].value;
		_model.listaIntegracoes.push(controls['listaIntegracoes'].value);

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
		this.testeIntegracao(model);
	}

	testeIntegracao(_model: EmpresaIntegracao) {
		this.viewLoading = true;

		this._service.testeIntegracao(_model).subscribe(res => {
			this.body = res.body;
			this.statusCode = res.statusCode;
			this.tempo = res.tempo;

			this.viewLoading = false;

		}, error => {
			this.viewLoading = false;
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}