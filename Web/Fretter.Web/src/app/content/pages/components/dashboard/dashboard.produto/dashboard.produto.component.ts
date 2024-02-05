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
	selector: 'm-dashboard-produto',
	templateUrl: './dashboard.produto.component.html',
	styleUrls:['./dashboard.produto.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardProdutoComponent implements OnInit, AfterViewInit, OnDestroy {
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
		// this.service.getProduto(filtro).subscribe(data => {
		// 	this.produtos = data;
		// 	this.createChart();

		// 	this.dataSource = new MatTableDataSource(data);
		// 	this.dataSource.sort = this.sort;
		// 	this.dataSource.paginator = this.paginator;
		// 	this.cdr.detectChanges();
		// });
	}

	createSeries(value, name, cor) {
		let series = this.chart.series.push(new am4charts.ColumnSeries())
		series.dataFields.valueY = value
		series.dataFields.categoryX = 'categoria'
		series.name = name;

		// series.stroke = am4core.color(cor);
		// series.color = am4core.color(cor);
		// series.fill = am4core.color(cor);

		// let bullet = series.bullets.push(new am4charts.LabelBullet())
		// bullet.interactionsEnabled = false
		// bullet.dy = 30;
		// bullet.label.text = "{valueY.formatNumber('#.')}";
		// bullet.label.fill = am4core.color('#ffffff');

		// series.tooltip.fontSize = "0.8em";
		series.tooltipText = "{name}: [bold]{valueY.formatNumber('#.')}";
        // series.tooltip.autoTextColor = false;
        // series.tooltip.label.fill = am4core.color(cor);

        // series.tooltip.getFillFromObject = false;
        // series.tooltip.background.fill =  am4core.color("#FFF");
        // series.tooltip.background.fillOpacity = 1;
        // series.tooltip.background.stroke = am4core.color(cor);
		// series.tooltip.background.strokeWidth = 2;
		
		this.chart.cursor = new am4charts.XYCursor();

    	return series;
	}

	createChart(){

		this.chart = am4core.create('chartdiv', am4charts.XYChart)
		this.chart.colors.step = 2;

		this.chart.legend = new am4charts.Legend()
		// this.chart.legend.position = 'top'
		this.chart.legend.paddingBottom = 20
		this.chart.legend.labels.template.maxWidth = 95

		let xAxis = this.chart.xAxes.push(new am4charts.CategoryAxis())
		xAxis.dataFields.category = 'categoria'
		xAxis.renderer.cellStartLocation = 0.1
		xAxis.renderer.cellEndLocation = 0.9
		xAxis.renderer.grid.template.location = 0;


		let yAxis = this.chart.yAxes.push(new am4charts.ValueAxis());
		yAxis.min = 0;

		let periodos = [];

		this.produtos.forEach(produto => {
			let encontrado = periodos.filter((item) => {
				return item.data == produto.data;
			});

			if(encontrado.length == 0)
			periodos.push(produto);
		});


		let dados = [];

		this.produtos.forEach(produto => {
			let encontrado = dados.filter((item) => {
				return item.categoria == produto.categoria;
			});

			if(encontrado.length == 0)
				dados.push({ categoria : produto.categoria});
		});

		this.produtos.forEach(produto => {
			let chartItem = dados.filter((item) => {
				return item.categoria == produto.categoria;
			});

			chartItem[0][produto.data] = produto.percentual;
		});
		this.chart.data = dados;

		periodos.forEach(categoria => {
			this.createSeries(categoria.data, categoria.data, categoria.cor);
		});
	}

	ngOnDestroy() {
        if (this.subFiltro)
       	 this.subFiltro.unsubscribe();       
    }   
}
