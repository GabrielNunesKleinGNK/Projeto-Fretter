import { EntityBase } from "./entityBase";

	export class EntregaOcorrenciaImportacao extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         entregaId : number;
         ocorrenciaImportacaoId : number;
         codigoImportacao : string;
         ocorrenciaParametroId : Number;
         dataOcorrencia : Date;
         data : Date;
    }
