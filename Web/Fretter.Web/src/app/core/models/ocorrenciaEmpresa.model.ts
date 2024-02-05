import { EntityBase } from "./entityBase";

	export class OcorrenciaEmpresa extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         empresaId : Number;
         codigo : string;
         descricao : string;
         ocorrenciaTipoId : Number;

         sigla: string;
         finalizadora: boolean;
    }
