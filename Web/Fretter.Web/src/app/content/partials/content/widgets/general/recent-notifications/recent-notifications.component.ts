import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { RelatorioFiltro } from '../../../../../../core/models/relatorio.filtro';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'm-recent-notifications',
  templateUrl: './recent-notifications.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [DatePipe]
})
export class RecentNotificationsComponent implements OnInit {

  filtro: RelatorioFiltro;
  data: any[];

  constructor(private cdr: ChangeDetectorRef,private datePipe: DatePipe) { }

  ngOnInit() {
    this.load();
  }
  load() {
    this.filtro = new RelatorioFiltro();
    this.filtro.dataInicio = new Date(this.datePipe.transform(new Date(), 'yyyy-MM-dd'));
    this.filtro.dataTermino= new Date(this.datePipe.transform(new Date(), 'yyyy-MM-dd'));
    this.filtro.dataTermino.setDate( this.filtro.dataTermino.getDate() + 1 );;
	}

}
