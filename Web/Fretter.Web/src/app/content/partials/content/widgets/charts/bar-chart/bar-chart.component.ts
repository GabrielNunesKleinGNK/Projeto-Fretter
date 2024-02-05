import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'm-bar-chart',
	templateUrl: './bar-chart.component.html',
	styleUrls: ['./bar-chart.component.scss']
})
export class BarChartComponent implements OnInit {

	public barChartOptions: any = {
		scaleShowVerticalLines: false,
		responsive: true
	};
	public barChartLabels: string[] = ['VELA', 'FORM', 'LAMINA', 'TEMAS', 'EMBALG', 'BALAO L', 'BALAO METAL'];
	public barChartType: string = 'bar';
	public barChartLegend: boolean = true;

	public barChartData: any[] = [
		{data: [65, 59, 80, 81, 56, 55, 40], label: 'Março'},
		{data: [28, 48, 40, 19, 86, 27, 90], label: 'Abril'}
	];


	constructor () { }

	ngOnInit () {
	}

	// events
	chartClicked (e: any): void {
	}

	chartHovered (e: any): void {
	}

}
