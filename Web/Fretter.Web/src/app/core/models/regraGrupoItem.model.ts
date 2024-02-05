import { EntityBase } from "./entityBase";

export class RegraGrupoItem extends EntityBase{
    constructor(){
		super();
		this.id = 0;
	}

    grupo: string;
    tipo: string;
}