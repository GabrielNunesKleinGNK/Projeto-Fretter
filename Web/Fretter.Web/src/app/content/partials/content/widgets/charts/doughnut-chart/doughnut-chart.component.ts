import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'm-doughnut-chart',
	templateUrl: './doughnut-chart.component.html',
	styleUrls: ['./doughnut-chart.component.scss']
})
export class DoughnutChartComponent implements OnInit {
	// Doughnut

	public doughnutChartLabels: string[] = ['Gestor 1', 'Gestor 2', 'Gestor 3','Gesor 4','Gestor 5'];
	public doughnutChartData: number[] = [350, 450, 100, 800, 700];
	public doughnutChartType: string = 'pie';

	constructor () { }

	ngOnInit () {
	}

	// events
	chartClicked (e: any): void {
	}

	chartHovered (e: any): void {
	}

}
