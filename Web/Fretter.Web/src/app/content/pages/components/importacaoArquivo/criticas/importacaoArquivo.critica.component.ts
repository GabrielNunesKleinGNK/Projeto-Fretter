import { Component,	OnInit,	ViewChild,	ChangeDetectorRef, Input, Inject} from '@angular/core';
import { MatPaginator, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MatTableDataSource } from '@angular/material/table';




@Component({
	selector: 'm-importacaoAquivo-critica',
	templateUrl: './importacaoArquivo.critica.component.html',
})
export class ImportacaoArquivoCriticaComponent implements OnInit {
	dataSource: MatTableDataSource<any> = new MatTableDataSource(new Array<any>());
	displayedColumns: string[] = ['linha', 'campo', 'descricao'];

	resultsLength: number = 0;
	start: number = 0;
	end: number = 10;
	size: number = 10;

	constructor(public dialogRef: MatDialogRef<ImportacaoArquivoCriticaComponent>,	@Inject(MAT_DIALOG_DATA) public data: any) {

	}

	ngOnInit() {
		this.pesquisar();
	}

	pesquisar(){
		this.dataSource.data = this.data.criticas.slice(this.start, this.end);
		this.resultsLength = this.data.criticas.length;
	}
	pageChange(event){
		if(event.pageIndex > event.previousPageIndex)
		{
			this.start = this.end;
			this.end =  this.end + this.size;
		} else	{
			this.end = this.start;
			this.start =  this.start - this.size;
		}

	
		this.pesquisar();
	}
}