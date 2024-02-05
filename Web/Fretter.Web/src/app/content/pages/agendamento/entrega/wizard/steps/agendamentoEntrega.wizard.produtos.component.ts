import {Component, OnInit, Inject, EventEmitter, Input, Output, ChangeDetectorRef} from '@angular/core';
import { AgendamentoEntrega } from '../../../../../../core/models/agendamentoEntrega';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AgendamentoEntregaProduto } from '../../../../../../core/models/agendamentoEntregaProduto';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { EntregaAgendamentoControleTela } from '../../../../../../core/models/ControlesTelas/entregaAgendamentoControleTela.model';
import { ProdutoService } from '../../../../../../core/services/produto.service';
import { AlertService } from '../../../../../../core/services/alert.service';
import { PesquisaProdutoComponent } from '../../../../components/shared/pesquisaProduto/pesquisaProduto.component';

@Component({
	selector: 'm-agendamentoEntrega-produtos-wizard',
	templateUrl: './agendamentoEntrega.wizard.produtos.component.html',
	styleUrls: ['./agendamentoEntrega.wizard.produtos.component.scss']
})

export class AgendamentoEntregaWizardProdutosComponent implements OnInit {
	@Input() value : AgendamentoEntrega;

	dataSource: MatTableDataSource<AgendamentoEntregaProduto>;
	displayedColumns: string[] = ['sku','nome', 'altura', 'largura', 'comprimento','peso',  'valorProduto', 'valorTotal','actions'];
	editing : boolean = false;
	model: AgendamentoEntregaProduto;
	dadosProdutoForm: FormGroup;
	controleTela: Array<EntregaAgendamentoControleTela> = new Array<EntregaAgendamentoControleTela>();
	
    constructor(
		private fb: FormBuilder, 
		private _serviceProduto : ProdutoService,
		public _alertService: AlertService, 
		private cdr: ChangeDetectorRef,
		public dialog: MatDialog) { }

	ngOnInit() {
		this.model = new AgendamentoEntregaProduto();
		this.createForm();

		this.dataSource = new MatTableDataSource(this.value.produtos);
		this.cdr.detectChanges();
		this.carregaControleTela();
	}

	createForm(){
		this.dadosProdutoForm = new FormGroup({
			'id' : new  FormControl(this.model.id),
			'sku' : new FormControl(this.model.sku),
			'ean': new FormControl(this.model.ean),
			'nome': new FormControl(this.model.nome),
			'valorProduto': new FormControl(this.model.valorProduto),
			'valorTotal': new FormControl(this.model.valorTotal),
			'largura': new FormControl(this.model.largura),
			'altura': new FormControl(this.model.altura),
			'comprimento': new FormControl(this.model.comprimento),
			'peso': new FormControl(this.model.peso)
		});
	}

	load() {
		if(this.value.produtos.length == 0)
		{
			this.value.produtos = new Array<AgendamentoEntregaProduto>();
			for(var indice = 0; indice <  this.value.vlQuantidade; indice++)
			{
				this.value.produtos.push(
				{ 
					id: 0,
					linha: indice + 1,  
					sku : null, 
					nome : null, 
					ean : null,
					valorTotal : 0,
					ativo : true,
					valorProduto : 0,
					altura: 0,
					largura:0,
					comprimento:0,
					peso:0,
					usuarioCadastro : null,
					usuarioAlteracao : null,
					dataAlteracao : null,
					dataCadastro : null
				});
			}
		}
		this.carregaControleTela();
		this.dataSource = new MatTableDataSource(this.value.produtos);
		this.cdr.detectChanges();
	}

	atualizarItem(_model : AgendamentoEntregaProduto){
		this.controleTela.map(x => {
			if(x.id == _model.linha) 
			x.estado = false;
		});
		
		this.load();
	}

	editarItem(_model : AgendamentoEntregaProduto){
		this.controleTela.map(x => {
			if(x.id == _model.linha) 
				x.estado = true;
			});
	}
	
	controleEdicaoProdutos(_model : AgendamentoEntregaProduto){	
		var estadoDoRegistro = this.controleTela.filter(x => x.id == _model.linha);
		if(estadoDoRegistro.length > 0)
			return estadoDoRegistro[0].estado;
	}

	buscarSku(_model : AgendamentoEntregaProduto){
		if(_model.sku !== undefined && _model.sku  !==  null && _model.sku  !==  '')
		{
			this._serviceProduto.getProdutoPorSku(_model.sku).subscribe(data => 
			{
				if(data){

					this.setProduto(_model, data);
					this.load();
				}
				else{
					this.abrirPesquisaProduto(_model);
				}
			});
		}		
		else
			this.abrirPesquisaProduto(_model);
	}

	carregaControleTela(){
		this.value.produtos.forEach(prod => 
			{
				this.controleTela.push({ id: prod.linha, estado : false, descricao : null })
			});
	}

	atualizaValorTotalItem(){
		this.model.valorTotal = this.model.valorProduto;
	}

	private abrirPesquisaProduto(_model : AgendamentoEntregaProduto) {
		const dialogRef = this.dialog.open(PesquisaProdutoComponent, { width: '1000px' });

		dialogRef.afterClosed().subscribe(res => {
			if(res !== null && res !== undefined)
				this.setProduto(_model, res.data);
		});
	}

	private setProduto(model: AgendamentoEntregaProduto, data: any){
		model.sku = data.codigo;
		model.comprimento = data.comprimento;
		model.altura = data.altura;
		model.largura = data.largura;
		model.peso = data.pesoLiq;
		model.valorProduto = data.preco;
		model.nome = data.nome;
		model.valorTotal = data.preco;
	}
}
