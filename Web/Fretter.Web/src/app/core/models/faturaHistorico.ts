import { EntityBase } from "./entityBase";
import { FaturaStatus } from "./faturaStatus";

export class FaturaHistorico extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
	}

	FaturaId : number;
	FaturaStatusId : number;
	Descricao : string;
	ValorCustoFrete : number;
	ValorCustoAdicional : number;
	ValorCustoReal : number;
	QuantidadeEntrega : number;
	QuantidadeSucesso : number;
	QuantidadeDivergencia : number;
	FaturaStatusAnteriorId : number;

	Motivo : string;

	faturaStatus : FaturaStatus;
	faturaStatusAnterior : FaturaStatus;
}
