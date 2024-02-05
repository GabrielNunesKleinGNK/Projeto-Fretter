import { EntityBase } from "./entityBase";

export class ConfiguracaoCteTipo extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
	}
    id: number;
    descricao: string;
}
