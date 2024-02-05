import { EntityBase } from "./entityBase";

	export class NotificacaoTemplate extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         nome : string;
         titulo : string;
         email : string;
         conteudo : string;
         metadados : string;
         codigoIntegracao : string;
    }
