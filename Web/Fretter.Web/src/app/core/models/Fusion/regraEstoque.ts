
import { Empresa } from "../empresa.model";
import { EntityBase } from "../entityBase";
import { Canal } from "./Canal";
import { Grupo } from "./grupo";

export class RegraEstoque extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
    }
    empresaId: number;  
    canalIdOrigem: number;
    canalIdDestino: number;
    grupoId: number;
    skus: Date; 
    dataCadastro: Date;
    canalOrigem: Canal;
    canalDestino: Canal;
    empresa: Empresa;
    grupo: Grupo;
}
