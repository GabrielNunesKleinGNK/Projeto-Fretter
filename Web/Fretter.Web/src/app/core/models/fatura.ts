import { EntityBase } from "./entityBase";
import { FaturaPeriodo } from "./faturaPeriodo";
import { FaturaStatus } from "./faturaStatus";
import { TransportadorCnpj } from "./transportadorCnpj.model";

export class Fatura extends EntityBase {


	constructor() {
		super();
		this.id = 0;
	}

	empresaId: number;
	transportadorId: number;
	faturaPeriodoId: number;
	valorCustoFrete: number;
	valorCustoAdicional: number;
	valorCustoReal: number;
	valorDocumento: number;
	quantidadeDevolvidoRemetente: number;
	faturaStatusId: number;
	dataVencimento: Date;
	quantidadeSucesso: number;
	quantidadeEntrega: number;
	quantidadeDivergencia: number;

	faturaStatus: FaturaStatus;
	faturaPeriodo: FaturaPeriodo;
	transportador: TransportadorCnpj;
	visualizar: boolean;	
}
