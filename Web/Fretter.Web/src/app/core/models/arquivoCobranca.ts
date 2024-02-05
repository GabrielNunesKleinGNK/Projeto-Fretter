import { EntityBase } from "./entityBase";

export class ArquivoCobranca extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
	}

	faturaId: number;
	identificacaoRemetente: string;
	identificacaoDestinatario: string;
	data: Date
	qtdTotal: number;
	arquivoUrl: string;
}
