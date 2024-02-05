import {
	Component,
	OnInit,
	Inject,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy
} from '@angular/core';

import {
MatDialogRef,
MAT_DIALOG_DATA,
} from '@angular/material';

import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { ImportacaoConfiguracao } from '../../../../../../core/models/importacaoConfiguracao.model';
import { ImportacaoConfiguracaoService } from '../../../../../../core/services/importacaoConfiguracao.service';
import { AlertService } from '../../../../../../core/services/alert.service';
import { ImportacaoConfiguracaoTipoService } from '../../../../../../core/services/importacaoConfiguracaoTipo.service';
import { EmpresaService } from '../../../../../../core/services/empresa.service';
import { TransportadorService } from '../../../../../../core/services/transportador.service';


@Component({
	selector: 'm-importacaoconfiguracao-edit',
	templateUrl: './importacaoConfiguracao.edit.component.html'
})
export class ImportacaoConfiguracaoEditComponent implements OnInit {
	model: ImportacaoConfiguracao;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	importacaoConfiguracaoTipoList: any[];
	empresaList: any[];
	transportadorList: any[];
	importacaoArquivoTipoList: any[] = [{ id: 1, nome: "CTe" }, { id: 2, nome: "CONEMB" }];
	constructor(public dialogRef: MatDialogRef<ImportacaoConfiguracaoEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: ImportacaoConfiguracaoService,
		private _tipoService: ImportacaoConfiguracaoTipoService,
		private _empresaService: EmpresaService,
		private _transportadorService: TransportadorService,
		private _alertService: AlertService) { }

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;
		this.bindList();
		setTimeout(() => { this.viewLoading = false; }, 500);
	}

	bindList() {
		this._tipoService.get().subscribe(data => {
			this.importacaoConfiguracaoTipoList = data;
		});

		this._transportadorService.getTransportadoresPorEmpresa().subscribe(data => {
			this.transportadorList = data;
		});
	}

	createForm() {
		this.modelForm = new FormGroup({
			'importacaoConfiguracaoTipoId': new FormControl(this.model.importacaoConfiguracaoTipoId),
			'empresaId': new FormControl(this.model.empresaId),
			'transportadorId': new FormControl(this.model.transportadorId),
			'importacaoArquivoTipoId': new FormControl(this.model.importacaoArquivoTipoId),
			'diretorio': new FormControl(this.model.diretorio),
			'usuario': new FormControl(this.model.usuario),
			'senha': new FormControl(this.model.senha),
			'outro': new FormControl(this.model.outro),
			'ultimaExecucao': new FormControl(this.model.ultimaExecucao),
			'ultimoRetorno': new FormControl(this.model.ultimoRetorno),
			'sucesso': new FormControl(this.model.sucesso),
			'compactado': new FormControl(this.model.compactado),
			'diretorioSucesso': new FormControl(this.model.diretorioSucesso),
			'diretorioErro': new FormControl(this.model.diretorioErro),
		});

	}

	get importacaoConfiguracaoTipoId() { return this.modelForm.get('importacaoConfiguracaoTipoId'); }
	get empresaId() { return this.modelForm.get('empresaId'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get importacaoArquivoTipoId() { return this.modelForm.get('importacaoArquivoTipoId'); }
	get diretorio() { return this.modelForm.get('diretorio'); }
	get usuario() { return this.modelForm.get('usuario'); }
	get senha() { return this.modelForm.get('senha'); }
	get outro() { return this.modelForm.get('outro'); }
	get ultimaExecucao() { return this.modelForm.get('ultimaExecucao'); }
	get ultimoRetorno() { return this.modelForm.get('ultimoRetorno'); }
	get sucesso() { return this.modelForm.get('sucesso'); }
	get compactado() { return this.modelForm.get('compactado'); }
	get diretorioSucesso() { return this.modelForm.get('diretorioSucesso'); }
	get diretorioErro() { return this.modelForm.get('diretorioErro'); }

	getTitle(): string {
		return this.model.id > 0 ? `Edição de Configuração de FTP: ${this.model.id}` : 'Nova Configuração de FTP';
	}


	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}
	toModel(): ImportacaoConfiguracao {
		const controls = this.modelForm.controls;
		const _model = new ImportacaoConfiguracao();
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

	save(_model: ImportacaoConfiguracao) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		this._service.save(_model).subscribe(res => {
			this._alertService.show("Sucesso.", "ImportacaoConfiguracao cadastrado / Alterado com sucesso.", 'success');
			this.viewLoading = false;
			this.dialogRef.close({ _model, isEdit: true });
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

