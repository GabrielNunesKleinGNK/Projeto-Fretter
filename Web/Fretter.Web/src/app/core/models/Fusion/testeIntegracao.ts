import { NumericDictionaryIteratee } from "lodash";
import { EntityBase } from "../entityBase";
import { Integracao } from "./integracao";

export class TesteIntegracao extends EntityBase {
	constructor(){
		super();
		this.id = 0;
    }

    configId: number;
    entregaId: number;
    json: string;

    body : string;
    statusCode : number;
    tempo: Date;
}
