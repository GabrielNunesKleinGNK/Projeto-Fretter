import { EntityBase } from "./entityBase";

export class FaturaStatus extends EntityBase {
	
	constructor(){
		super();
		this.id = 0;
	}
	descricao: string;
}
