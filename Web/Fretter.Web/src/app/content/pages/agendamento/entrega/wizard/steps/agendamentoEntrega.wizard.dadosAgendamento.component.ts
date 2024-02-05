import {Component, OnInit, Inject, EventEmitter, Input, Output} from '@angular/core';
import { AgendamentoEntrega } from '../../../../../../core/models/agendamentoEntrega';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../../../../../../core/services/alert.service';


@Component({
	selector: 'm-agendamentoEntrega-dadosAgendamento-wizard',
	templateUrl: './agendamentoEntrega.wizard.dadosAgendamento.component.html',
})

export class AgendamentoEntregaWizardDadosAgendamentoComponent implements OnInit {
	@Input() value : AgendamentoEntrega;
	dadosAgendamentoForm: FormGroup;
	model: AgendamentoEntrega;

    constructor(private fb: FormBuilder, public _alertService: AlertService) {			
	}

	ngOnInit() {		
		this.model = this.value;
		this.createForm();
	}

	createForm(){
		this.dadosAgendamentoForm = new FormGroup({
			'cep' : new FormControl(this.model.destinatario.cep, [Validators.required]),
			'nome': new FormControl(this.model.destinatario.nome, [Validators.required]),
			'cpfCnpj': new FormControl(this.model.destinatario.cpfCnpj, [Validators.required]),
			'logradouro': new FormControl(this.model.destinatario.logradouro, [Validators.required]),
			'numero': new FormControl(this.model.destinatario.numero, [Validators.required]),
			'complemento': new FormControl(this.model.destinatario.complemento),
			'bairro': new FormControl(this.model.destinatario.bairro, [Validators.required]),
			'cidade': new FormControl(this.model.destinatario.cidade, [Validators.required]),
			'uf': new FormControl(this.model.destinatario.uf, [Validators.required]),
			'pontoReferencia': new FormControl(this.model.destinatario.pontoReferencia),
			'telefone': new FormControl(this.model.destinatario.telefone, [Validators.required]),
			'cdPedido': new FormControl(this.model.cdPedido, [Validators.required]),
			'email' : new FormControl(this.model.destinatario.email),
			'dsObservacao': new FormControl(this.model.dsObservacao),
		});
	}
	get cep() { return this.dadosAgendamentoForm.get('cep'); }
	get cpfCnpj() { return this.dadosAgendamentoForm.get('cpfCnpj'); }
	get logradouro() { return this.dadosAgendamentoForm.get('logradouro'); }
	get numero() { return this.dadosAgendamentoForm.get('numero'); }
	get bairro() { return this.dadosAgendamentoForm.get('bairro'); }
	get cidade() { return this.dadosAgendamentoForm.get('cidade'); }
	get uf() { return this.dadosAgendamentoForm.get('uf'); }
	get telefone() { return this.dadosAgendamentoForm.get('telefone'); }
	get cdPedido() { return this.dadosAgendamentoForm.get('cdPedido'); }
	get nome() { return this.dadosAgendamentoForm.get('nome'); }
}
