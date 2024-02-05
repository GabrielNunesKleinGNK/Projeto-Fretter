import {
	Component,
	OnInit,
	Inject,
	AfterContentInit,
	Input,
	Output,
	EventEmitter
} from '@angular/core';

import {
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AlertService } from '../../../../../../../core/services/alert.service';
import { ContratoTransportadorRegra } from '../../../../../../../core/models/contratoTransportadorRegra';
import { ContratoTransportadorService } from '../../../../../../../core/services/contratoTransportador.service';
import { ConciliacaoTipoService } from '../../../../../../../core/services/conciliacaoTipo.service';
import { ConciliacaoTipo } from '../../../../../../../core/models/conciliacaoTipo';

@Component({
	selector: 'm-regra-edit',
	templateUrl: './regra.edit.component.html'	
})
export class ContratoTransportadorRegraEditComponent implements OnInit, AfterContentInit {
	model: ContratoTransportadorRegra;
	disabled: boolean = false;
	ocorrencias: any[];
	conciliacaoTipo : Array<ConciliacaoTipo>;	
	tipos = [{ id: 1, name: 'Valor Fixo' },
	{ id: 2, name: 'Percentual' }];

	operacoes = [{ id: false, name: 'Subtrai' },
	{ id: true, name: 'Acrescenta' }];
	informativoDesatuvamentoVisualizado : boolean = false;

	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	loadingAfterSubmit: boolean = false;
	viewLoading: boolean = false;

	constructor(public dialogRef: MatDialogRef<ContratoTransportadorRegraEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: ContratoTransportadorService,
		private _conciliacaoTipoService: ConciliacaoTipoService,		
		private _alertService: AlertService) {
	}

	ngOnInit() {
		this.viewLoading = true;
		this.model = this.data.model;

		this._service.obterOcorrencias().subscribe(data => {
			this.ocorrencias = data;
			this.viewLoading = false;
		});

		this._conciliacaoTipoService.get().subscribe(data => {
			this.conciliacaoTipo = data;
			this.viewLoading = false
		})

		this.createForm();
		setTimeout(() => {this.viewLoading = false;}, 500);
	}

	ngAfterContentInit() {
	}

	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'ocorrenciaId': new FormControl(this.model.ocorrenciasListId),
			'tipoCondicao': new FormControl(this.model.tipoCondicao, [Validators.required]),
			'operacao': new FormControl(this.model.operacao, [Validators.required]),
			'valor': new FormControl(this.model.valor, [Validators.required]),
			'ocorrenciasListId':new FormControl(this.model.ocorrenciasListId ),
			'conciliacaoTipoId':new FormControl(this.model.conciliacaoTipoId,[Validators.required])			
		});
	}
	get id() { return this.modelForm.get('id'); }
	get ocorrenciaId() { return this.modelForm.get('ocorrenciaId'); }
	get tipoCondicao() { return this.modelForm.get('tipoCondicao'); }
	get operacao() { return this.modelForm.get('operacao'); }
	get valor() { return this.modelForm.get('valor'); }
	get ocorrenciasListId() { return this.modelForm.get('ocorrenciasListId'); }
	get conciliacaoTipoId() { return this.modelForm.get('conciliacaoTipoId'); }	

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
	toModel(): ContratoTransportadorRegra {
		const controls = this.modelForm.controls;
		const _model = new ContratoTransportadorRegra();
		_model.id = controls['id'].value;
		_model.ocorrenciaId = controls['ocorrenciaId'].value;
		_model.tipoCondicao = controls['tipoCondicao'].value;
		_model.operacao = controls['operacao'].value;
		_model.valor = controls['valor'].value;
		_model.ocorrenciasListId = controls['ocorrenciasListId'].value;
		_model.conciliacaoTipoId = controls['conciliacaoTipoId'].value;		
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

	save(_model: ContratoTransportadorRegra) {

		if(this.validaValorMenorQueZeroMaiorQueDezMil(_model.valor))
			return;
		
		if(_model.id > 0){
			_model.ocorrenciasListId = new Array<number>();
			_model.ocorrenciasListId.push(_model.ocorrenciaId);
		}

		if((_model.ocorrenciaId == null || _model.ocorrenciaId == undefined)&& 
			(_model.ocorrenciasListId == null || _model.ocorrenciasListId == undefined || _model.ocorrenciasListId.length == 0 ))
		{
			this._alertService.show("Atenção","É necessário selecionar as ocorrências que seguirão essa regra.",'warning');
			return;
		}
		
		var itens = new Array<ContratoTransportadorRegra>();
		_model.ocorrenciasListId.forEach(oco => {
			itens.push(
				{ 
					ocorrenciaId : oco,
					ocorrencia : this.ocorrencias.find(x => x.id === oco).ocorrenciaNome,
					tipoCondicao: _model.tipoCondicao,
					operacao : _model.operacao,
					valor: _model.valor,
					ativo: true,
					id: _model.id,
					transportadorId: this.model.transportadorId,
					conciliacaoTipoId : this.model.conciliacaoTipoId,					
					dataAlteracao : null,
					dataCadastro : null,
					usuarioAlteracao : null,
					usuarioCadastro : null,
					ocorrenciasListId : null
				});
		});

		this.viewLoading = true;
		this.dialogRef.close(itens);
	}

	getTitle(): string {
		if(this.model.id == 0)
    		return 'Nova Regra';
		else
			return 'Editar Regra';
	}	

	validaValorMenorQueZeroMaiorQueDezMil(valor : number){
		return (valor === 0 || valor> 10000)  ? true : false;
	}
}