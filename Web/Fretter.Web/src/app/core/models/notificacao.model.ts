import { EntityBase } from "./entityBase";

	export class Notificacao extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         provedorId : Number;
         nome : string;
         email : string;
         telefone : string;
         titulo : string;
         conteudo : string;
         notificacaoTemplateId : Number;
         notificacaoStatusId : Number;
         errorMessage : string;
    }
