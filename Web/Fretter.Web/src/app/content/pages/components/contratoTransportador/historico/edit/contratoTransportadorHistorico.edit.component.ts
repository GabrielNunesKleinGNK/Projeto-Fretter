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
import { ContratoTransportadorHistoricoService } from '../../../../../../core/services/contratoTransportadorHistorico.service';
import { TransportadorService } from '../../../../../../core/services/transportador.service';
import { FaturaService } from '../../../../../../core/services/fatura.service';
import { AlertService } from '../../../../../../core/services/alert.service';
import { ToleranciaTipoService } from '../../../../../../core/services/toleranciaTipo.service';
import { ToleranciaTipo } from '../../../../../../core/models/toleranciaTipo';
import { ContratoTransportadorHistorico } from '../../../../../../core/models/contratoTransportadorHistorico';
import { ContratoTransportadorService } from '../../../../../../core/services/contratoTransportador.service';
import { combineLatest } from 'rxjs';


@Component({
	selector: 'm-contratoTransportadorHistorico-edit',
	templateUrl: './contratoTransportadorHistorico.edit.component.html',
	styleUrls: ['./contratoTransportadorHistorico.edit.component.scss']
})
export class ContratoTransportadorHistoricoEditComponent implements OnInit, AfterContentInit  {

	model: ContratoTransportadorHistorico;
	disabled: boolean = false;
	transportadores: any[];
	transportadoresCnpj: any[];
	toleranciaTipos: Array<ToleranciaTipo>;
	ciclos: any[];
	microServicos: any[];
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	loadingAfterSubmit: boolean = false;
	viewLoading: boolean = false;

	constructor(public dialogRef: MatDialogRef<ContratoTransportadorHistoricoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: ContratoTransportadorService,
		private _serviceHistorico: ContratoTransportadorHistoricoService,
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

		combineLatest([transportadores$, ciclos$, toleranciaTipos$, microServicos$])
			.subscribe(([transportadores, ciclos, toleranciaTipos, microServicos]) => {
			this.transportadores = transportadores;
			this.ciclos = ciclos;
			this.toleranciaTipos = toleranciaTipos;
			this.microServicos = microServicos;

			if(this.model.id > 0){
				this.getTransportadoresCnpj(this.model.transportadorId);
			}

			this.viewLoading = false;
		});

		this.model = this.data.model;
		console.log(this.model);
		this.createForm();
	}

	ngAfterContentInit(){
	}

	createForm() {
		this.modelForm = new FormGroup({
			'codigo': new FormControl(this.model.id, []),
			'descricao': new FormControl(this.model.descricao, [Validators.required]),
			'quantidadeTentativas': new FormControl(this.model.quantidadeTentativas, [Validators.required, Validators.pattern("[0-9]+")]),
			'taxaTentativaAdicional': new FormControl(this.model.taxaTentativaAdicional, [Validators.required]),
			'taxaRetornoRemetente': new FormControl(this.model.taxaRetornoRemetente, [Validators.required]),
			'vigenciaInicial': new FormControl( this.model.vigenciaInicial , [Validators.required]),
			'vigenciaFinal': new FormControl(this.model.vigenciaFinal, [Validators.required]),
			'transportadorId': new FormControl(this.model.transportadorId, [Validators.required]),
			'transportadorCnpjId': new FormControl(this.model.transportadorCnpjId, [Validators.required, Validators.nullValidator]),
			'transportadorCnpjCobrancaId': new FormControl(this.model.transportadorCnpjCobrancaId, [Validators.required, Validators.nullValidator]),
			'faturaCicloId': new FormControl(this.model.transportadorCnpjCobrancaId, [Validators.required, Validators.nullValidator]),
			'microServicoId': new FormControl(this.model.transportadorCnpjCobrancaId, [Validators.required]),

			'toleranciaTipoId': new FormControl(this.model.toleranciaTipoId),
			'permiteTolerancia': new FormControl(this.model.permiteTolerancia, [Validators.required]),
			'toleranciaInferior': new FormControl(this.model.toleranciaInferior, [Validators.required]),
			'toleranciaSuperior': new FormControl(this.model.toleranciaSuperior, [Validators.required])
		});

		this.modelForm.get('permiteTolerancia').valueChanges.subscribe(data => this.onPermiteToleranciaFormChange(data));
	}

	onPermiteToleranciaFormChange(value: any){
		let toleranciaTipoId = this.modelForm.get('toleranciaTipoId');
		if(value){
			toleranciaTipoId.setValidators([Validators.required]);
		}else {
			toleranciaTipoId.clearValidators();
		}
		toleranciaTipoId.updateValueAndValidity();
	}

	get descricao() { return this.modelForm.get('descricao'); }
	get quantidadeTentativas() { return this.modelForm.get('quantidadeTentativas'); }
	get taxaTentativaAdicional() { return this.modelForm.get('taxaTentativaAdicional'); }
	get taxaRetornoRemetente() { return this.modelForm.get('taxaRetornoRemetente'); }
	get vigenciaInicial() { return this.modelForm.get('vigenciaInicial');}
	get vigenciaFinal() { return this.modelForm.get('vigenciaFinal'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get transportadorCnpjId() { return this.modelForm.get('transportadorCnpjId'); }
	get transportadorCnpjCobrancaId() { return this.modelForm.get('transportadorCnpjCobrancaId'); }
	get faturaCicloId() { return this.modelForm.get('faturaCicloId'); }
	get toleranciaTipoId() { return this.modelForm.get('toleranciaTipoId'); }
	get permiteTolerancia() { return this.modelForm.get('permiteTolerancia'); }
	get toleranciaInferior() { return this.modelForm.get('toleranciaInferior'); }
	get toleranciaSuperior() { return this.modelForm.get('toleranciaSuperior'); }
	get microServicoId() { return this.modelForm.get('microServicoId'); }	

	getTitle(): string {
		return this.model.id > 0 ? `Editar Contratos` : 'Cadastrar Contrato';
	}

	getTransportadoresCnpj(transportadorId: number){
		this._transportadorService.getTransportadoresCnpj(transportadorId).subscribe(transportadoresCnpj => {
			this.transportadoresCnpj = transportadoresCnpj;
		});
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	permiteToleranciaChange(event){
		if(!event.checked){
			this.model.toleranciaTipoId = null;
			this.model.toleranciaSuperior = 0;
			this.model.toleranciaInferior = 0;
		}
	}
}