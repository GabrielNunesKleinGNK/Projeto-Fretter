import {Component, OnInit, Inject, Output, Input, EventEmitter} from '@angular/core';
import { AgendamentoEntrega } from '../../../../../../core/models/agendamentoEntrega';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
	selector: 'm-agendamentoEntrega-resumo-wizard',
	templateUrl: './agendamentoEntrega.wizard.resumo.component.html',
	styleUrls: ['./agendamentoEntrega.wizard.resumo.component.scss']
})

export class AgendamentoEntregaWizardResumoComponent implements OnInit {
	@Input() value : AgendamentoEntrega;
	@Output() valueChange = new EventEmitter<AgendamentoEntrega>();
	model: AgendamentoEntrega;
	modelForm: FormGroup;
	loadingAfterSubmit: boolean = false;
	hasFormErrors: boolean = false;

    constructor() {	}

	ngOnInit() {
		this.model = this.value;
		this.createForm();
	}

	createForm(){
		this.modelForm = new FormGroup({
			'idCanal' : new FormControl(this.model.idCanal),	
			'idRegiaoCEPCapacidade' : new FormControl(this.model.idRegiaoCEPCapacidade),	
			'cdEntrega' : new FormControl(this.model.cdEntrega),	
			'cdPedido' : new FormControl(this.model.cdPedido),	
			'cdCepOrigem' : new FormControl(this.model.cdCepOrigem,),	
			'cdProtocolo' : new FormControl(this.model.cdProtocolo),	
			'vlQuantidade' : new FormControl(this.model.vlQuantidade),	
			'dtAgendamento' : new FormControl(this.model.dtAgendamento),	
			'dsObservacao' : new FormControl(this.model.dsObservacao),	
			
			'destinatario' : new FormControl(this.model.destinatario),
			'produtos' : new FormControl(this.model.produtos),		
		});
	}
}
