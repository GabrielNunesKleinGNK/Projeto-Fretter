import { ChangeDetectionStrategy, Component, OnInit, AfterViewInit, ChangeDetectorRef, OnDestroy, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutConfigService } from '../../../../../core/services/layout-config.service';
import { SubheaderService } from '../../../../../core/services/layout/subheader.service';
// @ts-ignore
import * as am4core from "@amcharts/amcharts4/core"
// @ts-ignore
import * as am4charts from "@amcharts/amcharts4/charts"
// @ts-ignore
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { Subscription } from 'rxjs';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { LogDashboardService } from '../../../../../core/services/logDashboard.service';
import { LogDashboardFiltro } from '../../../../../core/models/logDashboadFiltro';

@Component({
	selector: 'm-log-dashboard-resumoProcesso',
	templateUrl: './logDashboard.resumoProcesso.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class LogDashboardResumoProcessoComponent implements OnInit, AfterViewInit, OnDestroy {
	dataSource: MatTableDataSource<any>;
	displayedColumns: string[] = ['data','categoria','percentual'];
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	subFiltro: Subscription;
	public config: any;

	public total : number = 0;
	public chart : any;
	public count: number;
	resumoDiario: any[];

	constructor(
		private router: Router,
		private layoutConfigService: LayoutConfigService,
		private subheaderService: SubheaderService,
		private service : LogDashboardService,
		private cdr: ChangeDetectorRef) {
	}

	ngOnInit(): void {
		am4core.useTheme(am4themes_animated);
		this.subFiltro = this.service.onAtualizar.subscribe(data => {
			this.load(data);
		});
	}

	ngAfterViewInit(): void {		
	}

	load(filtro : LogDashboardFiltro) {
		this.service.getProcesso(filtro).subscribe(data =>{
			this.resumoDiario = data;
			this.createChart();
		});
	}

	createChart(){
		this.chart = am4core.create('chartdivProcesso', am4charts.PieChart3D);
		this.chart.hiddenState.properties.opacity = 0; 
		this.chart.data =this.resumoDiario;
		
		this.chart.innerRadius = am4core.percent(40);
		this.chart.depth = 15;


		var series = this.chart.series.push(new am4charts.PieSeries3D());
		series.dataFields.value = "total";
		series.dataFields.depthValue = "total";
		series.dataFields.category = "type";
		series.slices.template.cornerRadius = 5;
		series.colors.step = 2;
	}
	

	
	ngOnDestroy() {
        if (this.subFiltro)
       	 this.subFiltro.unsubscribe();       
	}   

	getRandom(min, max) {
		return Math.random() * (max - min) + min;
	}

}