
import { Empresa } from "../empresa.model";
import { EntityBase } from "../entityBase";

export class Grupo extends EntityBase {
	
	constructor(){
		super();
		this.id = 0;
    }
    rmpresaId: number;
    codigo: string;
    descricao: string;

}
