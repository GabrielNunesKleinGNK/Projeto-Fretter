
import { EntityBase } from "../entityBase";

export class EntregaEmAbertoFiltro extends EntityBase {
	constructor() {
		super();
		this.pedidos = null;
	}

	transportadorId: number;
	ocorrenciaId: number;
	empresaMarketplaceId: number;
	entregasMarketplace: boolean = false;
	//descricao: string;
	//dataImportacao: Date = new Date();
	dataUltimaOcorrencia: Date;
	pedidos: string;

	dataInicio: Date;// = new Date();
	dataFim: Date;// = new Date();

	pagina: number = 0;
	paginaLimite: number = 10;
}
