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

import * as fs from 'file-saver';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { EntregaOcorrenciaService } from '../../../../../../core/services/entregaOcorrencia.service';
import { AlertService } from '../../../../../../core/services/alert.service';
import { EntregaEmAbertoFiltro } from '../../../../../../core/models/Filters/entregaEmAbertoFiltro.model';

@Component({
	selector: 'm-downloadArquivoOcorrencia-edit',
	templateUrl: './downloadOcorrenciaArquivo.edit.component.html'
})
export class DownloadArquivoOcorrenciaEditComponent implements OnInit {
	model: EntregaEmAbertoFiltro;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	listaLimites: Array<any> = [
		{ value: 500, texto: '500' },
		{ value: 1000, texto: '1.000' },
		{ value: 5000, texto: '5.000' },
		{ value: 10000, texto: '10.000' },
		{ value: 50000, texto: '50.000' }
	];
	marketplace: Array<any> = [
		{ opcao: true, texto: "Sim" },
		{ opcao: false, texto: "Não" }]

	constructor(public dialogRef: MatDialogRef<DownloadArquivoOcorrenciaEditComponent>,
		//@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: EntregaOcorrenciaService,
		private _alertService: AlertService) { }

	ngOnInit() {
		this.model = new EntregaEmAbertoFiltro;
		this.model.dataInicio = null;
		this.model.dataFim = null;
		this.model.paginaLimite = this.listaLimites[0].value;


		this.createForm();
		this.viewLoading = true;
		setTimeout(() => { this.viewLoading = false; }, 500);
	}


	createForm() {
		this.modelForm = new FormGroup({
			'dataInicio': new FormControl(this.model.dataInicio, [Validators.required]),
			'dataFim': new FormControl(this.model.dataFim, [Validators.required]),
			'paginaLimite': new FormControl(this.model.paginaLimite, [Validators.required]),
			'entregasMarketplace': new FormControl(this.model.entregasMarketplace, [Validators.required]),
			'pedidos': new FormControl(this.model.pedidos),
		});
	}

	get dataInicio() { return this.modelForm.get('dataInicio'); }
	get dataFim() { return this.modelForm.get('dataFim'); }
	get paginaLimite() { return this.modelForm.get('paginaLimite'); }
	get entregasMarketplace() { return this.modelForm.get('entregasMarketplace'); }
	get pedidos() { return this.modelForm.get('pedidos'); }

	getTitle(): string {
		return 'Parametros download';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	download(model: EntregaEmAbertoFiltro) {
		this.viewLoading = true;
		const controls = this.modelForm.controls;

		if (this.modelForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			this.viewLoading = false;
			return;
		}
		this.validaQuantidadePedidos();

		this._service.download(true, model).subscribe((res: any) => {
			let blob = new Blob([res], { type: res.type });
			fs.saveAs(blob, "Layout importacao de ocorrencias");

			this._alertService.show("Sucesso.", "Arquivo baixado com sucesso.", 'success');
			this.viewLoading = false;
		}, error => {
			this.viewLoading = false;
			if(error.status == 409)
				this._alertService.show("Atenção.", "O filtro de 'data inicio x data fim' não deve ser maior que 31 dias.", 'warning');
			else
				this._alertService.show("Atenção.", error.message, 'warning');
		});
	}
	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	atualizarParametros(event: any) {

	}

	validaQuantidadePedidos() {

		if (this.model.pedidos !== undefined && this.model.pedidos !== null){

			var list = this.model.pedidos.split(';');
			if (list !== undefined && list.length > 50) {
				this._alertService.show('Atenção', 'Não é permitido mais de 50 pedidos na busca', 'warning');
				return;
			}
		}
	}
}

