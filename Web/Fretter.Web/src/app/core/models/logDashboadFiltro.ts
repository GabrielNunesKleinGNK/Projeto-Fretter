
	export class LogDashboardFiltro{
		constructor() {
			this.level = "";
			this.periodoId = 1;
		}
		id : number;
		application: string;
		process: string;
		method: string;
		dataInicio : Date;
		dataTermino : Date;
		level : string;
		periodoId: number;
		periodos: any[] = [
			{id: 1, nome : "Hoje"},
			{id: 2, nome : "Ontem"}, 
			{id: 3, nome : "Semana Atual"}, 
			{id: 4, nome : "Semana Passada"}, 
			{id: 5, nome : "Mes Atual"}, 
			{id: 6, nome : "Mes Passado"},
			{id: 7, nome : "Trimestre"},
			{id: 8, nome : "Semestre"},
			{id: 9, nome : "Ano Atual"},
			{id: 10, nome : "12 Meses"},			
		];
		levels: any[] = [
			{id: "", nome : "Todos"},
			{id: "Information", nome : "Informação"},
			{id: "Warning", nome : "Alerta"}, 
			{id: "Error", nome : "Erro"}, 
		];
    }
