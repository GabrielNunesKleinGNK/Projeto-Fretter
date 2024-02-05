import { ChangeDetectionStrategy, Component, OnInit, AfterViewInit, ChangeDetectorRef, OnDestroy, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutConfigService } from '../../../../../core/services/layout-config.service';
import { SubheaderService } from '../../../../../core/services/layout/subheader.service';
import { DashboardService } from '../../../../../core/services/dashboard.service';
import { DashBoardFiltro } from '../../../../../core/models/dashboadFiltro';
// @ts-ignore
import * as am4core from "@amcharts/amcharts4/core"
// @ts-ignore
import * as am4charts from "@amcharts/amcharts4/charts"
// @ts-ignore
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { Subscription } from 'rxjs';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';

@Component({
	selector: 'm-dashboard-regional',
	templateUrl: './dashboard.regional.component.html',
	styleUrls:['./dashboard.regional.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardRegionalComponent implements OnInit, AfterViewInit, OnDestroy {
	dataSource: MatTableDataSource<any>;
	displayedColumns: string[] = ['regional','quantidade','pontos'];
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	subFiltro: Subscription;
	public config: any;

	public dados: any[];
	public total : number = 0;
	public chart : any;

	chartXY: am4charts.XYChart;
    chartPie: am4charts.PieChart;

	constructor(
		private router: Router,
		private layoutConfigService: LayoutConfigService,
		private subheaderService: SubheaderService,
		private service : DashboardService,
		private cdr: ChangeDetectorRef) {
	}

	ngOnInit(): void {

	}

	ngAfterViewInit(): void {
		am4core.useTheme(am4themes_animated);
		this.subFiltro = this.service.onAtualizar.subscribe(data => {
			this.load(data);
		});
	}

	load(filtro : DashBoardFiltro) {
		// this.service.getRegional(filtro).subscribe(data => {
		// 	this.dados = data;
		// 	this.createChart();

		// 	this.dataSource = new MatTableDataSource(data);
		// 	this.dataSource.sort = this.sort;
		// 	this.dataSource.paginator = this.paginator;
		// 	this.cdr.detectChanges();
		// });
	}

	createChart(){
		var chart = am4core.create("chartregionaldiv", am4charts.PieChart);
		var pieSeries = chart.series.push(new am4charts.PieSeries());
		pieSeries.dataFields.value = "quantidade";
		pieSeries.dataFields.category = "regional"

		
		// Let's cut a hole in our Pie chart the size of 30% the radius
		chart.innerRadius = am4core.percent(30);
		
		// Put a thick white border around each Slice
		pieSeries.slices.template.stroke = am4core.color("#fff");
		pieSeries.slices.template.strokeWidth = 2;
		pieSeries.slices.template.strokeOpacity = 1;
		pieSeries.slices.template
		  // change the cursor on hover to make it apparent the object can be interacted with
		  .cursorOverStyle = [
			{
			  "property": "cursor",
			  "value": "pointer"
			}
		  ];
		
		pieSeries.alignLabels = false;
		pieSeries.labels.template.text = "[bold]{category}";
		// pieSeries.tooltipText = "[bold]{name}";
		// pieSeries.labels.template.bent = true;
		// pieSeries.labels.template.radius = 3;
		// pieSeries.labels.template.padding(0,0,0,0);
		
		// pieSeries.ticks.template.disabled = true;
		
		// Create a base filter effect (as if it's not there) for the hover to return to
		var shadow = pieSeries.slices.template.filters.push(new am4core.DropShadowFilter);
		shadow.opacity = 0;
		
		// Create hover state
		var hoverState = pieSeries.slices.template.states.getKey("hover"); // normally we have to create the hover state, in this case it already exists
		
		// Slightly shift the shadow and make it more prominent on hover
		var hoverShadow = hoverState.filters.push(new am4core.DropShadowFilter);
		hoverShadow.opacity = 0.7;
		hoverShadow.blur = 5;
		
		// Add a legend
		chart.legend = new am4charts.Legend();
		
		chart.data = this.dados;
	}

	ngOnDestroy() {
        if (this.subFiltro)
       	 this.subFiltro.unsubscribe();       
    }   
}
