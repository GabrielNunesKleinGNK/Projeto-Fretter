import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertService } from '../../../../../core/services/alert.service';
import { EntregaAberta } from '../../../../../core/models/Fusion/entregaAberta.model';
import { EntregaOcorrenciaEditComponent } from '../edit/entregaOcorrencia.edit.component';
import { EntregaOcorrenciaService } from '../../../../../core/services/entregaOcorrencia.service';
import { EntregaOcorrencia } from '../../../../../core/models/Fusion/entregaOcorrencia.model';
import { TransportadorService } from '../../../../../core/services/transportador.service';
import { EntregaEmAbertoFiltro } from '../../../../../core/models/Filters/entregaEmAbertoFiltro.model';
import { OcorrenciaEmpresaListComponent } from './listadepara/ocorrenciaEmpresa.list.component';
import { DownloadArquivoOcorrenciaEditComponent } from './download/downloadOcorrenciaArquivo.edit.component';
import { OcorrenciaEntregaListComponent } from './ocorrenciasEntrega/ocorrenciaEntrega.list.component';
import { OcorrenciaArquivoListComponent } from './ocorrenciaArquivo/ocorrenciaArquivo.list.component';

@Component({
	selector: 'm-entregaocorrencia',
	styleUrls: ['./entregaOcorrencia.list.component.scss'],
	templateUrl: './entregaOcorrencia.list.component.html'
})
export class EntregaOcorrenciaListComponent implements OnInit {
	dataSource: MatTableDataSource<EntregaAberta>;//= new MatTableDataSource(Array<EntregaAberta>());
	displayedColumns: string[] = ['id', /*'codigoEntrega',*/ 'canal', 'transportador', 'notaserie',
		'diasSemAlteracao', 'dataUltimaOcorrencia', 'selecionar', 'actions'];
	viewLoading: boolean = false;
	desabilitaMassivoTela: boolean = true;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	transportadorList: any[];
	deParaList: any[];
	marketplace: Array<any> = [
		{ opcao: true, texto: "Sim" },
		{ opcao: false, texto: "Não" }]
	lstReprocessamentoMassivo = new Array<any>();
	filtro: EntregaEmAbertoFiltro = new EntregaEmAbertoFiltro();
	resultsLength: number;
	start: number = 0;
	size: number = 20;


	constructor(
		private _service: EntregaOcorrenciaService,
		private _transportadorService: TransportadorService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService) { }

	ngOnInit() {
		this.bindList();
		this.cdr.detectChanges();
	}

	load() {
		this.viewLoading = true;
		this.pesquisar();
		this.cdr.detectChanges();
	}

	bindList() {
		this._transportadorService.getTransportadoresPorEmpresa().subscribe(data => {
			this.transportadorList = data;
		});

		this._service.getDePara().subscribe(data => {
			this.deParaList = data;
		});
	}

	novo() {
		var importacaoConfiguracao = new EntregaAberta();
		const dialogRef = this.dialog.open(EntregaOcorrenciaEditComponent, { data: { model: importacaoConfiguracao } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	adicionarOcorrencia(_model: EntregaAberta) {
		if (this.lstReprocessamentoMassivo.length > 0) {
			this._alertService.show("Atenção", "Envio por entrega só é realizado quando não há entregas selecionadas pelo flag.", "warning");
			return;
		}

		var model = new EntregaOcorrencia();
		model.entregaId = _model.id;
		model.codigoEntrega = _model.codigoEntrega;
		model.transportadorId = _model.transportadorId;
		model.listaEntregas = new Array<number>();

		const dialogRef = this.dialog.open(EntregaOcorrenciaEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	adicionarOcorrenciaMassivo() {
		if (this.lstReprocessamentoMassivo.length <= 0) {
			this._alertService.show("Atenção", "Nenhuma entrega selecionada para envio massivo", "warning");
			return;
		}

		var model = new EntregaOcorrencia();
		model.listaEntregas = this.lstReprocessamentoMassivo.map(x => x.id);

		const dialogRef = this.dialog.open(EntregaOcorrenciaEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				this.desabilitaMassivoTela = false;
				return;
			}
			this.load();
			this.lstReprocessamentoMassivo = new Array<any>();
		});
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	abrirListaOcorrencia() {
		const dialogRef = this.dialog.open(OcorrenciaEmpresaListComponent, { data: { model: this.deParaList } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
		});
	}

	buscarOcorrencias(model: any) {
		let entregaId = model.id;
		const dialogRef = this.dialog.open(OcorrenciaEntregaListComponent, { data: { entregaId: model.id }, minWidth: '100vh' });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	verUltimaOcorrencia(model: any) {
		if (model.ultimaOcorrencia !== null) {
			let texto = "Ultima ocorrência foi inserida em " + (model.dataUltimaOcorrencia) + ". Ocorrência: " + model.ultimaOcorrencia;
			this._alertService.show("Ultima ocorrência rastreada", texto, "success");
		}
		else {
			let texto = "Não existe ocorrência roteirizada para essa entrega ainda.";
			this._alertService.show("Ultima ocorrência rastreada", texto, "warning");
		}

	}

	insereRegistrosReprocessamentoMassivo(event: any) {
		if (event.target.checked) {
			this.lstReprocessamentoMassivo.push({ id: event.target.id, pedido: event.target.className });
			this.desabilitaMassivoTela = !event.target.checked;
		}
		else {
			this.lstReprocessamentoMassivo = this.lstReprocessamentoMassivo.filter( x => x.id !== event.target.id);

			if (this.lstReprocessamentoMassivo.length <= 0) {
				this.desabilitaMassivoTela = !event.target.checked;
			}
		}
	}

	selecionarTodos(event: any) {
		var listaChecks = document.getElementsByName('check');

		var alerta = "Apenas as entregas que estão em tela podem ser selecionadas massivamente nessa opção." +
			" Para envio de maior massa, faça o envio via arquivo. Deseja continuar?";

		if (event.target.checked) {
			this._alertService.confirmationMessage("Atenção", alerta, "Sim", "Não").then(confirm => {
				if (confirm.value) {
					this.desabilitaMassivoTela = !event.target.checked;

					for (var indice = 0; indice < listaChecks.length; indice++) {
						var filtrada = this.lstReprocessamentoMassivo.
							filter(x => x.id === listaChecks[indice].id && x.pedido === listaChecks[indice].className);

						if (filtrada !== undefined && filtrada !== null && filtrada.length === 0)
							listaChecks[indice].click();
					}
				}
				else
					event.target.checked = false;
			})
		}
		else {
			for (var indice = 0; indice < listaChecks.length; indice++) {
				// var filtrada = this.lstReprocessamentoMassivo.
				// 	filter(x => x.id === listaChecks[indice].id && x.pedido === listaChecks[indice].className);

				// if (filtrada === undefined || filtrada === null || filtrada.length === 0)
					listaChecks[indice].click();
			}
		}
	}

	inputChange(fileInputEvent: any) {
		let file = fileInputEvent.target.files[0];
		if (file) {
			const formData = new FormData();
			formData.append('file', file);
			this.viewLoading = true;
			this._service.upload(formData).subscribe(() => {
				this._alertService.show("Sucesso", "Arquivo importado com sucesso!", 'success');
				this.viewLoading = false;
				//this.pesquisar();
			}, (error) => {
				if (error) this._alertService.show("Error", "Erro ao importar arquivo. Erro: " + error.error, 'error');
				else this._alertService.show("Error", "Erro ao importar arquivo arquivo.", 'error');

				this.viewLoading = false;
				//this.pesquisar();
			});
		}
	}

	download() {
		const dialogRef = this.dialog.open(DownloadArquivoOcorrenciaEditComponent);
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	buscarArquivos() {
		//let entregaId = model.id;
		const dialogRef = this.dialog.open(OcorrenciaArquivoListComponent);
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	pageChange(event) {
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}

	pesquisar() {
		this.viewLoading = true;
		this.filtro.pagina = this.start;
		this.filtro.paginaLimite = this.size;
		if (this.filtro.pedidos !== undefined && this.filtro.pedidos !== null)
			this.validaQuantidadePedidos();

		if ((this.filtro.dataInicio === undefined || this.filtro.dataInicio === null) &&
			(this.filtro.dataFim === undefined || this.filtro.dataFim === null)) {
			this.viewLoading = false;
			return this._alertService.show("Atenção", "Data de inicio e fim é obrigatório", "warning")
		}

		this._service.obterEntragasEmAberto(this.filtro).subscribe(data => {
			this.dataSource = data;
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
			this.viewLoading = false;

			if (data.length > 0) {
				this.resultsLength = data[0].total;
			}
		}, error => { this.viewLoading = false });
	}

	limpar() {
		this.filtro = new EntregaEmAbertoFiltro();
		this.dataSource = null;
		//		this.pesquisar();
	}

	validaQuantidadePedidos() {
		var list = this.filtro.pedidos.split(';');
		if (list !== undefined && list.length > 50) {
			this._alertService.show('Atenção', 'Não é permitido mais de 50 pedidos na busca', 'warning');
			return;
		}
	}
}
