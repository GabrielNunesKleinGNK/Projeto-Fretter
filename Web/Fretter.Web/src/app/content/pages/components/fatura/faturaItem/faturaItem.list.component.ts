import {
	Component,
	OnInit,
	Inject,
	ViewChild
} from '@angular/core';

import {
	MatPaginator,
	MatSort,
	MatDialog,
	MatTableDataSource,
	MAT_DIALOG_DATA
} from '@angular/material';

import { FaturaItem } from '../../../../../core/models/faturaItem';
import { FaturaItemService } from '../../../../../core/services/faturaItem.service';

@Component({
	selector: 'm-faturaItem-edit',
	templateUrl: './faturaItem.list.component.html',
	styleUrls: ['./faturaItem.list.component.scss']
})
export class FaturaItemListComponent implements OnInit {
	model: FaturaItem;
	dataSource: MatTableDataSource<FaturaItem> = new MatTableDataSource(new Array<FaturaItem>());
	displayedColumns: string[] = ['descricao', 'valor'];
	viewLoading: boolean = true;
	resultsLength: number = 0;
	start: number = 0;
	size: number = 15;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: FaturaItemService,
		public dialog: MatDialog) {
	}
	ngOnInit() {
		this.load();
	}

	load() {
		this.pesquisar();
	}

	pesquisar() {
		const model = new FaturaItem();
		delete model.id;
		model.faturaId = this.data;
		this.viewLoading = true;
		this._service.getFilter(model, this.start, this.size).subscribe(result => {
			this.dataSource.data = result.data;
			this.resultsLength = result.total;
			this.viewLoading = false;
		});
	}

	pageChange(event) {
		this.size = event.pageSize;
		this.start = this.size * event.pageIndex;
		this.pesquisar();
	}
}