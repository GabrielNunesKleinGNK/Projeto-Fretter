import {
	Component,
	OnInit,
	Inject,
	AfterContentInit
} from '@angular/core';

import {
	MatDialogRef,
	MAT_DIALOG_DATA,
	MatChipInputEvent,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AlertService } from '../../../../../../../core/services/alert.service';
import { ContratoTransportadorService } from '../../../../../../../core/services/contratoTransportador.service';
import { ImportacaoArquivoTipoService } from '../../../../../../../core/services/importacaoArquivoTipo.service';
import { ImportacaoArquivoTipo } from '../../../../../../../core/models/importacaoArquivoTipo';
import { ImportacaoArquivoTipoItem } from '../../../../../../../core/models/importacaoArquivoTipoItem';
import { ImportacaoArquivoTipoItemService } from '../../../../../../../core/services/importacaoArquivoTipoItem.service';
import { ContratoTransportadorArquivoTipo } from '../../../../../../../core/models/contratoTransportadorArquivoTipo';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import moment from 'moment';

@Component({
	selector: 'm-contratoTransportadorArquivoTipo-edit',
	templateUrl: './contratoTransportadorArquivoTipo.edit.component.html',
	styleUrls: ['./contratoTransportadorArquivoTipo.edit.component.scss']
})
export class ContratoTransportadorArquivoTipoEditComponent implements OnInit, AfterContentInit {
	model: ContratoTransportadorArquivoTipo;
	disabled: boolean = false;
	tipoArquivo: Array<ImportacaoArquivoTipo> = new Array<ImportacaoArquivoTipo>();
	tipoArquivoItem: Array<ImportacaoArquivoTipoItem> = new Array<ImportacaoArquivoTipoItem>();

	readonly separatorKeysCodes = [ENTER, COMMA];
	labels: any[] = [];
	addOnBlur = true;

	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	loadingAfterSubmit: boolean = false;
	viewLoading: boolean = false;

	constructor(public dialogRef: MatDialogRef<ContratoTransportadorArquivoTipoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: ContratoTransportadorService,
		private _serviceArquivoTipo: ImportacaoArquivoTipoService,
		private _serviceArquivoTipoItem: ImportacaoArquivoTipoItemService,
		private _alertService: AlertService) {
	}

	ngOnInit() {
		this.viewLoading = true;
		this.model = this.data.model;

		if (this.model.alias)
			for (let label of this.model.alias.split(";")) {
				this.labels.push({ name: label });
			}

		if (this.model.importacaoArquivoTipoItem.importacaoArquivoTipoId > 0)
			this.obterListaArquivoTipoItem(this.model.importacaoArquivoTipoItem.importacaoArquivoTipoId);

		this._serviceArquivoTipo.get().subscribe(data => {
			this.tipoArquivo = data;
			this.viewLoading = false
		})

		this.createForm();
		setTimeout(() => { this.viewLoading = false; }, 500);
	}

	ngAfterContentInit() {
	}

	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'importacaoArquivoTipoItemId': new FormControl(this.model.importacaoArquivoTipoItemId, [Validators.required]),
			'transportadorId': new FormControl(this.model.transportadorId, [Validators.required]),
			'alias': new FormControl(this.model.alias),
			'aliasList': new FormControl(this.model.aliasList),
			'importacaoArquivoTipoId': new FormControl(this.model.importacaoArquivoTipoItem.importacaoArquivoTipoId),
			'importacaoArquivoTipoItem': new FormControl(this.model.importacaoArquivoTipoItem)
		});
	}
	get id() { return this.modelForm.get('id'); }
	get importacaoArquivoTipoItemId() { return this.modelForm.get('importacaoArquivoTipoItemId'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get alias() { return this.modelForm.get('alias'); }
	get aliasList() { return this.modelForm.get('aliasList'); }
	get importacaoArquivoTipoId() { return this.modelForm.get('importacaoArquivoTipoId'); }
	get importacaoArquivoTipoItem() { return this.modelForm.get('importacaoArquivoTipoItem'); }

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
	toModel(): ContratoTransportadorArquivoTipo {
		const controls = this.modelForm.controls;
		const _model = new ContratoTransportadorArquivoTipo();
		_model.id = controls['id'].value;
		_model.importacaoArquivoTipoItemId = controls['importacaoArquivoTipoItemId'].value;
		_model.transportadorId = controls['transportadorId'].value;
		_model.alias = controls['alias'].value;
		_model.aliasList = controls['aliasList'].value;
		_model.importacaoArquivoTipoItem = controls['importacaoArquivoTipoItem'].value;
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

	save(_model: ContratoTransportadorArquivoTipo) {
		if (_model.id > 0) {
			_model.aliasList = new Array<string>();
			_model.aliasList.push(_model.alias);
		}

		var itens = new Array<ContratoTransportadorArquivoTipo>();
		_model.aliasList.forEach(alias => {
			itens.push(
				{
					alias: alias,
					ativo: true,
					id: _model.id,
					transportadorId: this.model.transportadorId,
					importacaoArquivoTipoItemId: this.model.importacaoArquivoTipoItemId,
					dataAlteracao: null,
					dataCadastro: moment().toDate(),
					usuarioAlteracao: null,
					usuarioCadastro: null,
					empresaId: null,
					aliasList: null,
					importacaoArquivoTipoItem: null,
					tipoArquivo: null,
					tipoCobranca: null
				});
		});

		this.viewLoading = true;
		this.dialogRef.close(itens);
	}

	getTitle(): string {
		if (this.model.id == 0)
			return 'Novo De/Para de Arquivos';
		else
			return 'Editar De/Para de Arquivos';
	}

	obterListaArquivoTipoItem(importacaoArquivoTipoId: number) {
		if (importacaoArquivoTipoId != null) {

			this._serviceArquivoTipoItem.getTipoCobrancaPorTipoArquivo(importacaoArquivoTipoId).subscribe(data => {
				this.tipoArquivoItem = data;
				this.viewLoading = false;
			});
		}
	}

	changeChip() {
		const controls = this.modelForm.controls;
		this.model.aliasList = this.labels.map(x => x.name);
		controls['aliasList'].setValue(this.model.aliasList);
		controls['aliasList'].markAsTouched();
	}

	add(event: MatChipInputEvent): void {
		const value = (event.value || '').trim();
		if (value) {
			this.labels.push({ name: value.toUpperCase() });
		}
		event.input.value = "";
		this.changeChip();
	}

	remove(fruit: any): void {
		const index = this.labels.indexOf(fruit);

		if (index >= 0) {
			this.labels.splice(index, 1);
		}
		this.changeChip();
	}
}