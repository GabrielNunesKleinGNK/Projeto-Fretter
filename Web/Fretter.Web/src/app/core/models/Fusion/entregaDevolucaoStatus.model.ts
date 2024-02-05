import { EntityBase } from "../entityBase";

export class EntregaDevolucaoStatus extends EntityBase{
	
	
	constructor(){
        super();
        this.id = 0;
	}
    
    IdEntregaTransporteTipo: number;
    Nome: string;
    CodigoIntegracao : number;
    Alias : string;
}
