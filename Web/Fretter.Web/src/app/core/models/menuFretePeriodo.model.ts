import { EntityBase } from "./entityBase";

export class MenuFretePeriodo extends EntityBase{
    constructor() { 
        super();
		this.id = 0;
    }

    DsNome: string;
    HrInicio: any;
    HrTermino: any;
}