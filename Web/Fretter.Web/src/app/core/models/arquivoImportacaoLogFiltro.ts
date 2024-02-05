
	export class ArquivoImportacaoLogFiltro{
		constructor() {
			this.periodoId = 1;
		}
		id : number;
		processType: string;
		method: string;
		dataInicio : Date;
		dataTermino : Date;
		periodoId: number;
		requestNumber:string;
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
		process: any[] = [
			{id: "", nome : "Todos"},
			{id: 1, nome : "Lote"},
			{id: 2, nome : "Entrega"}
		];
    }
