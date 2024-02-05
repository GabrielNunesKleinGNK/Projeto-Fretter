import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef, MatPaginator, MatTableDataSource } from '@angular/material';
import { FaturaConciliacaoService } from '../../../../../core/services/faturaConciliacao.service';
import { FaturaConciliacaoIntegracao } from '../../../../../core/models/faturaConciliacaoIntegracao.model';
import { ResultadoFaturaConciliacaoIntegracao } from '../../../../../core/models/resultadoFaturaConciliacaoIntegracao.model';
import { JsonViewComponent } from '../../logDashboard/logDashboard.jsonView/jsonView.component';
import { FaturaConciliacaoReenvio } from '../../../../../core/models/faturaConciliacaoReenvio.model';
import { AlertService } from '../../../../../core/services/alert.service';
import { RetornoReenvioFaturaConciliacao } from '../../../../../core/models/retornoReenvioFaturaConciliacao.model';
import { JsonIntegracaoFaturaConciliacao } from '../../../../../core/models/jsonIntegracaoFaturaConciliacao.model';

enum FiltroSelecionado {
  Todos = 1,
  Sucesso,
  Insucesso,
  NaoDefinido,
}

@Component({
  selector: 'm-faturaConciliacaoIntegracao',
  templateUrl: './faturaConciliacaoIntegracao.component.html',
  styleUrls: ['./faturaConciliacaoIntegracao.component.scss']
})

export class FaturaConciliacaoIntegracaoComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  dataSource: MatTableDataSource<FaturaConciliacaoIntegracao> = new MatTableDataSource(new Array<FaturaConciliacaoIntegracao>());
  displayedColumns: string[] = ['notaFiscal', 'serie', 'valorFrete', 'valorAdicional', 'httpStatus', 'actions'];

  modelList: ResultadoFaturaConciliacaoIntegracao;
  retornoReenvioFaturaConciliacao: RetornoReenvioFaturaConciliacao;
  jsonIntegracaoFaturaConciliacao: JsonIntegracaoFaturaConciliacao;
  selectedList: Array<FaturaConciliacaoIntegracao>;

  viewLoading: boolean = true;

  pageSize: number = 5;
  currentPage: number = 0;
  resultsLength: number = 0;

  totalBotaoReenvio: number = 0;
  esconderBotaoReenvioMassivo: boolean = true;
  
  start: number = 0;
  end: number = 5;

  constructor(
    public dialogRef: MatDialogRef<FaturaConciliacaoIntegracaoComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
    private _service: FaturaConciliacaoService,
    public dialog: MatDialog,
    private _alertService: AlertService,
  ) { }

  ngOnInit() {
    this.load();
    this.modelList = new ResultadoFaturaConciliacaoIntegracao;
    this.selectedList = new Array<FaturaConciliacaoIntegracao>()
  }

  load() {
    this.pesquisar();
  }

  pesquisar() {
    this.viewLoading = true;
    this._service.GetAllFaturaConciliacaoIntegracao(this.data).subscribe(result => {
      this.modelList = result;
      this.selectedList = this.modelList.integracoes;
      this.setListFaturaConciliacaoIntegracao();
      this.viewLoading = false;
    });
  }

  setListFaturaConciliacaoIntegracao() {
    this.dataSource.data = this.selectedList.slice(this.start, this.end);
    this.resultsLength = this.selectedList.length;
  }

	pageChange(event){
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;

    this.start = this.currentPage * this.pageSize;
    this.end = (this.currentPage + 1) * this.pageSize;
    
    this.setListFaturaConciliacaoIntegracao();
	}

  selecionarStatus(filtro: number){
    this.esconderBotaoReenvioMassivo = true;

    switch(filtro) { 
      case FiltroSelecionado.Todos: { 
        this.selectedList = this.modelList.integracoes;
        break; 
      } 
      case FiltroSelecionado.Sucesso: { 
        this.selectedList = this.modelList.integracoesSucesso;
        break; 
      }
      case FiltroSelecionado.Insucesso: { 
        this.selectedList = this.modelList.integracoesInsucesso;
        this.totalBotaoReenvio = this.selectedList.length;
        this.esconderBotaoReenvioMassivo = false;
        break; 
      } 
      case FiltroSelecionado.NaoDefinido: { 
        this.selectedList = this.modelList.integracoesNaoDefinida;
        this.totalBotaoReenvio = this.selectedList.length;
        this.esconderBotaoReenvioMassivo = false;
        break; 
      } 
      default: { 
        this.selectedList = this.modelList.integracoes;
        break; 
      }
    }

    if(this.currentPage > 0)
      this.paginator.firstPage();
    else
      this.setListFaturaConciliacaoIntegracao();
  }

  reenviarConciliacao(model: FaturaConciliacaoIntegracao){
    var reenvio = new FaturaConciliacaoReenvio(model.faturaConciliacaoId, model.faturaId, model.conciliacaoId);

    this._service.ReenviarFaturaConciliacaoIndividual(reenvio).subscribe(res => {
      this.retornoReenvioFaturaConciliacao = res;
      this.validarRetornoReenvio();
		});
  }

  reenviarConciliacaoLista(){
    var listaReenvio = new Array<FaturaConciliacaoReenvio>();

    this.selectedList.forEach(element => {
      listaReenvio.push(new FaturaConciliacaoReenvio(element.faturaConciliacaoId, element.faturaId, element.conciliacaoId)) 
    });

    this._service.ReenviarFaturaConciliacaoMassivo(listaReenvio).subscribe(res => {
      this.retornoReenvioFaturaConciliacao = res;
      this.validarRetornoReenvio();
		});
  }

  validarRetornoReenvio(){
    if(this.retornoReenvioFaturaConciliacao.sucesso){
      this.modelList.dataUltimoEnvio = this.retornoReenvioFaturaConciliacao.dataReenvio;
      this._alertService.show("Sucesso.", this.retornoReenvioFaturaConciliacao.mensagem, 'success');
    } else{
      this._alertService.show("Erro.", this.retornoReenvioFaturaConciliacao.mensagem, 'error');
    }
  }

	showObject(empresaIntegracaoItemDetalheId: number, tipo: number){
    var obj;
    if(this.jsonIntegracaoFaturaConciliacao == undefined || this.jsonIntegracaoFaturaConciliacao.empresaIntegracaoItemDetalheId != empresaIntegracaoItemDetalheId){
      this._service.GetJsonIntegracaoFaturaConciliacao(empresaIntegracaoItemDetalheId).subscribe(res => {
        this.jsonIntegracaoFaturaConciliacao = res;
        obj = this.montaObjetoJson(tipo);
        this.exibeComponentVisualizacaoJson(obj);
      });
    }
    else {
      obj = this.montaObjetoJson(tipo);
      this.exibeComponentVisualizacaoJson(obj);
    }
	}
  
  applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

  private montaObjetoJson(tipo: number): any {
    var jsonSelecionado;

    if(tipo == 1){
      jsonSelecionado =  this.jsonIntegracaoFaturaConciliacao.jsonEnvio
    }
    else{
      jsonSelecionado = this.jsonIntegracaoFaturaConciliacao.jsonRetorno
    }
    
    if(jsonSelecionado == '')
      jsonSelecionado = "{ \"mensagem\": \"JSON não encontrado\" }";

    let obj = {
      json: jsonSelecionado
    };

    return obj;
  }

  private exibeComponentVisualizacaoJson(obj: any){
		const dialogRef = this.dialog.open(JsonViewComponent, { data:{ obj } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
		});
  }
}