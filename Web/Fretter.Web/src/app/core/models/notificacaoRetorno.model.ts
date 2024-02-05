import { EntityBase } from "./entityBase";

	export class NotificacaoRetorno extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         notificacaoId : Number;
         notificacaoStatusId : Number;
         resposta : string;
    }
