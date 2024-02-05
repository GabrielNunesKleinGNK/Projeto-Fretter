export class RelatorioFiltro{
	constructor() {
		this.categoriaNome = "";
	}
	id : number;
	segmentoId: number;
	usuarioId:number;
	usuarioNome: string;
	clienteId:number;
	clienteNome : string;
	dataInicio: Date;
	dataTermino: Date;
	notaFiscalId: number;
	produtoCategoriaId: number;
	categoriaNome: string;
	periodoId: number;
	regiaoId : number;
	uf: string;

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
		{id: 11, nome : "Custom"},
	];

	public estados : any[] = ["AC","AL","AM","AP","BA","CE","DF","ES","GO","MA","MG","MS","MT","PA","PB","PE","PI","PR","RJ","RN","RO","RR","RS","SC","SE","SP","TO"];
	public regioes : any[] = [{id: 1, nome :"Area 1"},{id: 3, nome :"Area 3"},{id: 4, nome :"Area 4"},{id: 5, nome :"Area 5"},{id: 6, nome :"Area 6"}];


}
