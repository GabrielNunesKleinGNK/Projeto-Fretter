import { EntityBase } from "./entityBase";

	export class OcorrenciaParametroAcao extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         ocorrenciaParametroId : Number;
         provedorId : Number;
         notificacaoTemplateId : Number;
    }
