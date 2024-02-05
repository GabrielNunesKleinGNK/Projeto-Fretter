import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'm-recent-activities',
  templateUrl: './recent-activities.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RecentActivitiesComponent implements OnInit {
  filtro: any;
  data: any[];

  constructor(private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.load();
  }
  load() {
    this.filtro.dataInicio = new Date('2019-08-01');
    this.filtro.dataTermino = new Date('2019-08-16');
	}
}
