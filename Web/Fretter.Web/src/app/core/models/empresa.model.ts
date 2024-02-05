import { EntityBase } from "./entityBase";

	export class Empresa extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         numeroDocumento : string;
         razaoSocial : string;
         nomeFantasia : string;
         iE : string;
         uF : string;
    }
