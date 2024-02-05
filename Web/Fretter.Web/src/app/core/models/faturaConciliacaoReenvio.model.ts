import { EntityBase } from "./entityBase";

export class FaturaConciliacaoReenvio extends EntityBase{

	constructor(faturaConciliacaoId: number, faturaId: number, conciliacaoId: number){
    super();
    this.faturaConciliacaoId = faturaConciliacaoId;
    this.faturaId = faturaId;
    this.conciliacaoId = conciliacaoId;
		this.id = 0;
    }

    faturaConciliacaoId: number;
    faturaId: number;
    conciliacaoId: number;
}