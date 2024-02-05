import {
	Component,
	OnInit,
	Inject,
	AfterViewInit,	
} from '@angular/core';

import {	
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AgendamentoRegra } from '../../../../../core/models/agendamentoRegra.model';
import { AgendamentoRegraService } from '../../../../../core/services/agendamentoRegra.service';
import { AlertService } from '../../../../../core/services/alert.service';
import { Clausula } from '../../../../../core/models/clausula.model';
import { combineLatest } from 'rxjs';
import { OperadorComparacao } from '../../../../../core/models/operadorComparacao.model';
import { QueryClausula } from '../../../../../core/models/queryClausula.model';
import { RegraItem } from '../../../../../core/models/regraItem.model';
import { CanalService } from '../../../../../core/services/canal.service';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { Canal } from '../../../../../core/models/Fusion/Canal';
import { Transportador } from '../../../../../core/models/transportador.model';
import { RegraTipo } from '../../../../../core/models/regraTipo.model';

@Component({
	selector: 'm-agendamentoRegra-edit',
	templateUrl: './agendamentoRegra.edit.component.html',
})

export class AgendamentoRegraEditComponent implements OnInit {
	model: AgendamentoRegra;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	clausulas: Array<Clausula>;
	regraTiposOperadores: Array<OperadorComparacao>;
	regrasTipo: Array<RegraTipo>
	queries: Array<QueryClausula> = [];
	combine: boolean = false;

	lstCanais : Array<Canal>;
	transportadores: Array<Transportador>;
	transportadoresCnpj: any[];

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: any,
		public dialogRef: MatDialogRef<AgendamentoRegraEditComponent>,
		private fb: FormBuilder,
		private _service: AgendamentoRegraService,
		private _transportadorService: TransportadorService,
		private _canalService: CanalService,
		private _alertService: AlertService,
	) {	}

	ngOnInit() {
		this.model = this.data.model;

		const regraTipoOperador$ = this._service.getRegraTipoOperador();
		const clausulas$ = this._service.getRegraTipoItem();
		const transportadores$ = this._transportadorService.getTransportadoresPorEmpresa();
		const canais$ = this._canalService.getCanaisPorEmpresa();
		const regrasTipo$ = this._service.getRegraTipo();

		combineLatest([regraTipoOperador$, clausulas$, regrasTipo$, transportadores$, canais$])
			.subscribe(([regraTipoOperador, clausulas, regrasTipo, transportadores, canais]) => {
				this.regraTiposOperadores = regraTipoOperador;
				this.clausulas = clausulas;
				this.regrasTipo = regrasTipo
				this.transportadores = transportadores;
				this.lstCanais = canais;
		
				this.viewLoading = false;
				this.combine = true;

				if(this.model.id > 0){
					this.queries = this.mapRegraItemToQueryClausula(this.model);
					this.getTransportadoresCnpj(this.model.transportadorId);
				}
		});

		this.createForm();
		this.regraTipoId.disable();
	}

	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'nome': new FormControl(this.model.nome, [Validators.required]),
			'regraTipoId' : new FormControl(this.model.regraTipoId,[]),
			'canalId': new FormControl(this.model.canalId, []),
			'transportadorId': new FormControl(this.model.transportadorId, []),
			'transportadorCnpjId': new FormControl(this.model.transportadorCnpjId, []),
			'definirVigencia': new FormControl(this.model.definirVigencia, []),
			'dataInicio': new FormControl(this.model.dataInicio, []),
			'dataTermino': new FormControl(this.model.dataTermino, [])
		});
	}

	get id() { return this.modelForm.get('id'); }
	get nome() { return this.modelForm.get('nome'); }
	get regraTipoId() { return this.modelForm.get('regraTipoId'); }
	get canalId() { return this.modelForm.get('canalId'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get transportadorCnpjId() { return this.modelForm.get('transportadorCnpjId'); }
	get definirVigencia() { return this.modelForm.get('definirVigencia'); }
	get dataInicio() { return this.modelForm.get('dataInicio'); }
	get dataTermino() { return this.modelForm.get('dataTermino'); }


	getTitle(): string {
		return this.model.id > 0 ? `Edição de Regra de Agendamento` : 'Regra de Agendamento';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}


	toModel(): AgendamentoRegra {
		const controls = this.modelForm.controls;
		const _model = new AgendamentoRegra();

		_model.id = this.model.id;
		_model.nome = controls['nome'].value;
		_model.regraTipoId = controls['regraTipoId'].value;
		_model.canalId = controls['canalId'].value;
		_model.transportadorId = controls['transportadorId'].value;
		_model.transportadorCnpjId = controls['transportadorCnpjId'].value;
		_model.definirVigencia = controls['definirVigencia'].value;
		_model.dataInicio = controls['dataInicio'].value;
		_model.dataTermino = controls['dataTermino'].value;

		return _model;
	}

	getTransportadoresCnpj(transportadorId: number) {
		this._transportadorService.getTransportadoresCnpj(transportadorId).subscribe(transportadoresCnpj => {
			this.transportadoresCnpj = transportadoresCnpj;
		});
	}

	getTransportadoresCnpjChange(transportadorId: number) {
		this.getTransportadoresCnpj(transportadorId);
		this.transportadorCnpjId.reset();
	}

	limparComboTransportadorFilial(){
		this.transportadoresCnpj = [];
		this.transportadorCnpjId.reset();
	}
	
	definirVigenciaChange(){
		if(this.model.definirVigencia){
			this.dataInicio.setValidators([Validators.required]);
			this.dataTermino.setValidators([Validators.required]);
		} else {
			this.dataInicio.clearValidators();
			this.dataTermino.clearValidators();
		}
		
		this.dataInicio.updateValueAndValidity();
		this.dataTermino.updateValueAndValidity();
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

		if(this.queries == null ||this.queries.length == 0){
			this._alertService.show("Erro.", "Defina ao menos uma clásula", 'error');
			return;
		}

		const model = this.toModel();
		
		let arrRegraitem = new Array<RegraItem>();
		let regraItem = new RegraItem();

		this.queries.forEach(element => {
			
			regraItem.id = element.queryClausulaId;

			regraItem.regraGrupoItemId = element.grupoId;
			regraItem.regraGrupoItem.id = element.grupoId;
			regraItem.regraGrupoItem.grupo = element.descricaoGrupo;
			regraItem.regraGrupoItem.tipo = element.descricaoOperadorLogico;

			regraItem.regraTipoItemId = element.clausulaId;
			regraItem.regraTipoOperadorId = element.operadorComparacaoId;

			regraItem.valor = element.valorClausula;
			regraItem.valorInicial = element.valorClausulaInicial;
			regraItem.valorFinal = element.valorClausulaInicial;

			arrRegraitem.push(regraItem);
			regraItem = new RegraItem();
		});

		model.regraItens = arrRegraitem;

		this.save(model);
	}

	save(_model: AgendamentoRegra) {
		if(_model.id > 0){
			this._service.putAlterarRegra(_model).subscribe(res => {
				this._alertService.show("Sucesso.", "Regra alterada com sucesso.", 'success');
				this.dialogRef.close({ _model, isEdit: true });
			}, error => {
				let errorMessage = error.errors[0].message || "Erro ao alterar regra";
				this._alertService.show("Erro.", errorMessage, 'error');
			});
		} else {
			this._service.postIncluirRegra(_model).subscribe(res => {
				this._alertService.show("Sucesso.", "Regra cadastrada", 'success');
				this.dialogRef.close({ _model, isEdit: true });
			}, error => {
				let errorMessage = error.errors[0].message || "Erro ao alterar regra";
				this._alertService.show("Erro.", errorMessage, 'error');
			});
		}


	}

	mapRegraItemToQueryClausula(agendamentoRegra: AgendamentoRegra) : Array<QueryClausula>{

		let queriesClausula = new Array<QueryClausula>();
		let query = new QueryClausula();

		let regraGrupoItemId = agendamentoRegra.regraItens[0].regraGrupoItem.id;
		let regraGrupoItemTipo = agendamentoRegra.regraItens[0].regraGrupoItem.tipo;
		let regraGrupoItemDescricao = agendamentoRegra.regraItens[0].regraGrupoItem.grupo;

		let operadorLogicoId = regraGrupoItemTipo === 'E' ? 1 : 2;

		agendamentoRegra.regraItens.forEach(element => {
			let clausula = this.clausulas.find(clausula => clausula.id == element.regraTipoItemId)
			let operadorComparacao = this.regraTiposOperadores.find(operador => operador.id == element.regraTipoOperadorId)

			query.queryClausulaId = element.id;

			query.operadorLogicoId = operadorLogicoId;

			query.grupoId = regraGrupoItemId;
			query.descricaoGrupo = regraGrupoItemDescricao
			query.descricaoOperadorLogico = regraGrupoItemTipo;

			query.clausulaId = element.regraTipoItemId;
			query.descricaoClausula = clausula.nome;

			query.operadorComparacaoId = operadorComparacao.id;
			query.descricaoOperadorComparacao = operadorComparacao.nome;

			query.valorClausula = element.valor;
			query.valorClausulaInicial = element.valorInicial;
			query.valorClausulaFinal = element.valorFinal;

			queriesClausula.push(query);

			query = new QueryClausula();
		});

		return queriesClausula
	}
}