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
	selector: 'm-entrega-devolucao-xml',
	templateUrl: './entregaDevolucao.historicoXML.component.html'
})
export class EntregaDevolucaoHistoricoXMLComponent implements OnInit {
	xmlData: string;
	loadingAfterSubmit: boolean = false;

	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	constructor(public dialogRef: MatDialogRef<EntregaDevolucaoHistoricoXMLComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private _service: EntregaDevolucaoService) { }
	ngOnInit() {
		if (this.data)
			this.xmlData = this.data.xmlData;
	}

	close() {
		this.dialogRef.close();
	}
	copiaXML() {
		const selBox = document.createElement('textarea');
		selBox.style.position = 'absolute';
		selBox.style.left = '0';
		selBox.style.top = '0';
		selBox.style.opacity = '0';
		selBox.value = this.xmlData;
		selBox.setAttribute('readonly', '');
		document.body.appendChild(selBox);
		selBox.focus();
		selBox.select();
		document.execCommand('copy');
		document.body.removeChild(selBox);
	}
}
