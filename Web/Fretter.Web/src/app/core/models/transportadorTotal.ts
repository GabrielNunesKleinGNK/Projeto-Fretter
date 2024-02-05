import { EntityBase } from "./entityBase";

export class TransportadorTotal extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
	}
    transportador: string;
    qtdEntrega : number;
    qtdCte : number;
    qtdSucesso : number;
    qtdErro : number;
    qtdDivergencia : number;
    qtdDivergenciaPeso : number;
    qtdDivergenciaTarifa : number;
}
