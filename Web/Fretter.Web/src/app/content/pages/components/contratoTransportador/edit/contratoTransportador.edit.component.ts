import {
	Component,
	OnInit,
	Inject,
	AfterContentInit,
	Input
} from '@angular/core';

import {
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

import { ContratoTransportador } from '../../../../../core/models/contratoTransportador';
import { AlertService } from '../../../../../core/services/alert.service';
import { ContratoTransportadorService } from '../../../../../core/services/contratoTransportador.service';
import moment from 'moment';
import { DatePipe } from '@angular/common';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { combineLatest } from 'rxjs';
import { FaturaService } from '../../../../../core/services/fatura.service';
import { ToleranciaTipo } from '../../../../../core/models/toleranciaTipo';
import { ToleranciaTipoService } from '../../../../../core/services/toleranciaTipo.service';
import { ContratoTransportadorRegra } from '../../../../../core/models/contratoTransportadorRegra';
import { delay, result } from 'lodash';
import { ContratoTransportadorArquivoTipoService } from '../../../../../core/services/contratoTransportadorArquivoTipo.service';

@Component({
	selector: 'm-contratoTransportador-edit',
	templateUrl: './contratoTransportador.edit.component.html',
	styleUrls: ['./contratoTransportador.edit.component.scss']
})
export class ContratoTransportadorEditComponent implements OnInit, AfterContentInit {

	model: ContratoTransportador;
	disabled: boolean = false;
	transportadores: any[];
	transportadoresCnpj: any[];
	ocorrencias: any[];
	toleranciaTipos: Array<ToleranciaTipo>;
	ciclos: any[];
	microServicos: any[];
	modelForm: FormGroup;
	generalidadesForm: FormGroup;
	hasFormErrors: boolean = false;
	loadingAfterSubmit: boolean = false;
	viewLoading: boolean = false;

	constructor(public dialogRef: MatDialogRef<ContratoTransportadorEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: ContratoTransportadorService,
		private _serviceArquivoTipo: ContratoTransportadorArquivoTipoService,
		private _transportadorService: TransportadorService,
		private _faturaService: FaturaService,
		private _alertService: AlertService,
		private _toleranciaTipoService: ToleranciaTipoService) {
	}

	ngOnInit() {
		this.viewLoading = true;

		const transportadores$ = this._transportadorService.getTransportadoresPorEmpresa();
		const ciclos$ = this._faturaService.getCiclos();
		const toleranciaTipos$ = this._toleranciaTipoService.get();
		const microServicos$ = this._service.obterMicroServicos();
		const ocorrencias$ = this._service.obterOcorrencias();

		combineLatest([transportadores$, ciclos$, toleranciaTipos$, microServicos$, ocorrencias$])
			.subscribe(([transportadores, ciclos, toleranciaTipos, microServicos, ocorrencias]) => {
				this.transportadores = transportadores;
				this.ciclos = ciclos;
				this.toleranciaTipos = toleranciaTipos;
				this.microServicos = microServicos;
				this.ocorrencias = ocorrencias;

				if (this.model.id > 0) {
					this.getTransportadoresCnpj(this.model.transportadorId);
				}

				this.viewLoading = false;
			});

		this.model = this.data.model;
		this.model.contratoTransportadorRegra = new Array<ContratoTransportadorRegra>();
		this.createForm();
	}

	ngAfterContentInit() {
	}

	createForm() {
		this.modelForm = new FormGroup({
			'codigo': new FormControl(this.model.id, []),
			'descricao': new FormControl(this.model.descricao, [Validators.required]),
			'quantidadeTentativas': new FormControl(this.model.quantidadeTentativas, [Validators.required, Validators.pattern("[0-9]+")]),
			'taxaTentativaAdicional': new FormControl(this.model.taxaTentativaAdicional, [Validators.required]),
			'taxaRetornoRemetente': new FormControl(this.model.taxaRetornoRemetente, [Validators.required]),
			'vigenciaInicial': new FormControl(this.model.vigenciaInicial, [Validators.required]),
			'vigenciaFinal': new FormControl(this.model.vigenciaFinal, [Validators.required]),
			'transportadorId': new FormControl(this.model.transportadorId, [Validators.required]),
			'transportadorCnpjId': new FormControl(this.model.transportadorCnpjId, [Validators.required, Validators.nullValidator]),
			'transportadorCnpjCobrancaId': new FormControl(this.model.transportadorCnpjCobrancaId),
			'faturaCicloId': new FormControl(this.model.faturaCicloId, [Validators.required, Validators.nullValidator]),
			'microServicoId': new FormControl(this.model.transportadorCnpjCobrancaId, [Validators.required, Validators.nullValidator]),
			'toleranciaTipoId': new FormControl(this.model.toleranciaTipoId),
			'permiteTolerancia': new FormControl(this.model.permiteTolerancia, [Validators.required]),
			'faturaAutomatica': new FormControl(this.model.faturaAutomatica, [Validators.required]),
			'toleranciaInferior': new FormControl(this.model.toleranciaInferior, [Validators.required]),
			'recotaPesoTransportador': new FormControl(this.model.recotaPesoTransportador, [Validators.required]),
			'toleranciaSuperior': new FormControl(this.model.toleranciaSuperior, [Validators.required]),
			'conciliaEntregaFinalizada': new FormControl(this.model.conciliaEntregaFinalizada, [Validators.required]),
			'generalidades': new FormControl(this.model.contratoTransportadorRegra)
		});

		this.modelForm.get('permiteTolerancia').valueChanges.subscribe(data => this.onPermiteToleranciaFormChange(data));
	}

	onPermiteToleranciaFormChange(value: any) {
		let toleranciaTipoId = this.modelForm.get('toleranciaTipoId');
		if (value) {
			toleranciaTipoId.setValidators([Validators.required]);
		} else {
			toleranciaTipoId.clearValidators();
		}
		toleranciaTipoId.updateValueAndValidity();
	}

	get descricao() { return this.modelForm.get('descricao'); }
	get quantidadeTentativas() { return this.modelForm.get('quantidadeTentativas'); }
	get taxaTentativaAdicional() { return this.modelForm.get('taxaTentativaAdicional'); }
	get taxaRetornoRemetente() { return this.modelForm.get('taxaRetornoRemetente'); }
	get vigenciaInicial() { return this.modelForm.get('vigenciaInicial'); }
	get vigenciaFinal() { return this.modelForm.get('vigenciaFinal'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get transportadorCnpjId() { return this.modelForm.get('transportadorCnpjId'); }
	get transportadorCnpjCobrancaId() { return this.modelForm.get('transportadorCnpjCobrancaId'); }
	get faturaCicloId() { return this.modelForm.get('faturaCicloId'); }
	get toleranciaTipoId() { return this.modelForm.get('toleranciaTipoId'); }
	get permiteTolerancia() { return this.modelForm.get('permiteTolerancia'); }
	get faturaAutomatica() { return this.modelForm.get('faturaAutomatica'); }
	get toleranciaInferior() { return this.modelForm.get('toleranciaInferior'); }
	get toleranciaSuperior() { return this.modelForm.get('toleranciaSuperior'); }
	get microServicoId() { return this.modelForm.get('microServicoId'); }
	get recotaPesoTransportador() { return this.modelForm.get('recotaPesoTransportador'); }
	get conciliaEntregaFinalizada() { return this.modelForm.get('conciliaEntregaFinalizada'); }
	get generalidades() { return this.modelForm.get('generalidades'); }

	getTitle(): string {
		return this.model.id > 0 ? `Editar Contratos` : 'Cadastrar Contrato';
	}

	getTransportadoresCnpj(transportadorId: number) {
		this._transportadorService.getTransportadoresCnpj(transportadorId).subscribe(transportadoresCnpj => {
			this.transportadoresCnpj = transportadoresCnpj;
		});
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}
	
	toModel(): ContratoTransportador {
		const controls = this.modelForm.controls;
		const _model = new ContratoTransportador();

		_model.id = this.model.id;
		_model.descricao = controls['descricao'].value;
		_model.quantidadeTentativas = controls['quantidadeTentativas'].value;
		_model.taxaTentativaAdicional = controls['taxaTentativaAdicional'].value;
		_model.taxaRetornoRemetente = controls['taxaRetornoRemetente'].value;
		_model.vigenciaInicial = controls['vigenciaInicial'].value;
		_model.vigenciaFinal = controls['vigenciaFinal'].value;
		_model.transportadorId = controls['transportadorId'].value;
		_model.transportadorCnpjId = controls['transportadorCnpjId'].value;
		_model.transportadorCnpjListId = controls['transportadorCnpjId'].value;
		_model.transportadorCnpjCobrancaId = controls['transportadorCnpjCobrancaId'].value;
		_model.faturaCicloId = controls['faturaCicloId'].value;
		_model.toleranciaInferior = controls['toleranciaInferior'].value;
		_model.toleranciaSuperior = controls['toleranciaSuperior'].value;
		_model.permiteTolerancia = controls['permiteTolerancia'].value;
		_model.faturaAutomatica = controls['faturaAutomatica'].value;
		_model.toleranciaTipoId = controls['toleranciaTipoId'].value;
		_model.microServicoId = controls['microServicoId'].value;
		_model.recotaPesoTransportador = controls['recotaPesoTransportador'].value;
		_model.conciliaEntregaFinalizada = controls['conciliaEntregaFinalizada'].value;
		_model.contratoTransportadorRegra = controls['generalidades'].value;

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

	save(_model: ContratoTransportador) {
		this.loadingAfterSubmit = true;
		if (_model.id > 0) {
			_model.contratoTransportadorRegra = this.model.contratoTransportadorRegra;
			_model.contratoTransportadorArquivoTipo = this.model.contratoTransportadorArquivoTipo;
			
			this._alertService.confirmationMessage("", `As alterações do contrato entrarão em vigor nas proximas faturas.\nTem Certeza que deseja prosseguir ?`, 'Confirmar', 'Cancelar')
				.then((result) => {
					if (result.value) 
					{							
						this.salvarContratoCompleto(_model);										
					}
				});
		}
		else {
			_model.contratoTransportadorRegra = this.model.contratoTransportadorRegra;
			_model.contratoTransportadorArquivoTipo = this.model.contratoTransportadorArquivoTipo;			

			var cnpjCobrancaVazio = _model.transportadorCnpjCobrancaId == undefined;
			_model.transportadorCnpjListId.forEach(cnpj => {
				_model.transportadorCnpjId = cnpj;
				if (cnpjCobrancaVazio)
					_model.transportadorCnpjCobrancaId = cnpj;

				this.salvarContratoCompleto(_model);
			});
		}
	}

	 salvarContratoCompleto(_model): any {
		this._service.save(_model).subscribe(res => {
			if(res.id > 0)
			{
				var existeGeneralidades = (_model.contratoTransportadorRegra  != null 
					&& _model.contratoTransportadorRegra  != undefined && _model.contratoTransportadorRegra.length > 0);
				
				var existeArquivoTipo = (_model.contratoTransportadorArquivoTipo  != null 
					&& _model.contratoTransportadorArquivoTipo  != undefined && _model.contratoTransportadorArquivoTipo.length > 0);

				if(existeGeneralidades)
				{
					_model.contratoTransportadorRegra.forEach(ocorrencia => {
						ocorrencia.transportadorId = this.model.transportadorId;
					});
				
					this.salvarGeneralidade(_model.contratoTransportadorRegra, !existeArquivoTipo);
				}

				if(existeArquivoTipo)
				{
					_model.contratoTransportadorRegra.forEach(arquivo => {
						arquivo.transportadorId = this.model.transportadorId;
					});
				
					this.salvarDePara(_model.contratoTransportadorArquivoTipo);
				}

				this._alertService.show("Sucesso.", "Contrato Transportador cadastrado / alterado com sucesso. Não existia generalidade para salvar.", 'success');
				this.dialogRef.close({ _model, isEdit: true });
			}
		});
	}

	salvarGeneralidade(_model, mostraAlerta): any {
		this._service.saveRegras(_model).subscribe(res => {
			if(mostraAlerta){

				this._alertService.show("Sucesso.", "Contrato Transportador cadastrado / alterado com sucesso.", 'success');
				this.dialogRef.close({ _model, isEdit: true });
			}
		});
	}

	salvarDePara(_model): any {
		this._serviceArquivoTipo.postList(_model).subscribe(res => {
			this._alertService.show("Sucesso.", "Contrato Transportador cadastrado / alterado com sucesso.", 'success');
			this.dialogRef.close({ _model, isEdit: true });
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	permiteToleranciaChange(event) {
		if (!event.checked) {
			this.model.toleranciaTipoId = null;
			this.model.toleranciaSuperior = 0;
			this.model.toleranciaInferior = 0;
		}
	}

	delay() {
		return new Promise(resolve => setTimeout(resolve, 1));
	}
}