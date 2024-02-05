import { EntityBase } from "./entityBase";

export class ImportacaoArquivoCritica extends EntityBase {

	constructor(){
		super();
		this.id = 0;
	}
    
    descricao: string;
	linha: number;
	campo: string;
}
