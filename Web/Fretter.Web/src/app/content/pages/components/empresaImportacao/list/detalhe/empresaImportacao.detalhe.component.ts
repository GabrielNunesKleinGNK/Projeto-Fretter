import {
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Input,
	Inject
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { EmpresaImportacaoDetalhe } from '../../../../../../core/models/empresaImportacaoDetalhe.model';
import { AlertService } from '../../../../../../core/services/alert.service';
import { EmpresaService } from '../../../../../../core/services/empresa.service';

@Component({
	selector: 'm-empresaImportacaoDetalhe',
	templateUrl: './empresaImportacao.detalhe.component.html',
	styleUrls: ['./empresaImportacao.detalhe.component.scss']
})
export class EmpresaImportacaDetalheComponent implements OnInit {

	dataSource: MatTableDataSource<EmpresaImportacaoDetalhe>;
	displayedColumns: string[] = ['CodigoEmpresa', 'Nome', 'Cnpj', 'Cep', 'Email', 'Token', 'Sucesso'];
	@Input() importacaoArquivoId: number;
	viewLoading: boolean = false;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(
		public dialogRef: MatDialogRef<EmpresaImportacaDetalheComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: EmpresaService, public dialog: MatDialog,
		private cdr: ChangeDetectorRef, private _alertService: AlertService) {
		this.importacaoArquivoId = data.importacaoArquivoId;
	}
	ngOnInit() {
		this.load();
	}

	load() {
		this._service.obterEmpresaImportacaoDetalhe(this.importacaoArquivoId).subscribe(data => {
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		});
	}

	copiarToken(model) {
		if (model.token) {
			const selBox = document.createElement('textarea');
			selBox.style.position = 'fixed';
			selBox.style.left = '0';
			selBox.style.top = '0';
			selBox.style.opacity = '0';
			selBox.value = model.token;
			document.body.appendChild(selBox);
			selBox.focus();
			selBox.select();
			document.execCommand('copy');
			document.body.removeChild(selBox);
			this._alertService.show("Sucesso", `O token "${model.token}" foi copiado para área de tranferência`, "success");
		} else {
			this._alertService.show("Erro", `O token está vazio ou inválido`, "error");
		}
	}

	exibirErro(model) {
		if (model.descricao) {
			this._alertService.show("Erro", `Erro: "${model.descricao}"`, "error");
		}
	}

	close() {
		this.dialogRef.close();
	}
}

