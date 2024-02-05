import {
    Component,
    OnInit,
    ViewChild,
    ChangeDetectorRef
} from '@angular/core';

import { MatPaginator, MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { Subscription } from 'rxjs';
import { LogCotacaoFreteService } from '../../../../../core/services/logCotacaoFrete.service';
import { LogCotacaoFreteFiltro } from '../../../../../core/models/logCotacaoFreteFiltro';
import { LogCotacaoFreteLista } from '../../../../../core/models/logCotacaoFreteLista';
import { LogCotacaoFreteJsonViewComponent } from '../logCotacaoFrete.jsonView/logCotacaoFrete.jsonView.component';
import { AlertService } from '../../../../../core/services/alert.service';


@Component({
    selector: 'm-log-cotacao-frete-list',
    styleUrls: ['./logCotacaoFrete.list.component.scss'],
    templateUrl: './logCotacaoFrete.list.component.html'
})
export class LogCotacaoFreteListComponent implements OnInit {
    //List
    dataSource: MatTableDataSource<LogCotacaoFreteLista>;
    displayedColumns: string[] = ['timestamp',
        'empresaId',
        'instancia',     
        'hostName',
        'level',
        'cotacaoPeso',
        'cotacaoCepInvalido',
        'actions'];

    viewLoading: boolean = false;
    subFiltro: Subscription;

    @ViewChild(MatSort) sort: MatSort;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    constructor(
        public dialog: MatDialog,
        private service: LogCotacaoFreteService,
        private _alertService: AlertService,
        //Comum
        private cdr: ChangeDetectorRef
    ) {

    }
    ngOnInit() {
        this.subFiltro = this.service.onAtualizar.subscribe(data => {
            this.load(data);
        });
    }

    load(filtro: LogCotacaoFreteFiltro) {
        this.viewLoading = true;		
        this.service.getLista(filtro).subscribe(result => {
            this.dataSource = new MatTableDataSource(result);
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.paginator;
            this.cdr.detectChanges();
            this.viewLoading = false;
        }, error => {
			if (error) this._alertService.show("Error", "Houve um erro ao carregar Log: " + error.error, 'error');
			else this._alertService.show("Error", "Houve um erro ao carregar Log.", 'error');
			this.viewLoading = false;
		});
    }

    showObject(model){
		let obj = {
			json: model.objectJson
		};
		const dialogRef = this.dialog.open(LogCotacaoFreteJsonViewComponent, { data:{ obj } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
		});
	}
}

