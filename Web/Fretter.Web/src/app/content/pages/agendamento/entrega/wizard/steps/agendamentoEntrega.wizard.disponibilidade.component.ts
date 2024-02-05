import { Component, OnInit, Input } from '@angular/core';
import { AgendamentoEntrega } from '../../../../../../core/models/agendamentoEntrega';
import { MatDialog } from '@angular/material';
import { MatTableDataSource } from '@angular/material/table';
import { AgendamentoEntregaService } from '../../../../../../core/services/agendamentoEntrega.service';
import { DisponibilidadeEntrega } from '../../../../../../core/models/disponibilidadeEntrega';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AgendamentoDisponibilidadeFiltro } from '../../../../../../core/models/Filters/agendamentoDisponibilidadeFiltro.model';
import { AlertService } from '../../../../../../core/services/alert.service';
import { UsuarioService } from '../../../../../../core/services/usuario.service';
import { EntregaAgendamentoControleTela } from '../../../../../../core/models/ControlesTelas/entregaAgendamentoControleTela.model';
import { AgendamentoEntregaProduto } from '../../../../../../core/models/agendamentoEntregaProduto';
import moment from 'moment';
import { MenuFretePeriodoService } from '../../../../../../core/services/menuFretePeriodo.service';

@Component({
	selector: 'm-agendamentoEntrega-disponibilidade-wizard',
	templateUrl: './agendamentoEntrega.wizard.disponibilidade.component.html',
})

export class AgendamentoEntregaWizardDisponibilidadeComponent implements OnInit {
	@Input() value: AgendamentoEntrega;
	dataSource: MatTableDataSource<DisponibilidadeEntrega> = new MatTableDataSource(new Array<DisponibilidadeEntrega>());
	displayedColumns: string[] = [/*'dataCompleta'*/'data', 'manha', 'tarde', 'noite'];
	filtro: AgendamentoDisponibilidadeFiltro = new AgendamentoDisponibilidadeFiltro();
	model: Array<DisponibilidadeEntrega> = new Array<DisponibilidadeEntrega>();
	viewLoading: boolean = true;
	disabledButtonNext: boolean = true;
	cepEntregaForm: FormGroup;
	canaisUsuario: Array<any> = new Array<any>();
	controleTela: Array<EntregaAgendamentoControleTela> = new Array<EntregaAgendamentoControleTela>();
	controleAtualTela: EntregaAgendamentoControleTela = new EntregaAgendamentoControleTela();
	edicao: boolean = false;

	constructor(
		private _service: AgendamentoEntregaService,
		private _serviceUsuario: UsuarioService,
		public dialog: MatDialog,
		public _alertService: AlertService,
		private _formBuilder: FormBuilder
	) {
	}

	ngOnInit() {
		this.viewLoading = false;
		this.canaisUsuario = new Array<any>();
		this._serviceUsuario.getCanaisUsuario().subscribe(data => {
			this.canaisUsuario = data
		});

		if (this.value.id > 0) {
			this.filtro.canalId = this.value.idCanal;
			this.filtro.cep = this.value.destinatario.cep;
			this.edicao = true;
		}
	}

	pesquisar() {
		this.controleTela =  new Array<EntregaAgendamentoControleTela>();
		this.obterDisponibilidade();
		this.destivaBotoesDeAnterior();
	}

	obterDisponibilidade() {
		this.viewLoading = true;
		this.atualizarPai();

		if (this.validarCep(this.filtro.cep) && this.filtro.canalId > 0 && this.filtro.quantidadeItens > 0) {
			this._service.obterDisponibilidade(this.filtro).subscribe(res => {
				this.model = res;
				this.load();
				this.obterDadosCep();
			});
		}
		else {
			this.viewLoading = false;
			this._alertService.show("Atenção.", "O cep deve conter 8 caracteres e os campos filial e quantidade devem estar preenchidos.", 'warning');
		}
	}

	obterDadosCep() {
		this.value.destinatario.cep = this.filtro.cep;
		this._service.obterDetalhesPorCep(this.filtro.cep).subscribe(data => {
			if (data.result) {
				this.value.destinatario.cep = data.result.cep;
				this.value.destinatario.cidade = data.result.nomeMunicipio;
				this.value.destinatario.uf = data.result.uf;
				this.value.destinatario.bairro = data.result.bairro;
				this.value.destinatario.logradouro = data.result.logradouro;
			}
			else{
				this.value.destinatario.cidade = null;
				this.value.destinatario.uf = null;
				this.value.destinatario.bairro = null;
				this.value.destinatario.logradouro = null;
				
				this.viewLoading = false;
			}
		}, error => {
			if (error)
				this._alertService.show("Atenção", "Falha na consulta do cep nos correios. No próroximo step preencha o endereço manualmente.", 'warning');
			console.log(error.error);
			this.viewLoading = false;
		});
	}

	load() {
		this.dataSource.data = this.model;
		this.viewLoading = false;
		this.edicao = false;
	}

	proximoRangeDeData(proximo: any) {
		if (this.validarCep(this.filtro.cep)) {
			if (proximo)
				this.filtro.pagina++;
			else
				this.filtro.pagina--;

			this.pesquisar();
		}
	}

	destivaBotoesDeAnterior() {
		if (this.validarCep(this.filtro.cep) && this.filtro.pagina > 1) {
			this.disabledButtonNext = false;
		}
		else {
			this.disabledButtonNext = true;
		}
	}

	validarCep(cep: string) {
		if (cep != null && cep != undefined && cep.length == 8) {
			return true;
		}
		else {
			return false;
		}
	}

	selecionarPeriodo(_model: DisponibilidadeEntrega, periodo: string) {
		if (_model[periodo] < this.value.vlQuantidade)
			this._alertService.show('Atenção', 'A quantidade disponível nesse período do dia é menor que a quantidade de itens.', 'warning');
		else {
			this.value.idRegiaoCEPCapacidade = _model.id;
			this.value.idTransportador = _model.transportadorId;
			this.value.idTransportadorCnpj = _model.transportadorCnpjId;
			this.value.dtAgendamento = _model.data;

			//this.value.dataSelecionada = _model.dataCompleta;
			this.value.periodoSelecionado = periodo;

			var item = this.controleTela.find(x => x.id == _model.id && x.descricao == periodo);

			if (item !== undefined) {
				return;
			} 

			this.controleTela.push({ id: _model.id, estado: true, descricao: periodo});
				
			if (_model[periodo] > 0) {
				this.model.map(x => {
					var elementoEditadoAnteriormente = this.controleTela.filter(x => 
						(x.estado == true && x.descricao !== periodo && x.id == _model.id) || 
						(x.estado == true && x.id !== _model.id))[0];
																				  
					if (elementoEditadoAnteriormente !== undefined && x.id === elementoEditadoAnteriormente.id) 
					{
						x[elementoEditadoAnteriormente.descricao] =
							x[elementoEditadoAnteriormente.descricao] + this.value.vlQuantidade;

						this.controleTela = new Array<EntregaAgendamentoControleTela>();
						this.controleTela.push({ id: _model.id, estado: true, descricao: periodo });
					}
					
					if (x.id === _model.id && this.controleTela.filter(x => x.id == _model.id && x.estado != false)) {
						x[periodo] = x[periodo] - this.value.vlQuantidade;
					}
				});

				this.load();
			}
		}
	}

	atualizarPai() {
		this.value.cdCepDestino = this.filtro.cep;
		this.value.vlQuantidade = this.filtro.quantidadeItens;
		this.value.idCanal = this.filtro.canalId;
		this.value.produtos = new Array<AgendamentoEntregaProduto>();
	}
}
