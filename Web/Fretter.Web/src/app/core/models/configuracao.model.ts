import { EntityBase } from "./entityBase";

	export class Configuracao extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         chave : string;
         valor : string;
    }
