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
import moment from 'moment';
import { DatePipe } from '@angular/common';
import { combineLatest } from 'rxjs';
import { string } from '@amcharts/amcharts4/core';
import { AlertService } from '../../../../../../../core/services/alert.service';
import { EmpresaIntegracaoItemDetalhe } from '../../../../../../../core/models/Fusion/empresaIntegracaoItemDetalhe.model';
import { EmpresaIntegracaoItemDetalheService } from '../../../../../../../core/services/empresaIntegracaoItemDetalhe.service';

@Component({
	selector: 'm-empresaintegracaoitemdetalhe-edit',
	templateUrl: './itemDetalhe.edit.component.html',
	styleUrls: ['./itemDetalhe.edit.component.scss']
})
export class EmpresaIntegracaoItemDetalheEditComponent implements OnInit {
	model: EmpresaIntegracaoItemDetalhe;
	disabled: boolean = false;
	arquivos: any[];
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;

	constructor(public dialogRef: MatDialogRef<EmpresaIntegracaoItemDetalheEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: EmpresaIntegracaoItemDetalheService,
		private _alertService: AlertService,
		public dialog: MatDialog
	) {}

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;
		this.viewLoading = false;
	}

	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'empresaIntegracaoItemId': new FormControl(this.model.empresaIntegracaoItemId),
			'codigoIntegracao': new FormControl(this.model.codigoIntegracao),
			'chave': new FormControl(this.model.chave),
			'requestURL': new FormControl(this.model.requestURL),
			'jsonEnvio': new FormControl(this.model.jsonEnvio),
			'jsonBody': new FormControl(this.model.jsonBody),
			'jsonRetorno': new FormControl(this.model.jsonRetorno),
			'httpTempo': new FormControl(this.model.httpTempo),
			'httpStatus': new FormControl(this.model.httpStatus),
			'httpResponse': new FormControl(this.model.httpResponse),
			'sucesso': new FormControl(this.model.sucesso),
			'pendenteProcessamentoRetorno': new FormControl(this.model.pendenteProcessamentoRetorno),
			'processadoRetornoSucesso': new FormControl(this.model.processadoRetornoSucesso),
			'mensagemRetorno': new FormControl(this.model.mensagemRetorno),
			'httpStatusCode': new FormControl(this.model.httpStatusCode),
		});
	 }
	get id() { return this.modelForm.get('id'); }
	get empresaIntegracaoItemId() { return this.modelForm.get('empresaIntegracaoItemId'); }
	get codigoIntegracao() { return this.modelForm.get('codigoIntegracao'); }
	get chave() { return this.modelForm.get('chave'); }
	get requestURL() { return this.modelForm.get('requestURL'); }
	get jsonEnvio() { return this.modelForm.get('jsonEnvio'); }
	get jsonBody() { return this.modelForm.get('jsonBody'); }
	get jsonRetorno() { return this.modelForm.get('jsonRetorno'); }
	get httpTempo() { return this.modelForm.get('httpTempo'); }
	get httpStatus() { return this.modelForm.get('httpStatus'); }
	get httpResponse() { return this.modelForm.get('httpResponse'); }
	get sucesso() { return this.modelForm.get('sucesso'); }
	get pendenteProcessamentoRetorno() { return this.modelForm.get('pendenteProcessamentoRetorno'); }
	get processadoRetornoSucesso() { return this.modelForm.get('processadoRetornoSucesso'); }
	get mensagemRetorno() { return this.modelForm.get('mensagemRetorno'); }
	get httpStatusCode() { return this.modelForm.get('httpStatusCode'); }

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	toModel(): EmpresaIntegracaoItemDetalhe {
		const controls = this.modelForm.controls;
		const _model = new EmpresaIntegracaoItemDetalhe();
		_model.id =  controls['id'].value;
		_model.empresaIntegracaoItemId = controls['empresaIntegracaoItemId'].value;
		_model.codigoIntegracao = controls['codigoIntegracao'].value;
		_model.chave = controls['chave'].value;
		_model.requestURL = controls['requestURL'].value;
		_model.jsonEnvio = controls['jsonEnvio'].value;
		_model.jsonBody = controls['jsonBody'].value;
		_model.jsonRetorno = controls['jsonRetorno'].value;
		_model.httpTempo = controls['httpTempo'].value;
		_model.httpStatus = controls['httpStatus'].value;
		_model.httpResponse = controls['httpResponse'].value;
		_model.sucesso = controls['sucesso'].value;
		_model.pendenteProcessamentoRetorno = controls['pendenteProcessamentoRetorno'].value;
		_model.processadoRetornoSucesso = controls['processadoRetornoSucesso'].value;
		_model.mensagemRetorno = controls['mensagemRetorno'].value;
		_model.httpStatusCode = controls['httpStatusCode'].value;
		
		return _model;
	}

	onSubmit() {
		this.viewLoading = true;

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
		this.reprocessar(model);
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	reprocessar(model : EmpresaIntegracaoItemDetalhe) {
		var ids = new Array<number>();
		ids.push(model.codigoIntegracao);
		
		this._service.reprocessarLote(ids).subscribe(data => {
			if(data)
				this._alertService.show("Sucesso","Ocorrencia enviada para reprocessamento", "success")
			else
				this._alertService.show("Sucesso","Ocorrencia enviada para reprocessamento", "success")
			
			document.getElementById('closeButton').click();
			this.viewLoading = false;
		});
	}
}