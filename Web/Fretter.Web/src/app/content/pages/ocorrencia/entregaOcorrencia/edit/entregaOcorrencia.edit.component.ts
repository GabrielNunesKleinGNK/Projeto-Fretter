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

import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { EntregaOcorrenciaService } from '../../../../../core/services/entregaOcorrencia.service';
import { AlertService } from '../../../../../core/services/alert.service';
import { EntregaOcorrencia } from '../../../../../core/models/Fusion/entregaOcorrencia.model';

@Component({
	selector: 'm-entregaOcorrencia-edit',
	templateUrl: './entregaOcorrencia.edit.component.html'
})
export class EntregaOcorrenciaEditComponent implements OnInit {
	model: EntregaOcorrencia;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	empresaList: any[];
	transportadorList: any[];
	deParaList: any[];

	constructor(public dialogRef: MatDialogRef<EntregaOcorrenciaEditComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _service: EntregaOcorrenciaService,
		private _alertService: AlertService) { }

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		this.viewLoading = true;
		this.bindList();
		setTimeout(() => { this.viewLoading = false; }, 500);
	}

	bindList() {
		this._service.getDePara().subscribe(data => {
			this.deParaList = data;
		});
	}

	createForm() {
		this.modelForm = new FormGroup({
			'id': new FormControl(this.model.id),
			'entregaId': new FormControl(this.model.entregaId),
			'listaEntregas': new FormControl(this.model.listaEntregas),
			'codigoEntrega': new FormControl(this.model.codigoEntrega),
			'transportadorId': new FormControl(this.model.transportadorId),
			'ocorrenciaId': new FormControl(this.model.ocorrenciaId, [Validators.required]),
			'ocorrencia': new FormControl(this.model.ocorrencia),
			'dataOcorrencia': new FormControl(this.model.dataOcorrencia, [Validators.required]),
			'complementar': new FormControl(this.model.complementar),
			'sigla': new FormControl(this.model.sigla),
			'finalizar': new FormControl(this.model.finalizar),
			'dominio': new FormControl(this.model.dominio),
			'cidade': new FormControl(this.model.cidade),
			'uf': new FormControl(this.model.uf),
			'latitude': new FormControl(this.model.latitude),
			'longitude': new FormControl(this.model.longitude),
		});
	}

	get id() { return this.modelForm.get('id'); }
	get entregaId() { return this.modelForm.get('entregaId'); }
	get listaEntregas() { return this.modelForm.get('listaEntregas'); }
	get ocorrenciaId() { return this.modelForm.get('ocorrenciaId'); }
	get ocorrencia() { return this.modelForm.get('ocorrencia'); }
	get transportadorId() { return this.modelForm.get('transportadorId'); }
	get dataOcorrencia() { return this.modelForm.get('dataOcorrencia'); }
	get complementar() { return this.modelForm.get('complementar'); }
	get sigla() { return this.modelForm.get('sigla'); }
	get finalizar() { return this.modelForm.get('finalizar'); }
	get dominio() { return this.modelForm.get('dominio'); }
	get cidade() { return this.modelForm.get('cidade'); }
	get uf() { return this.modelForm.get('uf'); }
	get latitude() { return this.modelForm.get('latitude'); }
	get longitude() { return this.modelForm.get('longitude'); }

	getTitle(): string {
		return 'Adicionar nova ocorrência na entrega: ' + this.model.entregaId;
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.modelForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	toModel(): EntregaOcorrencia {
		const controls = this.modelForm.controls;
		const _model = new EntregaOcorrencia();
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

	save(_model: EntregaOcorrencia) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		var listaModel = new Array<EntregaOcorrencia>();

		if (_model.listaEntregas.length > 0) {

			_model.listaEntregas.forEach(e => {
				listaModel.push(
					{
						id: -1,
						entregaId: e,
						codigoEntrega: null,
						ocorrenciaId: _model.ocorrenciaId,
						ocorrencia: _model.ocorrencia,
						dataOcorrencia: _model.dataOcorrencia,
						transportadorId: null,
						complementar: _model.complementar,
						sigla: _model.sigla,
						finalizar: _model.finalizar,
						dominio: _model.dominio,
						cidade: _model.cidade,
						uf: _model.uf,
						latitude: _model.latitude,
						longitude: _model.longitude,
						dataAlteracao: null,
						dataCadastro: null,
						ativo: true,
						usuarioCadastro: null,
						usuarioAlteracao: null,
						listaEntregas: []
					}
				);
			});
		}
		else
			listaModel.push(_model);


		this._service.inserir(listaModel).subscribe(res => {
			if (res.length > 0) {
				this._alertService.show("Atenção.",
					"Algumas entregas não tiveram as ocorrências inseridas. Vide o .csv que foi baixado em sua máquina.",
					'warning');
				this.viewLoading = false;
				this.downloadArquivoDeAlerta(res);
			}
			else {
				this._alertService.show("Sucesso.", "Ocorrência cadastrada com sucesso.", 'success');
				this.viewLoading = false;
			}
			this.dialogRef.close({ _model, isEdit: true });
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	atualizaOcorrencia(event: any) {
		this.model.sigla = event.sigla;
		this.model.finalizar = event.finalizadora;
		this.model.ocorrencia = event.descricao;
	}

	downloadArquivoDeAlerta(res: any) {
		var informativo = '';
		res.forEach((i) => {
			informativo += i.entregaId + ' - ' + i.retornoValidacao + ';'
		})

		const file = new Blob([informativo]);
		const a = document.createElement("a");
		const url = URL.createObjectURL(file);

		a.href = url;
		a.download = "Ocorrências não inseridas.csv";
		document.body.appendChild(a);
		a.click();

		setTimeout(function () {
			document.body.removeChild(a);
			window.URL.revokeObjectURL(url);
		}, 0)
	}
}

