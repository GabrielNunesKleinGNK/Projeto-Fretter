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

import { EntregaDevolucao } from "../../../../../core/models/Fusion/entregaDevolucao.model";
import { EntregaDevolucaoService } from "../../../../../core/services/entregaDevolucao.service";
import { EntregaDevolucaoStatusAcao } from "../../../../../core/models/Fusion/entregaDevolucaoStatusAcao.model";
import { EntregaDevolucaoStatusAcaoService } from "../../../../../core/services/entregaDevolucaoStatusAcao.service";
import { AlertService } from '../../../../../core/services/alert.service';

@Component({
	selector: 'm-entregaDevolucao-edit',
	templateUrl: './entregaDevolucao.edit.component.html',
	styleUrls: ['./entregaDevolucao.edit.component.scss']
})
export class EntregaDevolucaoEditComponent implements OnInit {
	model: EntregaDevolucao;
	modelForm: FormGroup;
	lstStatusAcao: EntregaDevolucaoStatusAcao[] = new Array<EntregaDevolucaoStatusAcao>();
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	acaoSelecionada: number;
	motivo: string;

	constructor(public dialogRef: MatDialogRef<EntregaDevolucaoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: EntregaDevolucaoService,
		private _serviceStatusAcao: EntregaDevolucaoStatusAcaoService,
		private _alertService: AlertService) { }

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;
		this.bindList();
		setTimeout(() => { this.viewLoading = false; }, 500);

		this._serviceStatusAcao.get().subscribe(data => {
			var listAcoes = [];
			var listStatusAcoes = data.filter(x => x.entregaDevolucaoStatusId == this.model.entregaDevolucaoStatus
				&& x.entregaTransporteTipoId == this.model.entregaTransporteTipoId && x.visivel == true)

			listStatusAcoes.forEach((statusAcao) => {
				listAcoes.push({ id: statusAcao.acao.id, nome: statusAcao.acao.nome, informaMotivo: statusAcao.informaMotivo });
			});

			this.lstStatusAcao = listAcoes;
		});

	}

	bindList() {
	}

	createForm() {
		this.modelForm = new FormGroup({
			'entregaId': new FormControl(this.model.entregaId),
			'entregaTransporteTipoId': new FormControl(this.model.entregaTransporteTipoId),
			'codigoColeta': new FormControl(this.model.codigoColeta),
			'codigoRastreio': new FormControl(this.model.codigoRastreio),
			'validade': new FormControl(this.model.validade),
			'observacao': new FormControl(this.model.observacao),
			'inclusao': new FormControl(this.model.inclusao),
			'entregaDevolucaoStatus': new FormControl(this.model.entregaDevolucaoStatus),
			'processado': new FormControl(this.model.processado),
			'acaoSelecionada': new FormControl(this.acaoSelecionada),
			'motivo': new FormControl(this.motivo),
		});
	}
	get entrega() { return this.modelForm.get('entrega'); }
	get entregaTransporteTipo() { return this.modelForm.get('entregaTransporteTipo'); }
	get codigoColeta() { return this.modelForm.get('codigoColeta'); }
	get codigoRastreio() { return this.modelForm.get('codigoRastreio'); }
	get validade() { return this.modelForm.get('validade'); }
	get observacao() { return this.modelForm.get('observacao'); }
	get inclusao() { return this.modelForm.get('inclusao'); }
	get entregaDevolucaoStatus() { return this.modelForm.get('entregaDevolucaoStatus'); }
	get processado() { return this.modelForm.get('processado'); }

	getTitle(): string {
		return this.model.id > 0 ? `Edição da Entrega Devolucao: ${this.model.entregaId}` : 'Novo Entrega Devolucao';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	toModel(): EntregaDevolucao {
		const controls = this.modelForm.controls;
		const _model = new EntregaDevolucao();
		_model.id = this.model.id;

		Object.keys(controls).forEach(controlName => {
			_model[controlName] = controls[controlName].value;
		}
		);

		return _model;
	}

	onSubmit() {
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;
		const controls = this.modelForm.controls;

		if (this.modelForm.invalid) {
			Object.keys(controls).forEach(controlName => {
				controls[controlName].markAsTouched();
				if (!controls[controlName].valid)
					console.log(controlName);
			}
			);
			this.hasFormErrors = true;
			return;
		}

		const model = this.toModel();
		this.save(model);
	}

	save(_model: EntregaDevolucao) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		if (this.validaMotivo() && !this.motivo) {
			this._alertService.show("Atenção.", "A ação selecionada necessita de um motivo.", 'warning');
			this.viewLoading = false;
		}
		else
			this._service.Acao({ entregaDevolucaoId: this.model.id, acaoId: this.acaoSelecionada, motivo: this.motivo })
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

