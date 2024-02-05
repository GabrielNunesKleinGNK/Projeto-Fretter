
	export class FaturaPeriodoFiltro{
		constructor() {
			this.periodoId = 1;
			this.statusConciliacaoId = 0;
		}
		id : number;
		application: string;
		process: string;
		method: string;
		dataInicio : Date;
		dataTermino : Date;
		periodoId: number;
		transportadorId: number;
		statusConciliacaoId: number;
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
		statusConciliacao: any[] = [
			{id: 0, nome : "Todos"},
			{id: 2, nome : "Valida"}, 
			{id: 3, nome : "Divergente"}, 
		];
    }
