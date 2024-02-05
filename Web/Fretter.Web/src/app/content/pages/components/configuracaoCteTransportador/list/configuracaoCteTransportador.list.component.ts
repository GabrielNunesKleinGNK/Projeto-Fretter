import {
	Component,
	OnInit,
	ChangeDetectorRef,
	AfterContentChecked
} from '@angular/core';

import { MatDialog } from '@angular/material';
import {MatTableDataSource} from '@angular/material/table';
import { AlertService } from '../../../../../core/services/alert.service';
import { ConfiguracaoCteTransportadorEditComponent } from '../edit/configuracaoCteTransportador.edit.component';
import { ConfiguracaoCteTransportadorService } from '../../../../../core/services/configuracaoCteTransportador.service';
import { ConfiguracaoCteTransportador } from '../../../../../core/models/configuracaoCteTransportador';
import { ConfiguracaoCteTransportadorFiltro } from '../../../../../core/models/Filters/configuracaoCteTransportadorFiltro';
import { TransportadorCnpjService } from '../../../../../core/services/transportadorCnpj.service';

@Component({
  selector: 'm-configuracaoCteTransportador',
  templateUrl: './configuracaoCteTransportador.list.component.html'
})
export class ConfiguracaoCteTransportadorListComponent implements OnInit, AfterContentChecked  {
	
	dataSource: MatTableDataSource<ConfiguracaoCteTransportador> = new MatTableDataSource(new Array<ConfiguracaoCteTransportador>());
	displayedColumns: string[] = ['id', 'transportador', 'cnpj', 'alias', 'tipo', 'dataCadastro', 'actions'];
	filtro: ConfiguracaoCteTransportadorFiltro;
	transportadoresCnpj: any[];

	viewLoading: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 5;

	constructor(private _service: ConfiguracaoCteTransportadorService, 
				public dialog: MatDialog, 
				private cdr: ChangeDetectorRef, 
				private _alertService : AlertService,
				private transportadorCnpjService: TransportadorCnpjService){
	}
	
	ngOnInit() {
		this.filtro = new ConfiguracaoCteTransportadorFiltro();
		this.load();
	}
	
	ngAfterContentChecked(): void {
		this.cdr.detectChanges();
	}

	load() {
		this.transportadorCnpjService.getFilter(null, 0, 10000).subscribe(result =>{
			this.transportadoresCnpj = result.data;
	   	});
		this.pesquisar();
	}

	pesquisar(){
		this.viewLoading = true;
		this._service.getFilter(this.filtro, this.start, this.size).subscribe(result => {
			this.dataSource.data = result.data;
			this.resultsLength = result.total;
			this.viewLoading = false;
		});
	}

	pageChange(event){		
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}


	novo() {
		var configuracaoTransportador = new ConfiguracaoCteTransportador();
		const dialogRef = this.dialog.open(ConfiguracaoCteTransportadorEditComponent, { data: { model:configuracaoTransportador } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	edit(model: ConfiguracaoCteTransportador) {
		const dialogRef = this.dialog.open(ConfiguracaoCteTransportadorEditComponent, { data: { model } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.load();
		});
	}

	delete(model: ConfiguracaoCteTransportador) {
		this._alertService.confirmationMessage("",`Deseja realmente deletar a configuração "${model.id}"?`,'Confirmar','Cancelar').then((result) => {
			if (result.value) {
				this._service.delete(model.id).subscribe(r=> {
					this._alertService.show("Sucesso","Configuração deletada com sucesso.",'success');
					this.load();
				});
			}
		});
	}
}

