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

import { ImportacaoArquivo } from "../../../../../core/models/importacaoArquivo";
import { ImportacaoArquivoService } from "../../../../../core/services/importacaoArquivo.service";
import { AlertService } from '../../../../../core/services/alert.service';

@Component({
	selector: 'm-importacaoArquivo-edit',
	templateUrl: './importacaoArquivo.edit.component.html',
	styleUrls: ['./importacaoArquivo.edit.component.scss']
})
export class ImportacaoArquivoEditComponent implements OnInit {
	model: ImportacaoArquivo;
	disabled: boolean = false;
	arquivos: any[];
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;

	constructor(public dialogRef: MatDialogRef<ImportacaoArquivoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: ImportacaoArquivoService,
		private _alertService: AlertService,
	) {

	}

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;
		setTimeout(() => { this.viewLoading = false; }, 500);
	}

	createForm() {
		this.modelForm = new FormGroup({
			'arquivo': new FormControl(this.model.arquivo, [Validators.required]),
		});
	}
	get arquivo() { return this.modelForm.get('arquivo'); }

	getTitle(): string {
		return 'Importação de Arquivo';
	}

	onSubmit() {
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;

		if (this.arquivos === undefined || this.arquivos.length === 0) {
			this.validaExistenciaDeArquivos(true);
			return;
		}
		else
			this.validaExistenciaDeArquivos(false)

		const formData = new FormData();
		this.arquivos.forEach((f) => {
			formData.append('files', f)
		});

		this.save(formData);
	}

	save(_model: FormData) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;

		this._service.importarArquivos(_model).subscribe(res => {
			this._alertService.show("Sucesso.", "Arquivo importado com sucesso.", 'success');
			this.viewLoading = false;
			this.dialogRef.close({ _model, isEdit: true });
		}, error => {
			this.viewLoading = false;
		});
	}

	processFile(arquivoInput: any) {
		this.arquivos = [];

		if (arquivoInput === undefined || arquivoInput.length === 0) {
			this.validaExistenciaDeArquivos(true);
			return;
		}
		else
			this.validaExistenciaDeArquivos(false)

		for (let i = 0; i < arquivoInput.length; i++) {
			this.arquivos.push(arquivoInput[i])
		}
	}

	validaExistenciaDeArquivos(apresentaAlerta?: boolean) {
		var element = document.getElementsByName('alert');
		if (Array.isArray(element)) {
			element.forEach(e => {
				if (apresentaAlerta) {
					e.style.display = 'flex';
				}
				else {
					e.style.display = 'none';
				}
			});
		}
	}
}