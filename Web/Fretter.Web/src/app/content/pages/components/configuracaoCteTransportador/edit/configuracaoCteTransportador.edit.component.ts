import {
	Component,
	OnInit,
	Inject,
	AfterViewInit
} from '@angular/core';

import {
	MatChipInputEvent,
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

import { AlertService } from '../../../../../core/services/alert.service';
import { ConfiguracaoCteTransportador } from '../../../../../core/models/configuracaoCteTransportador';
import { ConfiguracaoCteTipoService } from '../../../../../core/services/configuracaoCteTipo.service';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { combineLatest } from 'rxjs';
import { ConfiguracaoCteTransportadorService } from '../../../../../core/services/configuracaoCteTransportador.service';
import {COMMA, ENTER} from '@angular/cdk/keycodes';

@Component({
	selector: 'm-configuracaoCteTransportador-edit',
	templateUrl: './configuracaoCteTransportador.edit.component.html',
	styleUrls: ['./configuracaoCteTransportador.edit.component.scss']
})
export class ConfiguracaoCteTransportadorEditComponent implements OnInit, AfterViewInit{

	model: ConfiguracaoCteTransportador;
	disabled: boolean = false;
	tipos: any[];
	transportadores: any[];
	transportadoresCnpj: any[];
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	loadingAfterSubmit: boolean = false;
	viewLoading: boolean = false;
	
	readonly separatorKeysCodes = [ENTER, COMMA];
	labels: any[] = [];
	addOnBlur = true;


	constructor(public dialogRef: MatDialogRef<ConfiguracaoCteTransportadorEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: ConfiguracaoCteTransportadorService,
		private _alertService: AlertService,
		private tipoService: ConfiguracaoCteTipoService,
		private transportadorService: TransportadorService) {
	}

	ngAfterViewInit(): void {
	}

	ngOnInit() {
		
		this.model = this.data.model;
		this.createForm();

		if(this.model.id > 0){
			this.model.transportadorId = this.model.transportadorCnpj.transportadorId;
			this.getTransportadoresCnpj(this.model.transportadorId);
			
			if(this.model.alias)
				for(let label of this.model.alias.split(";")){
					this.labels.push({name: label});
				}
		}

		this.viewLoading = true;
		const tipos$ = this.tipoService.get();
		const transportadores$ = this.transportadorService.getTransportadoresPorEmpresa();

		combineLatest([tipos$, transportadores$]).subscribe(([tipos, transportadores]) => {
			this.tipos = tipos;
			this.transportadores = transportadores;
			this.viewLoading = false;
		});
	}

	createForm() {
		this.modelForm = new FormGroup({
			'codigo': new FormControl(this.model.id, []),
			'alias': new FormControl(this.model.alias, [Validators.required]),
			'transportadorCnpjId': new FormControl(this.model.transportadorCnpjId, [Validators.required]),
			'transportadorId': new FormControl(this.model.transportadorCnpjId, [Validators.required]),
			'configuracaoCteTipoId': new FormControl(this.model.configuracaoCteTipoId, [Validators.required]),
		});
	}

	get alias() { return this.modelForm.get('alias'); }
	get transportadorCnpjId() { return this.modelForm.get('transportadorCnpjId'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get configuracaoCteTipoId() { return this.modelForm.get('configuracaoCteTipoId'); }


	getTitle(): string {
		return this.model.id > 0 ? `Editar Configuração CTe` : 'Cadastrar Configuração CTe';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	toModel(): ConfiguracaoCteTransportador {
		const controls = this.modelForm.controls;
		const _model = new ConfiguracaoCteTransportador();

		_model.id = this.model.id;
		_model.transportadorCnpjId = controls['transportadorCnpjId'].value;
		_model.configuracaoCteTipoId = controls['configuracaoCteTipoId'].value;
		_model.alias = controls['alias'].value;
		return _model;
	}

	changeChip(){
		const controls = this.modelForm.controls;
		let alias = this.labels.map(x => x.name).join(";");
		this.model.alias = alias;
		controls['alias'].setValue(alias);
		controls['alias'].markAsTouched();
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

	transportadoresChange(event: any){
		if(!event || !event.id)
			return this.transportadoresCnpj = [];
		this.getTransportadoresCnpj(event.id);
		this.model.transportadorCnpjId = null;
	}

	getTransportadoresCnpj(transportadorId: Number){
		this.transportadorService.getTransportadoresCnpj(transportadorId).subscribe( transportadoresCnpj => {
			this.transportadoresCnpj = transportadoresCnpj;
		});
	}

	save(_model: ConfiguracaoCteTransportador) {
		this.loadingAfterSubmit = true;

		this._service.save(_model).subscribe(res => {
			this._alertService.show("Sucesso.", "Configuração CTe cadastrado / alterado com sucesso.", 'success');
			this.dialogRef.close({ _model, isEdit: true });
		}, error => {
			this._alertService.show("Erro.", "Houve um erro ao Configurar a CTe.", 'error');
			console.log(error);
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	obterNomeTipo(){
		if(this.tipos && this.tipos.filter(x => x.id == this.model.configuracaoCteTipoId).length > 0)
			return this.tipos.filter(x => x.id == this.model.configuracaoCteTipoId)[0].descricao;
		else 
			return "";
	}

	add(event: MatChipInputEvent): void {
		const value = (event.value || '').trim();
		if (value) {
		  this.labels.push({name: value.toUpperCase()});
		}
		event.input.value = "";
		this.changeChip();
	}
	
	remove(fruit: any): void {
		const index = this.labels.indexOf(fruit);

		if (index >= 0) {
			this.labels.splice(index, 1);
		}
		this.changeChip();
	}
}