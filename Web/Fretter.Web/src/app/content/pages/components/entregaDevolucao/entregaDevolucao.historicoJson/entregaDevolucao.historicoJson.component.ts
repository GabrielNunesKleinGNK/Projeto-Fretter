import {
	Component,
	OnInit,
	ViewChild,
	Inject
} from '@angular/core';

import { MatDialogRef, MatPaginator, MAT_DIALOG_DATA } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { EntregaDevolucaoService } from "../../../../../core/services/entregaDevolucao.service";

@Component({
	selector: 'm-entrega-devolucao-json',
	templateUrl: './entregaDevolucao.historicoJson.component.html'
})
export class EntregaDevolucaoHistoricoJsonComponent implements OnInit {
	jsonEntrega: string;
	loadingAfterSubmit: boolean = false;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(public dialogRef: MatDialogRef<EntregaDevolucaoHistoricoJsonComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: EntregaDevolucaoService) { }
	ngOnInit() {
		if (this.data)
			this.jsonEntrega = JSON.parse(this.data.jsonData);
	}

	close() {
		this.dialogRef.close();
	}
}
