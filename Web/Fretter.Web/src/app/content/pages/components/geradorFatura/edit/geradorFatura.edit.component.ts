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

import { ImportacaoFatura } from "../../../../../core/models/importacaoFatura";
import { GeradorFaturaService } from "../../../../../core/services/geradorFatura.service";
import { AlertService } from '../../../../../core/services/alert.service';
@Component({
	selector: 'm-gerador-fatura-edit',
	templateUrl: './geradorFatura.edit.component.html',
	styleUrls: ['./geradorFatura.edit.component.scss']
})
export class GeradorFaturaEditComponent implements OnInit {
	model: ImportacaoFatura;
	disabled: boolean = false;
	arquivos: any[];
	entregas: any[];
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;

	constructor(public dialogRef: MatDialogRef<GeradorFaturaEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: GeradorFaturaService,
		private _alertService: AlertService,
	) {

	}

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		setTimeout(() => { this.viewLoading = false; }, 500);
	}

	createForm() {
		this.modelForm = new FormGroup({
			'arquivo': new FormControl(this.model.arquivo, [Validators.required]),
		});
	}
	get arquivo() { return this.modelForm.get('arquivo'); }

	getTitle(): string {
		return 'Arquivo DOCCOB';
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
		this._service.getEntregaPorDoccob(_model).subscribe(res => {
			this.entregas = res as any[];


			if (this.entregas.length > 0) {
				if(this.entregas[0].criticas.length > 0){
					this._alertService.show("Erro.", "Erro ao ler arquivo. A seguir, mais detalhes sobre os erros.", 'error');
					this._service.onCarregarCriticas(this.entregas[0].criticas);
					this._service.onExibirCriticas(true);
					this.entregas = [];
				} else {
					this._alertService.show("Sucesso.", "Arquivo processado com sucesso.", 'success');
					this._service.onExibirCriticas(false);

				}
			} else {
				this._alertService.show("Erro.", "Não foi possível ler o Arquivo importado. Formatos permitidos: DOCCOB 5.0 e 3.0", 'error');
				this._service.onExibirCriticas(false);
			}

			this._service.onCarregar(this.entregas);
			this._service.onCarregarTipo(2);
			this._service.onCarregarLoad(false);
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