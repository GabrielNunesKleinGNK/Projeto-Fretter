
import { NumericDictionaryIteratee } from "lodash";
import { EntityBase } from "../entityBase";

export class EmpresaIntegracaoItemDetalheDto extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
    }

    entrgaId : number;
    entregaOcorrenciaId  : number;
    ocorrenciaId  : number;
    descricao : string;
    sigla : string;
    dataEnvio : Date;
    sucesso : boolean;
    statusCode : string;
    total: number;
}
