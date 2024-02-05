import {
	Component,
	OnInit,
	Inject,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy
} from '@angular/core';

import {
	MatPaginator,
	MatSort,
	MatSnackBar,
	MatDialog,
	MatTableDataSource,
	MatDialogRef,
	MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

import { FaturaService } from "../../../../../core/services/fatura.service";
import { AlertService } from '../../../../../core/services/alert.service';
import { Fatura } from '../../../../../core/models/fatura';
import { FaturaStatusService } from '../../../../../core/services/faturaStatus.service';
import { FaturaHistoricoService } from '../../../../../core/services/faturaHistorico.service';
import { FaturaStatusAcaoService } from '../../../../../core/services/faturaStatusAcao.service';
import { FaturaStatusAcao } from '../../../../../core/models/faturaStatusAcao.model';

@Component({
	selector: 'm-fatura-edit',
	templateUrl: './fatura.edit.component.html',
	styleUrls: ['./fatura.edit.component.scss']
})
export class FaturaEditComponent implements OnInit {
	model: Fatura;
	disabled: boolean = false;
	lstStatus: any[];
	lstHistorico: any[];
	lstStatusAcao: Array<FaturaStatusAcao> = new Array<FaturaStatusAcao>();
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	acaoSelecionada: number;
	motivo: string;

	constructor(public dialogRef: MatDialogRef<FaturaEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: FaturaService,
		private _serviceStatusAcao: FaturaStatusAcaoService,
		private _alertService: AlertService,
		private faturaStatusService: FaturaStatusService,
		private faturaHistoricoService: FaturaHistoricoService
	) {
	}

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;

		this.faturaStatusService.get().subscribe(data => {
			this.lstStatus = data;
		});

		this._serviceStatusAcao.get().subscribe(data => {
			var listAcoes = [];
			var listStatusAcoes = data.filter(x => x.faturaStatusId == this.model.faturaStatusId && x.visivel == true)

			listStatusAcoes.forEach((statusAcao) => {
				listAcoes.push(
					{
						id: statusAcao.faturaAcao.id,
						nome: statusAcao.faturaAcao.descricao,
						informaMotivo: statusAcao.informaMotivo
					});
			});

			this.lstStatusAcao = listAcoes;
		});

		this.viewLoading = false;
	}

	createForm() {
		this.modelForm = new FormGroup({
			'empresaId': new FormControl(this.model.empresaId),
			'transportadorId': new FormControl(this.model.transportadorId),
			'faturaPeriodoId': new FormControl(this.model.faturaPeriodoId),
			'valorCustoFrete': new FormControl(this.model.valorCustoFrete),
			'valorCustoAdicional': new FormControl(this.model.valorCustoAdicional),
			'valorCustoReal': new FormControl(this.model.valorCustoReal),
			'valorDocumento': new FormControl(this.model.valorDocumento),
			'quantidadeDevolvidoRemetente': new FormControl(this.model.quantidadeDevolvidoRemetente),
			'faturaStatusId': new FormControl(this.model.faturaStatusId),
			'dataVencimento': new FormControl(this.model.dataVencimento),
			'dataCadastro': new FormControl(this.model.dataCadastro),
			'quantidadeEntrega': new FormControl(this.model.quantidadeEntrega),
			'quantidadeSucesso': new FormControl(this.model.quantidadeSucesso),
			'quantidadeDivergencia': new FormControl(this.model.quantidadeEntrega - this.model.quantidadeSucesso),
			'acaoSelecionada': new FormControl(this.acaoSelecionada),
			'motivo': new FormControl(this.motivo),
		});
	}
	get empresaId() { return this.modelForm.get('empresaId'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get faturaPeriodoId() { return this.modelForm.get('faturaPeriodoId'); }
	get valorCustoFrete() { return this.modelForm.get('valorCustoFrete'); }
	get valorCustoAdicional() { return this.modelForm.get('valorCustoAdicional'); }
	get valorCustoReal() { return this.modelForm.get('valorCustoReal'); }
	get quantidadeDevolvidoRemetente() { return this.modelForm.get('quantidadeDevolvidoRemetente'); }
	get faturaStatusId() { return this.modelForm.get('faturaStatusId'); }
	get dataVencimento() { return this.modelForm.get('dataVencimento'); }
	get quantidadeEntrega() { return this.modelForm.get('quantidadeEntrega'); }
	get quantidadeSucesso() { return this.modelForm.get('quantidadeSucesso'); }
	get quantidadeDivergencia() { return this.modelForm.get('quantidadeDivergencia'); }

	getTitle(): string {
		if (this.model.visualizar == true)
			return 'Dados de Fatura';
		else
			return 'Atualização de Status/ Histórico da Fatura';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	toModel(): Fatura {
		const controls = this.modelForm.controls;
		const _model = new Fatura();
		_model.id = this.model.id;
		_model.empresaId = this.model.empresaId;
		_model.transportadorId = this.model.transportadorId;
		_model.faturaPeriodoId = this.model.faturaPeriodoId;
		_model.valorCustoFrete = this.model.valorCustoFrete;
		_model.valorCustoAdicional = this.model.valorCustoAdicional;
		_model.valorCustoReal = this.model.valorCustoReal;
		_model.quantidadeDevolvidoRemetente = this.model.quantidadeDevolvidoRemetente;
		_model.faturaStatusId = this.model.faturaStatusId;
		_model.dataVencimento = this.model.dataVencimento;
		_model.quantidadeEntrega = this.model.quantidadeEntrega;
		_model.quantidadeSucesso = this.model.quantidadeSucesso;
		_model.quantidadeDivergencia = this.model.quantidadeDivergencia;

		return _model;
	}

	onSubmit() {
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;
		const controls = this.modelForm.controls;

		if (this.modelForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		}

		const model = this.toModel();
		this.save(model);
	}

	save(_model: Fatura) {

		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		if (this.validaMotivo() && !this.motivo) {
			this._alertService.show("Atenção.", "A ação selecionada necessita de um motivo.", 'warning');
			this.viewLoading = false;
		}
		else
			this._service.Acao({ faturaId: this.model.id, acaoId: this.acaoSelecionada, motivo: this.motivo })
				.subscribe(() => {
					this._alertService.show("Sucesso.", "Ação realizada com sucesso.", 'success');
					this.viewLoading = false;
					this.dialogRef.close({ _model, isEdit: true });
				});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	validaMotivo() {
		var result = this.lstStatusAcao.filter(x => x.id == this.acaoSelecionada)[0];
		if (result && result.informaMotivo)
			return true;
		else
			return false;
	}
}