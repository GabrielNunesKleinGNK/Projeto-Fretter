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

import { EmpresaIntegracao } from '../../../../../../core/models/Fusion/empresaIntegracao';
import { EmpresaIntegracaoService } from '../../../../../../core/services/empresaIntegracao.service';
import { CanalVendaService } from '../../../../../../core/services/canalVenda.service';
import moment from 'moment';
import { DatePipe } from '@angular/common';
import { combineLatest } from 'rxjs';
import { string } from '@amcharts/amcharts4/core';
import { AlertService } from '../../../../../../core/services/alert.service';
import { IntegracaoEditComponent } from '../../integracao/edit/integracao.edit.component';
import { Integracao } from '../../../../../../core/models/Fusion/integracao';
import { EmpresaIntegracaoFiltro } from '../../../../../../core/models/Fusion/empresaIntegracaoFiltro.model';
import { CanalVenda } from '../../../../../../core/models/Fusion/canalVenda';
import { EntregaOrigemImportacao } from '../../../../../../core/models/Fusion/entregaOrigemImportacao.model';
import { EntregaOrigemImportacaoService } from '../../../../../../core/services/entregaOrigemImportacao.service';

@Component({
	selector: 'm-empresaintegracao-edit',
	templateUrl: './empresaIntegracao.edit.component.html',
	styleUrls: ['./empresaIntegracao.edit.component.scss']
})
export class EmpresaIntegracaoEditComponent implements OnInit {
	model: EmpresaIntegracao;
	disabled: boolean = false;
	arquivos: any[];
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	lstCanaisVenda : Array<CanalVenda>;
	lstEntregaOrigemImportacao : Array<EntregaOrigemImportacao>;

	constructor(public dialogRef: MatDialogRef<EmpresaIntegracaoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: EmpresaIntegracaoService,
		private _serviceCanalVenda : CanalVendaService,
		private _serviceEntregaOrigemImportacao : EntregaOrigemImportacaoService,
		private _alertService: AlertService,
		public dialog: MatDialog
	) {}

	ngOnInit() {
		var canal = new CanalVenda();
		var origem = new EntregaOrigemImportacao();
		this._serviceCanalVenda.getCanalVendaPorEmpresa(canal).subscribe(result => {
			this.lstCanaisVenda = result.data;
		});

		this._serviceEntregaOrigemImportacao.getAll(origem).subscribe(result=>{
			this.lstEntregaOrigemImportacao = result.data;
		});

		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;
		this.viewLoading = false;
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
			'ListaIntegracoes': new FormControl(this.model.listaIntegracoes),
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
	get ListaIntegracoes() { return this.modelForm.get('ListaIntegracoes'); }

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

	toModel(): EmpresaIntegracao {
		const controls = this.modelForm.controls;
		const _model = new EmpresaIntegracao();
		_model.id =  controls['id'].value;
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

		controls['ListaIntegracoes'].value.forEach(element => {
			_model.listaIntegracoes.push(element);	
		});
		
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

	save(_model: EmpresaIntegracao) {
		this.viewLoading = true;

		if(_model.listaIntegracoes.length > 0){
			this._service.save(_model).subscribe(res => {
				this._alertService.show("Sucesso.", "Integração salva com sucesso.", 'success');
				this.viewLoading = false;
				this.dialogRef.close({ _model, isEdit: true });
			}, error => {
				this.viewLoading = false;
			});
		}
		else{
			this._alertService.show("Atenção.", "É necessário preencher os dados da próxima aba.", 'warning');
			this.viewLoading = false;
		}
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}