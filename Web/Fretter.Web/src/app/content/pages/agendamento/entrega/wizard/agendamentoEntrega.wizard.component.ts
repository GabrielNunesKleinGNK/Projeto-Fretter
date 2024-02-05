import {Component, OnInit, Input, Output, EventEmitter, Inject} from '@angular/core';
import { AgendamentoEntrega } from '../../../../../core/models/agendamentoEntrega';
import { AlertService } from '../../../../../core/services/alert.service';
import { AgendamentoEntregaService } from '../../../../../core/services/agendamentoEntrega.service';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatStepper } from '@angular/material';

@Component({
	selector: 'm-agendamentoEntrega-wizard',
	templateUrl: './agendamentoEntrega.wizard.component.html',
})

export class AgendamentoEntregaWizardComponent implements OnInit {
	isLinear = true;
	model: AgendamentoEntrega;
	modelForm: FormGroup;
	dadosAgendamentoDestinatarioForm: FormGroup;
	dadosAgendamentoProdutoForm: FormGroup;
	loadingAfterSubmit: boolean = false;
	hasFormErrors: boolean = false;
	retornoCorreios: boolean = false;

	stepOneCompleted: boolean = false;
	stepTwoCompleted: boolean = false;
	
	@Output() newItemEvent = new EventEmitter<AgendamentoEntrega>();
	
	secondFormGroup: FormGroup;

    constructor(
		@Inject(MAT_DIALOG_DATA) public data: any,
		private _alertService: AlertService, 
		private _service: AgendamentoEntregaService, 
		public dialogRef: MatDialogRef<AgendamentoEntregaWizardComponent>,
		private _formBuilder: FormBuilder
		) {	}

	ngOnInit() {
		this.model = this.data.model;
		if(this.data.model.id > 0){
			this.model.destinatario = this.data.model.destinatarios[0];
			this.stepOneCompleted = true;
			this.stepTwoCompleted = true;
		}

		this.createForm();
	}

	ngAfterViewInit(){	
		// document.getElementsByTagName('mat-step-header')[1].addEventListener('click', () => document.getElementById('step2').click()) ;
		// document.getElementsByTagName('mat-step-header')[2].addEventListener('click', () => 
		// {
		// 	document.getElementById('step2').click();
		// 	document.getElementById('step3').click();
		// });
	}

	 createForm(){
	// 	this.modelForm = new FormGroup({
	// 		'id' : new FormControl(this.model.id),
	// 		'idCanal' : new FormControl(this.model.idCanal, [Validators.required]),	
	// 		'idRegiaoCEPCapacidade' : new FormControl(this.model.idRegiaoCEPCapacidade, [Validators.required]),	
	// 		'cdEntrega' : new FormControl(this.model.cdPedido, [Validators.nullValidator, Validators.minLength(1)]),	
	// 		'cdPedido' : new FormControl(this.model.cdPedido, [Validators.nullValidator, Validators.minLength(1)]),	
	// 		'vlQuantidade' : new FormControl(this.model.vlQuantidade, [Validators.required])
	// 	});

		// this.dadosAgendamentoDestinatarioForm = new FormGroup({
		// 	'cep' : new FormControl(this.model.destinatario.cep,[Validators.required]),
		// 	'nome': new FormControl(this.model.destinatario.nome,[Validators.required]),
		// 	'cpfCnpj': new FormControl(this.model.destinatario.cpfCnpj,[Validators.required]),
		// 	'logradouro': new FormControl(this.model.destinatario.logradouro,[Validators.required]),
		// 	'numero': new FormControl(this.model.destinatario.numero, [Validators.required]),
		// 	'bairro': new FormControl(this.model.destinatario.bairro,[Validators.required]),
		// 	'cidade': new FormControl(this.model.destinatario.cidade,[Validators.required]),
		// 	'uf': new FormControl(this.model.destinatario.uf,[Validators.required]),
		// 	'telefone': new FormControl(this.model.destinatario.telefone,[Validators.required])
		// });

	// 	this.dadosAgendamentoProdutoForm = new FormGroup({'': new FormControl()});
	// 	this.model.produtos.forEach(prod => {
	// 		//this.dadosAgendamentoProdutoForm.addControl('id', new FormControl(prod.id, [Validators.required]));
	// 		this.dadosAgendamentoProdutoForm.addControl('sku', new FormControl(prod.sku, [Validators.nullValidator, Validators.minLength(1)]));
	// 		this.dadosAgendamentoProdutoForm.addControl('nome', new FormControl(prod.nome, [Validators.nullValidator, Validators.minLength(1)]));
	// 		this.dadosAgendamentoProdutoForm.addControl('valorUnitario', new FormControl(prod.valorUnitario, [Validators.required]));
	// 		this.dadosAgendamentoProdutoForm.addControl('total', new FormControl(prod.total, [Validators.required]));
	// 		this.dadosAgendamentoProdutoForm.addControl('largura', new FormControl(prod.largura, [Validators.required]));
	// 		this.dadosAgendamentoProdutoForm.addControl('altura', new FormControl(prod.altura, [Validators.required]));
	// 		this.dadosAgendamentoProdutoForm.addControl('comprimento', new FormControl(prod.comprimento, [Validators.required]));
	// 		this.dadosAgendamentoProdutoForm.addControl('peso', new FormControl(prod.peso, [Validators.required]));
	// 	});
	 }
	// get idCanal() { return this.modelForm.get('idCanal'); }
	// get idRegiaoCEPCapacidade() { return this.modelForm.get('idRegiaoCEPCapacidade'); }
	// get cdEntrega() { return this.modelForm.get('cdEntrega'); }
	// get vlQuantidade() { return this.modelForm.get('vlQuantidade'); }


	onSubmit() {
		this.newItemEvent.emit(this.model); 
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;
		
		const controlsEntrega = this.modelForm.controls;
		const controlsEntregaDestinatario = this.dadosAgendamentoDestinatarioForm.controls;
		const controlsEntregaProduto = this.dadosAgendamentoProdutoForm.controls;

		if (this.modelForm.invalid) {
			Object.keys(controlsEntrega).forEach(controlName =>
				controlsEntrega[controlName]
			);
			this.hasFormErrors = true;
			return;
		}

		if (this.dadosAgendamentoDestinatarioForm.invalid) {
			Object.keys(controlsEntregaDestinatario).forEach(controlName =>
				controlsEntregaDestinatario[controlName].markAsDirty()
			);
			this.hasFormErrors = true;
			return;
		}

		if (this.dadosAgendamentoProdutoForm.invalid) {
			Object.keys(controlsEntregaProduto).forEach(controlName =>
				controlsEntregaProduto[controlName]
			);
			this.hasFormErrors = true;
			return;
		}

		this.save(this.model);
	}

	save(_model: AgendamentoEntrega) {
		this.loadingAfterSubmit = true;

		this._service.save(_model).subscribe(res => {
			this.print();
			this._alertService.show("Sucesso.", "Agendamento cadastrado com sucesso / alterado com sucesso.", 'success');
			this.dialogRef.close({ _model, isEdit: true });
		});
	}

	print(){
		 window.print()
	}

	validaStep1(stepper: MatStepper){
		this.modelForm = this._formBuilder.group({
			'canal' : new FormControl(this.model.idCanal,[Validators.required]),
			'cep': new FormControl(this.model.destinatario.cep,[Validators.required]),
			'vlQuantidade': new FormControl(this.model.vlQuantidade,[Validators.required]),
			'idRegiaoCEPCapacidade' : new FormControl(this.model.idRegiaoCEPCapacidade, [Validators.required])
		  });

		  if(this.modelForm.invalid){
			const controlsEntregaDisponibilidade = this.modelForm.controls;	
			var obrigatoriosSemPreencher = '';
			
			Object.keys(controlsEntregaDisponibilidade).forEach(controlName =>{
				if(controlsEntregaDisponibilidade[controlName].invalid){
					obrigatoriosSemPreencher += (obrigatoriosSemPreencher === '' ? "'" : "', '" ) + controlName;
				}
			});

			this._alertService.show("Atenção", "Campos " + obrigatoriosSemPreencher + "' da disponibilidade não foram preenchidos.", "warning");
			this.stepOneCompleted = false;
			stepper.selected.completed = false;
			return;
		}

		if(this.model.destinatario.cidade == null && this.model.destinatario.uf == null) {
			this._alertService.show('Atenção','Não foi possível obter os dados dos endereço. Preencha manualmente.', 'warning')
		}

		this.stepOneCompleted = true;
		stepper.selected.completed = true;
		stepper.next();
	}

	validaStep2(stepper: MatStepper){
		this.dadosAgendamentoDestinatarioForm = this._formBuilder.group({
			'cep' : new FormControl(this.model.destinatario.cep,[Validators.required]),
			'nome': new FormControl(this.model.destinatario.nome,[Validators.required]),
			'cpfCnpj': new FormControl(this.model.destinatario.cpfCnpj,[Validators.required]),
			'logradouro': new FormControl(this.model.destinatario.logradouro,[Validators.required]),
			'numero': new FormControl(this.model.destinatario.numero, [Validators.required]),
			'bairro': new FormControl(this.model.destinatario.bairro,[Validators.required]),
			'cidade': new FormControl(this.model.destinatario.cidade,[Validators.required]),
			'uf': new FormControl(this.model.destinatario.uf,[Validators.required]),
			'telefone': new FormControl(this.model.destinatario.telefone,[Validators.required]),
			'cdPedido': new FormControl(this.model.cdPedido, [Validators.required])
		});
		
		if(this.model.produtos.length > 0)
		{
			this.dadosAgendamentoProdutoForm = new FormGroup({'': new FormControl()});
			this.model.produtos.forEach(prod => {
				//this.dadosAgendamentoProdutoForm.addControl('id', new FormControl(prod.id, [Validators.required]));
				this.dadosAgendamentoProdutoForm.addControl('sku', new FormControl(prod.sku, [Validators.required]));
				this.dadosAgendamentoProdutoForm.addControl('nome', new FormControl(prod.nome, [Validators.required]));
				this.dadosAgendamentoProdutoForm.addControl('valorProduto', new FormControl(prod.valorProduto, [Validators.required]));
				this.dadosAgendamentoProdutoForm.addControl('valorTotal', new FormControl(prod.valorTotal, [Validators.required]));
				this.dadosAgendamentoProdutoForm.addControl('largura', new FormControl(prod.largura, [Validators.required]));
				this.dadosAgendamentoProdutoForm.addControl('altura', new FormControl(prod.altura, [Validators.required]));
				this.dadosAgendamentoProdutoForm.addControl('comprimento', new FormControl(prod.comprimento, [Validators.required]));
				this.dadosAgendamentoProdutoForm.addControl('peso', new FormControl(prod.peso, [Validators.required]));
			});
		}

		if(this.dadosAgendamentoDestinatarioForm.invalid){
			const controlsEntregaDestinatario = this.dadosAgendamentoDestinatarioForm.controls;	
			var obrigatoriosSemPreencher = '';
			Object.keys(controlsEntregaDestinatario).forEach(controlName =>{
				if(controlsEntregaDestinatario[controlName].invalid){
					obrigatoriosSemPreencher += (obrigatoriosSemPreencher === '' ? "'" : "', '" ) + controlName;
				}
			});

			this._alertService.show("Atenção", "Campos " + obrigatoriosSemPreencher + "' do destinatário não foram preenchidos.", "warning")
			this.stepTwoCompleted = false;
			stepper.selected.completed = false;
			return;
		}

		if(this.dadosAgendamentoProdutoForm === undefined || this.dadosAgendamentoProdutoForm.invalid){
			const controlsEntregaProduto = this.dadosAgendamentoProdutoForm.controls;	
			var obrigatoriosSemPreencher = '';
			Object.keys(controlsEntregaProduto).forEach(controlName =>{
				if(controlsEntregaProduto[controlName].invalid)
					obrigatoriosSemPreencher += (obrigatoriosSemPreencher === '' ? "'" : "', '" ) + controlName;
			});
			this._alertService.show("Atenção", "Campos " + obrigatoriosSemPreencher + "' do produto não foram preenchidos.", "warning")
			this.stepTwoCompleted = false;
			stepper.selected.completed = false;
			return;
		}
		
		this.stepTwoCompleted = true;
		stepper.selected.completed = true;
		stepper.next()
	}
}
