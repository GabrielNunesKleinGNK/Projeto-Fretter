import { EntityBase } from "./entityBase";

	export class NotificacaoStatus extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         nome : string;
         provedorId : Number;
         codigoIntegracao : string;
    }
