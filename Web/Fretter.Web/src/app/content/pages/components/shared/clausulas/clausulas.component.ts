import { Component,	OnInit,	Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Clausula } from '../../../../../core/models/clausula.model';
import { OperadorComparacao } from '../../../../../core/models/operadorComparacao.model';
import { QueryClausula } from '../../../../../core/models/queryClausula.model';
import { MatTableDataSource } from '@angular/material';

@Component({
	selector: 'm-clausulas',
	templateUrl: './clausulas.component.html'
})
export class ClausulasComponent implements OnInit {

	@Input() clausulas: Array<Clausula>;
	@Input() operadoresComparacao: Array<OperadorComparacao>;
	@Input() queries: Array<QueryClausula>;

	model: QueryClausula;
	modelForm: FormGroup;
	
	dataSource: MatTableDataSource<QueryClausula> = new MatTableDataSource(Array<QueryClausula>());
	displayedColumns: string[] = ['operadorLogico', 'descricaoClausula', 'operadorComparacao', 'valor','acoes'];

	operadoresLogicos = [{id: 1, descricao: 'E'}, {id: 2, descricao: 'Ou'}];

	grupoId: number;

	somenteNumeros: boolean;
	somenteLetras: boolean;
	tipoDado: string;
	hiddenValue: boolean;
	hiddenRangeValue: boolean;

	ngOnInit() {
		this.tipoDado = 'text';
		this.hiddenValue = true;
		this.hiddenRangeValue = true;

		this.model = new QueryClausula();

		if (this.queries != undefined && this.queries != null && this.queries.length > 0 ){
			this.model.descricaoGrupo = this.queries[0].descricaoGrupo;
			this.model.operadorLogicoId = this.queries[0].operadorLogicoId;
			this.grupoId = this.queries[0].grupoId;
		} 

		this.createForm();

		this.dataSource.data = this.queries;

		this.habilitaComboOperadorLogico(); 
	}

	habilitaComboOperadorLogico() {
		if(this.dataSource.data.length > 0){
			this.operadorLogicoId.disable();
		} else {
			this.operadorLogicoId.enable();
		}
	}
	
	createForm(){
		this.modelForm = new FormGroup({
			'descricaoGrupo': new FormControl(this.model.descricaoGrupo, [Validators.required]),
			'operadorLogicoId': new FormControl(this.model.operadorLogicoId, [Validators.required]),
			'clausulaId': new FormControl(this.model.clausulaId, [Validators.required]),
			'operadorComparacaoId': new FormControl(this.model.operadorComparacaoId, [Validators.required]),
			'valorClausula': new FormControl(this.model.valorClausula, []),
			'valorClausulaInicial': new FormControl(this.model.valorClausulaInicial, []),
			'valorClausulaFinal': new FormControl(this.model.valorClausulaFinal, []),
		});

	}
	
	get descricaoGrupo() { return this.modelForm.get('descricaoGrupo'); }
	get operadorLogicoId() { return this.modelForm.get('operadorLogicoId'); }
	get clausulaId() { return this.modelForm.get('clausulaId'); }
	get operadorComparacaoId() { return this.modelForm.get('operadorComparacaoId'); }
	get valorClausula() { return this.modelForm.get('valorClausula'); }
	get valorClausulaInicial() { return this.modelForm.get('valorClausulaInicial'); }
	get valorClausulaFinal() { return this.modelForm.get('valorClausulaFinal'); }

	montarCampoValue(id: number) {

		var clausula = this.clausulas.find(clausula => clausula.id == id)
		
		if(id == undefined || id == 0){
			this.hiddenValue = true;
			return;
		}

		this.valorClausula.markAsUntouched();
		this.valorClausulaInicial.markAsUntouched();
		this.valorClausulaFinal.markAsUntouched();

		if(clausula.range){
			this.hiddenRangeValue = false;
			this.hiddenValue = true;

			this.valorClausula.clearValidators();
			this.valorClausula.updateValueAndValidity();
			
			this.valorClausulaInicial.setValidators([Validators.required])
			this.valorClausulaFinal.setValidators([Validators.required])
		} else {
			this.hiddenValue = false;
			this.hiddenRangeValue = true;

			this.valorClausulaFinal.clearValidators();
			this.valorClausulaInicial.clearValidators();

			this.valorClausulaFinal.updateValueAndValidity();
			this.valorClausulaInicial.updateValueAndValidity();

			this.valorClausula.setValidators([Validators.required]);
		}


		if (clausula.dadoTipo == 1){
			this.somenteLetras = true;
			this.somenteNumeros = false;
			this.tipoDado = 'text';
		} else if (clausula.dadoTipo == 2){
			this.somenteNumeros = true;
			this.somenteLetras = false;
			this.tipoDado = 'text';
		}else {
			this.tipoDado = 'date';
			this.somenteNumeros = false;
			this.somenteLetras = false;
		}
	}

	limparCampos(){
		const controls = this.modelForm.controls;

		Object.keys(controls).forEach(controlName =>
			controls[controlName].markAsUntouched()
		);

		this.clausulaId.reset();
		this.operadorComparacaoId.reset();
		this.valorClausula.reset();
		this.valorClausulaInicial.reset();
		this.valorClausulaFinal.reset();

		this.tipoDado = 'text';
		this.hiddenValue = true;
		this.hiddenRangeValue = true;
	}

	toModel(): QueryClausula {
		const controls = this.modelForm.controls;
		const _model = new QueryClausula();

		_model.descricaoGrupo = controls['descricaoGrupo'].value;
		_model.operadorLogicoId = controls['operadorLogicoId'].value;
		_model.clausulaId = controls['clausulaId'].value;
		_model.operadorComparacaoId = controls['operadorComparacaoId'].value;
		_model.valorClausula = controls['valorClausula'].value;
		_model.valorClausulaInicial = controls['valorClausulaInicial'].value;
		_model.valorClausulaFinal = controls['valorClausulaFinal'].value;

		return _model;
	}

	addNovaClausula() {
		const controls = this.modelForm.controls;

		if (this.modelForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			return;
		}

		var query = this.toModel();

		var operadorLogico = this.operadoresLogicos.find(operador => operador.id == query.operadorLogicoId).descricao
		var clausula = this.clausulas.find(clausula => clausula.id == query.clausulaId).nome
		var operadorComparacao = this.operadoresComparacao.find(operador => operador.id == query.operadorComparacaoId).nome

		query.descricaoOperadorLogico = operadorLogico;
		query.descricaoClausula = clausula;
		query.descricaoOperadorComparacao = operadorComparacao;

		query.grupoId = this.grupoId;

		this.queries.push(query);

		this.dataSource.data = this.queries;

		this.habilitaComboOperadorLogico();

		this.limparCampos();
	}

	removeClausula(model: QueryClausula) {

		let index = this.queries.findIndex(d => d.clausulaId === model.clausulaId && 
			d.operadorLogicoId === model.operadorLogicoId && 
			d.operadorComparacaoId === model.operadorComparacaoId && 
			d.valorClausula === model.valorClausula
		);
		
        this.queries.splice(index, 1);

		this.dataSource.data = this.queries;

		this.habilitaComboOperadorLogico(); 
	}
}