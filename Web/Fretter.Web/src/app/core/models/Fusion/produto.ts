
import { Empresa } from "../empresa.model";
import { EntityBase } from "../entityBase";

export class Produto extends EntityBase {
	
	constructor(){
		super();
		this.id = 0;
    }
    
    empresaId: number;
    codigo: string;
    nome : string;
    preco : number;
    pesoLiq : number;
    largura : number;
    altura : number;
    comprimento : number;
}
