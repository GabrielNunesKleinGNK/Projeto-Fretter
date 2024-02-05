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
	selector: 'm-log-dashboard-resumoMensagem',
	templateUrl: './logDashboard.resumoMensagem.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class LogDashboardResumoMensagemComponent implements OnInit, AfterViewInit, OnDestroy {
	dataSource: MatTableDataSource<any>;
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	subFiltro: Subscription;
	public config: any;

	public total : number = 0;
	public chart : any;
    pieSeries:any;
	resumoDiario: any[];

	constructor(
		private router: Router,
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
		this.service.getResumo(filtro).subscribe(data =>{
			this.resumoDiario = data;
			this.createChart();
		});
	}
	
	createChart(){
		
		this.chart = am4core.create('chartdivMensagem', am4charts.PieChart);
		this.chart.data = this.generateChartData(undefined);

		this.pieSeries = this.chart.series.push(new am4charts.PieSeries());
		
		this.pieSeries.dataFields.value = "percent";
		this.pieSeries.dataFields.category = "type";
		this.pieSeries.slices.template.propertyFields.fill = "color";
		this.pieSeries.slices.template.propertyFields.isActive = "pulled";
		this.pieSeries.slices.template.strokeWidth = 0;
		this.pieSeries.slices.template.events.on("hit", 
			function(event){
				let selected;
				if (event.target.dataItem.dataContext.id != undefined) {
					selected = event.target.dataItem.dataContext.id;
				} else {
					selected = undefined;
				}
				this.chart.data = this.generateChartData(selected);
			},this);
	}

	ngOnDestroy() {
        if (this.subFiltro)
       	 this.subFiltro.unsubscribe();       
	}   

	getRandom(min, max) {
		return Math.random() * (max - min) + min;
	}

	public generateChartData(selected) {
		let chartData = [];
		for (var i = 0; i < this.resumoDiario.length; i++) {
		  if (i == selected) {
			for (var x = 0; x < this.resumoDiario[i].subs.length; x++) {
			  chartData.push({
				type: this.resumoDiario[i].subs[x].type,
				percent: this.resumoDiario[i].subs[x].percent,
				color: this.resumoDiario[i].color,
				pulled: true
			  });
			}
		  } else {
			chartData.push({
			  type: this.resumoDiario[i].type,
			  percent: this.resumoDiario[i].percent,
			  color: this.resumoDiario[i].color,
			  id: i
			});
		  }
		}
		return chartData;
	  }
	
	  eventPieSeriesChart(event: any) {
		let selected;
		if (event.target.dataItem.dataContext.id != undefined) {
			selected = event.target.dataItem.dataContext.id;
		} else {
			selected = undefined;
		}
		this.chart.data = this.generateChartData(selected);
	}
}


