import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

import { AlertService } from '../../../../../../../core/services/alert.service';
import { EmpresaIntegracaoItemDetalheService } from '../../../../../../../core/services/empresaIntegracaoItemDetalhe.service';
import { EmpresaIntegracaoItemDetalheFiltro } from '../../../../../../../core/models/Filters/empresaIntegracaoItemDetalheFiltro.model';
import { EmpresaIntegracaoItemDetalhe } from '../../../../../../../core/models/Fusion/empresaIntegracaoItemDetalhe.model';
import { EmpresaIntegracaoItemDetalheDto } from '../../../../../../../core/models/Fusion/empresaIntegracaoItemDetalhe.dto';
import { EmpresaIntegracaoItemDetalheEditComponent } from '../edit/itemDetalhe.edit.component';
import { CanalService } from '../../../../../../../core/services/canal.service';
import { Canal } from '../../../../../../../core/models/Fusion/Canal';
import { TransportadorService } from '../../../../../../../core/services/transportador.service';
import { Transportador } from '../../../../../../../core/models/transportador.model';
import moment from 'moment';


@Component({
	selector: 'm-empresaIntegracaoItemDetalhe',
	templateUrl: './itemDetalhe.list.component.html',
	styleUrls: ['./itemDetalhe.list.component.scss'],
})
export class EmpresaIntegracaoItemDetalheListComponent implements OnInit{
	dataSource: MatTableDataSource<EmpresaIntegracaoItemDetalheDto>;
	filtro: EmpresaIntegracaoItemDetalheFiltro = new EmpresaIntegracaoItemDetalheFiltro();
	displayedColumns: string[] = ['id', 'entregaId', 'ocorrenciaId', 'descricao', 'sigla', 'dataEnvio', 'sucesso', 'statusCode', 'reprocessamento', 'actions'];
	viewLoading: boolean = false;
	lstReprocessamentoMassivo = new Array<number>();
    lstCanais = new Array<Canal>();
	lstTransportadores = new Array<Transportador>();
	desativarEnvioMassivo: boolean = true;
	lstStatus: any[] = [
		{ value: true, nome: "Sim" },
		{ value: false, nome: "Não" }	
	];
	resultsLength: number = 0;
	start: number = 0;
	size: number = 20;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: EmpresaIntegracaoItemDetalheService,
		private _serviceCanal: CanalService, 
		private _serviceTransportador: TransportadorService, 
		public dialog: MatDialog, 
		private cdr: ChangeDetectorRef, 
		private _alertService : AlertService){
		
	}
	ngOnInit() {
		this.load();
	}

	load() {
		this.pesquisar();
	}

	reprocessarMassivo() {
		this.viewLoading = true;
		this._service.reprocessarLote(this.lstReprocessamentoMassivo).subscribe(data => {
			if(data){
				this._alertService.show("Sucesso", "Ocorrências enviadas para processamento.", "success");
			}
			else{
				this._alertService.show("Atenção", "Falha ao enviar ocorrências para processamento.", "warning");
			}

			this.viewLoading = false;
			this.lstReprocessamentoMassivo = new Array<number>();
			this.ativaDesativaMassivo();
			this.load();
		}, error =>{
			this.viewLoading = false;
		});
	}

	reprocessar(itemDetalheId : number) {
		var model = new EmpresaIntegracaoItemDetalhe(); 
		this._service.getById(itemDetalheId).subscribe(data => {
			var model = new EmpresaIntegracaoItemDetalhe(); 
			model = data;
			const dialogRef = this.dialog.open(EmpresaIntegracaoItemDetalheEditComponent, { data: { model } });
			dialogRef.afterClosed().subscribe(res => {
				this.load();
			});
		});
	}
	 
	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	insereRegistrosReprocessamentoMassivo(id : number, click : boolean)
	{
		if(click)
			this.lstReprocessamentoMassivo.push(id);
		else
		{
			var indice = this.lstReprocessamentoMassivo.indexOf(id)
			this.lstReprocessamentoMassivo.splice(indice, 1);
		}
		this.ativaDesativaMassivo();
	}

	pageChange(event){
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.load();
	}

	ativaDesativaMassivo(){
		this.desativarEnvioMassivo =  this.lstReprocessamentoMassivo.length <= 1;
	}

	pesquisar(){
		this.viewLoading = true;

		this.filtro.pagina = this.start;
		this.filtro.paginaLimite = this.size;

		this._service.obterDados(this.filtro).subscribe(result => {
			this.dataSource = result;
			if(result.length > 0)
				this.resultsLength = result[0].total;
			
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.viewLoading = false;
			
			this.cdr.detectChanges();
		 }, error =>{
			this.viewLoading = false;
		});

		this._serviceCanal.getCanaisPorEmpresa().subscribe(data => {
			this.lstCanais = data;
		 });

		 this._serviceTransportador.getTransportadoresPorEmpresa().subscribe(data => {
           this.lstTransportadores = data;
		 });
	}

	limpar() {
		this.filtro = new EmpresaIntegracaoItemDetalheFiltro();
		this.pesquisar();
	}
  }