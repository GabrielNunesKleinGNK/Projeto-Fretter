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
import { AlertService } from '../../../../../../core/services/alert.service';
import { ConciliacaoService } from '../../../../../../core/services/conciliacao.service';

@Component({
	selector: 'm-relatorioDetalhado-detalhes',
	templateUrl: './relatorioDetalhado.detalhes.component.html',
	styleUrls: ['./relatorioDetalhado.detalhes.component.scss']
})
export class RelatorioDetalhadoDetalhesComponent implements OnInit {
	model: any;
	modelForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	viewLoadingRecalculoFrete: boolean = false;
	loadingAfterSubmit: boolean = false;
	loadRecalculo: boolean = false;

	constructor(private _service: ConciliacaoService, public dialogRef: MatDialogRef<any>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private _alertService: AlertService,
	) {
	}

	ngOnInit() {
		this.model = this.data.model;
		this.createForm();
		//		this.viewLoading = true;		
		this.viewLoading = false;
		this.viewLoadingRecalculoFrete = false;
		var data = this.model.jsonValoresRecotacao

		const obj = JSON.parse(this.model.jsonValoresRecotacao);
	}

	createForm() {
		this.modelForm = new FormGroup({
			'codigoConciliacao': new FormControl(this.model.codigoConciliacao),
			'transportador': new FormControl(this.model.transportador),
			'codigoEntrega': new FormControl(this.model.codigoEntrega),
			'codigoPedido': new FormControl(this.model.codigoPedido),
			'qtdTentativas': new FormControl(this.model.qtdTentativas),
			'divergenciaPeso': new FormControl(this.model.divergenciaPeso),
			'divergenciaTarifa': new FormControl(this.model.divergenciaTarifa),
			'dataEmissao': new FormControl(this.model.dataEmissao),
			'status': new FormControl(this.model.status),
			'valorCustoFrete': new FormControl(this.model.valorCustoFrete),
			'valorCustoAdicional': new FormControl(this.model.valorCustoAdicional),
			'valorCustoReal': new FormControl(this.model.valorCustoReal),
			'dataCadastro': new FormControl(this.model.dataCadastro),
			'finalizado': new FormControl(this.model.finalizado),
			'processadoIndicador': new FormControl(this.model.processadoIndicador),
			'jsonValoresRecotacao': new FormControl(this.model.jsonValoresRecotacao),
			'jsonValoresCte ': new FormControl(this.model.jsonValoresCte),
			'entregaPeso': new FormControl(this.model.entregaPeso),
			'entregaAltura': new FormControl(this.model.entregaAltura),
			'entregaLargura': new FormControl(this.model.entregaLargura),
			'entregaComprimento': new FormControl(this.model.entregaComprimento),
		});
	}
	get codigoConciliacao() { return this.modelForm.get('codigoConciliacao'); }
	get transportador() { return this.modelForm.get('transportador'); }
	get codigoEntrega() { return this.modelForm.get('codigoEntrega'); }
	get codigoPedido() { return this.modelForm.get('codigoPedido'); }
	get qtdTentativas() { return this.modelForm.get('qtdTentativas'); }
	get divergenciaPeso() { return this.modelForm.get('divergenciaPeso'); }
	get divergenciaTarifa() { return this.modelForm.get('divergenciaTarifa'); }
	get dataEmissao() { return this.modelForm.get('dataEmissao'); }
	get status() { return this.modelForm.get('status'); }
	get valorCustoFrete() { return this.modelForm.get('valorCustoFrete'); }
	get valorCustoReal() { return this.modelForm.get('valorCustoReal'); }
	get dataCadastro() { return this.modelForm.get('dataCadastro'); }
	get finalizado() { return this.modelForm.get('finalizado'); }
	get processadoIndicador() { return this.modelForm.get('processadoIndicador'); }
	get valoresRecotacao() { return this.modelForm.get('valoresRecotacao'); }
	get jsonValoresCte() { return this.modelForm.get('jsonValoresCte'); }
	get jsonValoresRecotacao() { return this.modelForm.get('jsonValoresRecotacao'); }
	get entregaPeso() { return this.modelForm.get('entregaPeso'); }
	get entregaAltura() { return this.modelForm.get('entregaAltura'); }
	get entregaLargura() { return this.modelForm.get('entregaLargura'); }
	get entregaComprimento() { return this.modelForm.get('entregaComprimento'); }

	getTitle(): string {
		return 'Detalhes';
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

	getArquivoConciliacao(conciliacaoId: number) {
		this.viewLoading = true;
		this._service.getArquivo(conciliacaoId).subscribe(urlArquivo => {
			this.viewLoading = false;
			window.open(urlArquivo, "_blank");
		}, error => {
			if (error) this._alertService.show("Error", "Houve um erro ao carregar o Arquivo " + error.error, 'error');
			else this._alertService.show("Error", "Houve um erro ao carregar o Arquivo ", 'error');

			this.viewLoading = false;
		});
	}

	enviarRecalculoFrete(){
		let id : number[] = [this.model.codigoConciliacao]
		
		this._alertService.confirmationMessage("",`Este recálculo utilizará as tabelas vigentes atuais, tem certeza que deseja seguir?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this.viewLoadingRecalculoFrete = true;
				this._service.postEnviaConciliacaoRecalculoFrete(id).subscribe(res => {
					this._alertService.show("Sucesso.", "Item enviado para recálculo.", 'success');
					this.viewLoadingRecalculoFrete = false;
					this.loadRecalculo = true;
					this.dialogRef.close();
				}, error => {			
					if (error.errors){
						if (error.errors.length > 0) {
							let errorMessage = error.errors[0].message || "Erro ao enviar para recálculo";
							this._alertService.show("Erro.", errorMessage, 'error');
						}
					}
					this.viewLoadingRecalculoFrete = false;
				});
			}
		});
	}
}