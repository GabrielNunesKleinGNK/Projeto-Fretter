import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import jsonview from '@pgrabovets/json-view';

@Component({
  selector: 'm-log-cotacao-frete-jsonView',
  templateUrl: './logCotacaoFrete.jsonView.component.html',
  styleUrls: ['./logCotacaoFrete.jsonView.component.scss']
})
export class LogCotacaoFreteJsonViewComponent implements OnInit  {
  constructor(public dialogRef: MatDialogRef<LogCotacaoFreteJsonViewComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any){

    }

  ngOnInit() {
    let tree = jsonview.create(this.data.obj.json);
    jsonview.render(tree, document.querySelector('.jsonView'));
    jsonview.expand(tree);
  }

  close() {
		this.dialogRef.close();
	}
}