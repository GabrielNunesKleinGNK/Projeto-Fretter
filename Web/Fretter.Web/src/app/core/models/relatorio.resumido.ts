import { EntityBase } from "./entityBase";

export class RelatorioResumido extends EntityBase {
	
	clienteId:number;
	clienteNome : string;
	dataInicio: Date;
	dataTermino: Date;
	duracao : number;
	valor: number;
}
