import { ChangeDetectorRef, OnDestroy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { DashBoardFiltro } from '../../../../../core/models/dashboadFiltro';
import { DashboardService } from '../../../../../core/services/dashboard.service';

@Component({
  selector: 'm-dashboard-transportadorValor',
  templateUrl: './dashboard.transportadorValor.component.html',
  styleUrls: ['./dashboard.transportadorValor.component.scss']
})
export class DashboardTransportadoresValorComponent implements OnInit, OnDestroy {

  public subFiltro: Subscription;
  constructor(private _service: DashboardService, private cdr: ChangeDetectorRef) { }
  public transportadores: any[];

  ngOnInit() {
    this.subFiltro = this._service.onAtualizar.subscribe(data => {
      this.load(data);
    });
  }

  load(filtro: DashBoardFiltro) {
    this._service.getTransportadoresValor(filtro).subscribe(data => {
      this.transportadores = data;
      this.cdr.detectChanges();
    });

    this.cdr.detectChanges();
  }

  ngOnDestroy(): void {
    if (this.subFiltro)
      this.subFiltro.unsubscribe();
  }
}
