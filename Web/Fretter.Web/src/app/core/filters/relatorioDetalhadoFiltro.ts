export class RelatorioDetalhadoFiltro {
	constructor() {
	}

	DataInicial: string;
	DataFinal: string;
	EmpresaId: number;
	TransportadorId: number;
	StatusId: number;
	FaturaId: number;
	CodigoEntrega: string;
	CodigoPedido: string;
	CodigoDanfe: string;
	//Filtro Paginacao
	PageSelected: number;
	PageSize: number;
	OrderByDirection: string;
	ListSize: number;
}

