import {
	Component,
	OnInit,
	ElementRef,
	ViewChild,
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Input
} from '@angular/core';

import { MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { ArquivoCobranca } from '../../../../../../core/models/arquivoCobranca';
import { AlertService } from '../../../../../../core/services/alert.service';
import { ArquivoCobrancaService } from '../../../../../../core/services/arquivoCobranca.service';
import { FaturaService } from '../../../../../../core/services/fatura.service';

@Component({
  selector: 'm-arquivoCobranca',
  templateUrl: './arquivoCobranca.component.html',
  styleUrls: ['./arquivoCobranca.component.scss']
})
export class ArquivoCobrancaComponent implements OnInit {

	dataSource: MatTableDataSource<ArquivoCobranca>;
	displayedColumns: string[] = [ 'Id', 'DocRemetente', 'DocDestinatario', 'QtdTotal', 'QtdItens', 'ValorTotal','DataCadastro', 'actions'];
	@Input() faturaId : number;
	viewLoading: boolean = false;
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(private _service: ArquivoCobrancaService, public dialog: MatDialog, private cdr: ChangeDetectorRef, private _alertService : AlertService
		){
		
	}
	ngOnInit() {
		this.load();
	}

	load() {
		this._service.getAll(this.faturaId).subscribe(data => {
			this.dataSource = new MatTableDataSource(data);
			this.dataSource.sort = this.sort;
			this.dataSource.paginator = this.paginator;
			this.cdr.detectChanges();
		 });
	}

	inputChange(fileInputEvent: any) {
		let file = fileInputEvent.target.files[0];
		if(file){
			const formData = new FormData();
			formData.append('file', file);
			this.viewLoading = true;
			this._service.importarArquivo(formData, this.faturaId).subscribe(() => {
				this._alertService.show("Sucesso", "Arquivo importado com sucesso!", 'success');
				this.load();
				this.viewLoading = false;
			}, (error) => {
				console.log(error);
				this._alertService.show("Error", "Erro ao importar arquivo DOCCOB!", 'error');
				this.viewLoading = false;
			});
		}
	}

	deletarArquivo(arquivo){
		this._alertService.confirmationMessage("",`Deseja realmente deletar o arquivo "${arquivo.id}"?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this._service.delete(arquivo.id).subscribe(() => {
					this._alertService.show("Sucesso", "Arquivo removido com sucesso!", 'success');
					this.load();
				}, (error) => {
					console.log(error);
					this._alertService.show("Error", "Erro ao deletar arquivo DOCCOB!", 'error');
				});
			}
		});
	}
}

