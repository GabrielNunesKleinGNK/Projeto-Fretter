import { EntityBase } from "./entityBase";

export class AgendamentoEntregaProduto extends EntityBase {

	constructor(){
		super();
		this.id = 0;
        this.valorProduto = 0.0;
        this.valorTotal = 0.0;
        this.altura = 0.0; 
        this.largura = 0.0;
        this.comprimento = 0.0;
        this.peso = 0.0; 
        this.sku = null;
        this.ean = null;
        this.nome = null;
	}
    
    linha: number;
    sku :  string;
    ean : string;
    nome : string;
    valorProduto : number;
    valorTotal : number;  
    altura : number;
    largura: number;
    comprimento: number;
    peso: number; 
}