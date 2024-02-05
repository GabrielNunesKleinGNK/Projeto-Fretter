import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef
} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as fs from 'file-saver';
import { Subscription } from 'rxjs';
import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ImportacaoFatura } from '../../../../../core/models/importacaoFatura';
import { GeradorFaturaService } from "../../../../../core/services/geradorFatura.service";
import { AlertService } from '../../../../../core/services/alert.service';
import { ImportacaoArquivo } from '../../../../../core/models/importacaoArquivo';
import moment from 'moment';
import { ExcelService } from '../../../../../core/services/excel.service';

@Component({
	selector: 'm-gerador-fatura-list',
	templateUrl: './geradorFatura.list.component.html',
	styleUrls: ['./geradorFatura.list.component.scss'],
})
export class GeradorFaturaListComponent implements OnInit {
	isLinear = false;
	fmConciliacao: FormGroup;
	fmResumo: FormGroup;
	dataSource: MatTableDataSource<ImportacaoFatura>;
	displayedColumns: string[] = ['notaFiscal', 'transportador','conciliacaoTipo', 'codigoPedido', 'valorCustoFrete', 'valorCustoReal', 'valorFreteDoccob', 'statusConciliacao', 'dataEmissao', 'dataCadastro', 'actions'];
	viewLoading: boolean = false;
	viewLoadingFatura: boolean = false;
	viewProcess: boolean = false;
	viewLoadingExcel: boolean = false;
	headerFilterSelected: number = 0;
	visaoSelecionada: string = "Total";
	tipoProcesso: number = 1; //1 Pesquisa 2 Doccob
	entregas: ImportacaoFatura[];
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	subEntregas: Subscription;
	subLoad: Subscription;
	subTipo: Subscription;

	constructor(
		private _service: GeradorFaturaService,
		public dialog: MatDialog,
		private cdr: ChangeDetectorRef,
		private _alertService: AlertService,
		private _formBuilder: FormBuilder, 
		private _excelService: ExcelService
	) {	}

	ngOnInit() {
		this.fmConciliacao = this._formBuilder.group({
		});
		this.fmResumo = this._formBuilder.group({
			secondCtrl: ['', Validators.required]
		});
		this.subEntregas = this._service.onAtualizar.subscribe(data => {
			this.entregas = data;
			this.load(data);
		});
		this.subLoad = this._service.onLoad.subscribe(data => {
			if (data == true)
				this.dataSource = new MatTableDataSource();
			this.viewLoading = data;
			this.cdr.detectChanges();
		});
		this.subTipo = this._service.onTipo.subscribe(data => {
			this.tipoProcesso = data;
		});
	}


	load(data: any[]) {
		this.dataSource = new MatTableDataSource(data);
		this.dataSource.sort = this.sort;
		this.dataSource.paginator = this.paginator;
		this.cdr.detectChanges();
	}

	getTotalSelecionado() {
		if (this.entregas != null)
			return this.entregas.filter(function (item) {
				return item.selecionado;
			}).length;
		return 0;
	}

	getTotalValida() {
		if (this.entregas != null)
			return this.entregas.filter(function (item) {
				return item.statusConciliacao == "Valida";
			}).length;
		return 0;
	}

	getTotalDivergente() {
		if (this.entregas != null)
			return this.entregas.filter(function (item) {
				return item.statusConciliacao == "Divergente" || item.statusConciliacao == "Pendente";
			}).length;
		return 0;
	}

	filtraTotal() {
		this.headerFilterSelected = 1;
		if (this.entregas != null) {
			this.visaoSelecionada = "Total";
			this.load(this.entregas);
		}
	}

	filtraNaoEncontrada() {
		this.headerFilterSelected = 2;
		if (this.entregas != null) {
			this.visaoSelecionada = "Não Encontradas";
			var data = this.entregas.filter(function (item) {
				return item.conciliacaoId == 0;
			});
			if (data.length > 0)
				this.load(data);
			else this._alertService.show("Erro.", "Não existe registros a serem exibidos", 'error');
		}
	}

	filtraDivergente() {
		this.headerFilterSelected = 3;
		if (this.entregas != null) {
			this.visaoSelecionada = "Divergentes";
			var data = this.entregas.filter(function (item) {
				return item.statusConciliacao == "Divergente" || item.statusConciliacao == "Pendente";
			});
			if (data.length > 0)
				this.load(data);
			else this._alertService.show("Erro.", "Não existe registros a serem exibidos", 'error');
		}
	}

	filtraValida() {
		this.headerFilterSelected = 4;
		if (this.entregas != null) {
			this.visaoSelecionada = "Validas";
			var data = this.entregas.filter(function (item) {
				return item.statusConciliacao == "Valida";
			});
			if (data.length > 0)
				this.load(data);
			else this._alertService.show("Erro.", "Não existe registros a serem exibidos", 'error');
		}
	}

	getTotalNaoEncontrada() {
		if (this.entregas != null)
			return this.entregas.filter(function (item) {
				return item.conciliacaoId == 0;
			}).length;
		return 0;
	}

	getTotal() {
		if (this.entregas != null)
			return this.entregas.length;;
		return 0;
	}

	getValorTotal() {
		let valorTotal = 0;
		let tipoProcessoIn = 1;

		if (this.tipoProcesso)
			tipoProcessoIn = this.tipoProcesso;

		if (this.entregas != null) {
			this.entregas.filter(function (item) {
				return valorTotal += (tipoProcessoIn == 2 ? item.valorFreteDoccob : item.valorCustoReal);
			});
			return valorTotal;
		}
		return 0;
	}

	getSomaValorSelecionado() {
		let valorTotal = 0;
		let tipoProcessoIn = 1;

		if (this.tipoProcesso)
			tipoProcessoIn = this.tipoProcesso;

		if (this.entregas != null) {
			this.entregas.filter(function (item) {
				return valorTotal += item.selecionado ? (tipoProcessoIn == 2 ? item.valorFreteDoccob : item.valorCustoReal) : 0;
			});
			return valorTotal;
		}
		return 0;
	}

	getSomaValorFreteCusto() {
		let valorTotal = 0;
		if (this.entregas != null) {
			this.entregas.filter(function (item) {
				return valorTotal += item.valorCustoFrete;
			});
			return valorTotal;
		}
		return 0;
	}

	getSomaValorFreteReal() {
		let valorTotal = 0;
		if (this.entregas != null) {
			this.entregas.filter(function (item) {
				return valorTotal += item.valorCustoReal;
			});
			return valorTotal;
		}
		return 0;
	}

	exibirTotalizadores() {
		if (this.entregas != null)
			return this.entregas.length > 0 ? 'block' : 'none';
		return 'none';
	}

	selecionarTodos(values: any): void {
		if (values.currentTarget.checked && this.headerFilterSelected == 1) {
			this.entregas = this.entregas.map(item => {
				item.selecionado = item.habilitado && true;
				return item
			});
		}
		else if (values.currentTarget.checked && this.headerFilterSelected == 3) {
			this.entregas = this.entregas.map(item => {
				item.selecionado = item.habilitado && (item.statusConciliacao == "Divergente" || item.statusConciliacao == "Pendente") && true;
				return item
			});
		}
		else if (values.currentTarget.checked && this.headerFilterSelected == 4) {
			this.entregas = this.entregas.map(item => {
				item.selecionado = item.habilitado && (item.statusConciliacao == "Valida") && true;
				return item
			});
		}
		else {
			this.entregas = this.entregas.map(item => {
				item.selecionado = false;
				return item
			});
		}
	}
	desativarGerarFatura() {
		if (this.entregas != null)
			return this.entregas.filter(function (item) {
				return item.selecionado && item.habilitado;
			}).length == 0 || this.viewLoading || this.viewProcess;
		else
			return true;
	}

	desativarSelecionarTodos() {
		if (this.entregas != null)
			return this.entregas.filter(function (item) {
				return item.habilitado;
			}).length == 0;
		else
			return true;
	}

	applyFilter(filterValue: string) {
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	processarFatura() {
		this.viewProcess = true;
		this._service.processarFaturaManual(this.entregas).subscribe((response: any) => {
			let blob = new Blob([response], { type: response.type });
			fs.saveAs(blob, 'Fatura_Demonstrativo.xlsx');
			this._alertService.show("Sucesso.", "Fatura gerada com sucesso.", 'success');
			this.viewProcess = false;
		}, error => {
			this._alertService.show("Erro.", "Ocorreu um erro ao processar a fatura.", 'error');
			this.viewProcess = false;
		});
	}

	processarFaturaAprovacao() {
		this.viewLoadingFatura = true;
		this._service.processarFaturaAprovacao(this.entregas).subscribe((faturaId: number) => {
			this.viewLoadingFatura = false;
			if (faturaId > 0)
				this._alertService.showWithConfiguration({
					title: `Fatura: ${faturaId} criada com sucesso.`,
					text: 'Deseja ir para a tela de Gerenciamento de Faturas ?',
					type: 'warning',
					showCloseButton: true,
					showCancelButton: true,
					confirmButtonText: "Confirmar",
					cancelButtonText: "Cancelar"
				}).then((result) => {
					this.viewLoadingFatura = false;
					if (result.value) {
						window.location.href = "/faturas";
					}
					else window.location.reload();
				});

		}, error => {
			this._alertService.show("Erro.", "Ocorreu um erro ao processar a fatura.", 'error');
			this.viewLoadingFatura = false;
		});
	}
	exportarExcel() {
		this.viewLoadingExcel = true;

		let excelData = this.entregas.map(object => ({ ...object }));
		this._excelService.generateExcel(
			`Relatorio_Notas_GeracaoFaturas_${moment().format('YYYY-MM-DDTHHmm')}`,
			excelData,
			{
				conciliacaoTipo: { titulo: 'Tipo' },
				notaFiscal: { titulo: 'Nota Fiscal' },
				serie: { titulo: 'Serie' },
				transportador: { titulo: 'Transportador' },
				codigoPedido: { titulo: 'Cod. Pedido' },
				valorCustoFrete: { titulo: 'Custo Frete' },
				valorCustoReal: { titulo: 'Custo Real' },
				valorFreteDoccob: { titulo: 'Custo Doccob' },
				statusConciliacao: { titulo: 'Status' },
				chaveCte: { titulo : 'Chave Cte' },
				dataEmissao: { titulo: 'Dt. Emissão', tipo: 'date'},
				dataCadastro: { titulo: 'Dt. Cadastro', tipo: 'date' }
			});
		
		this.viewLoadingExcel = false;
		this.cdr.detectChanges();
	}
}