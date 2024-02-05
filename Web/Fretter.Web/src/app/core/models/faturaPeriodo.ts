import { EntityBase } from "./entityBase";

export class FaturaPeriodo extends EntityBase {
	
	constructor(){
		super();
		this.id = 0;
	}
	
	faturaCicloId : number;
	diaVencimento : number;
	dataInicio : Date;
	dataFim : Date;
	vigente : Date;
	processado : boolean;
	dataProcessamento : Date;
	quantidadeProcessado : number;
	duracaoProcessamento : number;
}
