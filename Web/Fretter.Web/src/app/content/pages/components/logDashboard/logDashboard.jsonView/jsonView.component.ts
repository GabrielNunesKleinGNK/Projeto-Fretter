import { Component, OnInit, Inject } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { Subject } from 'rxjs';
import jsonview from '@pgrabovets/json-view';

@Component({
  selector: 'm-jsonView',
  templateUrl: './jsonView.component.html',
  styleUrls: ['./jsonView.component.scss']
})
export class JsonViewComponent implements OnInit  {
  constructor(public dialogRef: MatDialogRef<JsonViewComponent>,
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