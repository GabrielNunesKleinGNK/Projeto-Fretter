import { EntityBase } from "./entityBase";

export class FaturaItem extends EntityBase {
	constructor() {
		super();
		this.id = 0;
	}

	valor : number;    
	descricao : string;
	faturaId : number;
	
}
