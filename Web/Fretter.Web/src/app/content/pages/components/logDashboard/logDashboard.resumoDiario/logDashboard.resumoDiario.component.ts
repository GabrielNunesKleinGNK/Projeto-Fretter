import { ChangeDetectionStrategy, Component, OnInit, AfterViewInit, ChangeDetectorRef, OnDestroy, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutConfigService } from '../../../../../core/services/layout-config.service';
import { SubheaderService } from '../../../../../core/services/layout/subheader.service';
// @ts-ignore
import * as am4core from "@amcharts/amcharts4/core"
// @ts-ignore
import * as am4charts from "@amcharts/amcharts4/charts"
import am4lang_pt_BR from "@amcharts/amcharts4/lang/pt_BR";
// @ts-ignore
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { Subscription } from 'rxjs';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { LogDashboardService } from '../../../../../core/services/logDashboard.service';
import { LogDashboardFiltro } from '../../../../../core/models/logDashboadFiltro';

@Component({
	selector: 'm-log-dashboard-resumoDiario',
	templateUrl: './logDashboard.resumoDiario.component.html',
	styleUrls:['./logDashboard.resumoDiario.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class LogDashboardResumoDiarioComponent implements OnInit, AfterViewInit, OnDestroy {
	dataSource: MatTableDataSource<any>;
	displayedColumns: string[] = ['data','categoria','percentual'];
  
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;

	subFiltro: Subscription;
	public config: any;

	public produtos: any[];
	public total : number = 0;
	public chart : any;

	chartXY: am4charts.XYChart;
    chartPie: am4charts.PieChart;
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
		this.service.getGrafico(filtro).subscribe(data =>{
			this.resumoDiario = data;
			this.createChart();
		});

		
	}
	
	createChart(){

		this.chart = am4core.create('chartdiv', am4charts.XYChart)
		this.chart.language.locale = am4lang_pt_BR;
		this.chart.colors.step = 2;

		this.chart.data = this.generateChartData();

		let dateAxis = this.chart.xAxes.push(new am4charts.DateAxis());
		dateAxis.renderer.minGridDistance = 50;

		let valueAxis = this.chart.yAxes.push(new am4charts.ValueAxis());

		this.createAxisAndSeries("erro", "Erro", false,valueAxis, "#e85350");
		this.createAxisAndSeries("warning", "Alerta", false,valueAxis, "#f5e61e");
		this.createAxisAndSeries("info", "Informação", false,valueAxis, "#2e73b7");

		// Add legend
		this.chart.legend = new am4charts.Legend();

		// Add cursor
		this.chart.cursor = new am4charts.XYCursor();
	}

	ngOnDestroy() {
        if (this.subFiltro)
       	 this.subFiltro.unsubscribe();       
	}   

	getRandom(min, max) {
		return Math.random() * (max - min) + min;
	}

	generateChartData() {

		let dados = [];

		this.resumoDiario.forEach(mensagem => {
			let encontrado = dados.filter((item) => {
				return item.date == Date.parse(mensagem.data);
			});

			if(encontrado.length == 0)
				dados.push({ date : Date.parse(mensagem.data)});
		});

		this.resumoDiario.forEach(mensagem => {
			let chartItem = dados.filter((item) => {
				return item.date == Date.parse(mensagem.data);
			});


			if(mensagem.status == "Error")
				chartItem[0]["erro"] = mensagem.quantidade;

			if(mensagem.status == "Warning")
				chartItem[0]["warning"] = mensagem.quantidade;

			if(mensagem.status == "Information")
				chartItem[0]["info"] = mensagem.quantidade;		
			
		});
		return dados;
	}

	createAxisAndSeries(field, name, opposite, valueAxis, cor) {		
		let series = this.chart.series.push(new am4charts.LineSeries());
		series.dataFields.valueY = field;
		series.dataFields.dateX = "date";
		series.strokeWidth = 2;
		series.yAxis = valueAxis;
		series.name = name;
		series.tooltipText = "{name}: [bold]{valueY}[/]";
		series.tensionX = 0.8;
		series.showOnInit = true;
		series.stroke = am4core.color(cor);
		series.color = am4core.color(cor);
		series.fill = am4core.color(cor);
		
		let interfaceColors = new am4core.InterfaceColorSet();
		
		let bullet = series.bullets.push(new am4charts.CircleBullet());
		bullet.circle.stroke = interfaceColors.getFor("background");
		bullet.circle.strokeWidth = 2;
		
		valueAxis.renderer.line.strokeOpacity = 1;
		valueAxis.renderer.line.strokeWidth = 2;
		valueAxis.renderer.line.stroke = series.stroke;
		valueAxis.renderer.labels.template.fill = series.stroke;
		valueAxis.renderer.opposite = opposite;

		series.tooltip.label.fill = am4core.color(cor);
		series.tooltip.background.stroke = am4core.color(cor);
	  }

}