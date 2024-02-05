import { EntityBase } from "./entityBase";

	export class TransportadorCnpj extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         numeroDocumento : string;
         razaoSocial : string;
         nome : string;
    }
