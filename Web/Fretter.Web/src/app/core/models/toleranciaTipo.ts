import { EntityBase } from "./entityBase";

export class ToleranciaTipo extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
	}
    id: number;
    descricao: string;
}
